using UnityEngine;
using UnityEditor;

/// <summary>
/// This Editor script gives you the ability to run the "Build Level" option
/// after adding the Level Builder to the scene.
/// 
/// Note: 
///     Objects are spawned as prefab instances which means you can update the master prefab
///     to affect all the ones the level builder creates. However, each time you hit the 
///     "Build Level" button all currently modified prefabs are destroyed and re-created with 
///     the default settings. This is a current limitation of this builder but it should make
///     level builder more quickly in a jam setting. ;)
/// </summary>
[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderCustomEditor : Editor
{
    /// <summary>
    /// A reference to the builder script
    /// </summary>
    LevelBuilder m_builder;

    /// Only enable the build functionality while editor mode
    #region EDITOR_UI 
#if (UNITY_EDITOR)
    private void OnEnable()
    {
        m_builder = (LevelBuilder)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Build Level")) {
            BuildLevel(m_builder.LevelTexture);
        }
        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
#endif
    #endregion

    /// <summary>
    /// Builds a level using the given texture
    /// This BuildLevel functionality is designed to create a level saved to a scene
    /// so that you can make changes to the level and keep them at run time
    /// </summary>
    /// <param name="levelTexture"></param>
    public void BuildLevel(Texture2D levelTexture)
    {
        if (!levelTexture) {
            Debug.LogError("Level texture cannot be null!");
            return;
        }

        ClearLevel();

        m_builder.CreatePrefabMapping();

        for (int x = 0; x < levelTexture.width; x++) {
            for (int z = 0; z < levelTexture.height; z++) {
                Color32 colorId = levelTexture.GetPixel(x, z);
                GameObject prefab = m_builder.GetPrefabByColorId(colorId);

                if (!prefab) {
                    Debug.LogWarningFormat(
                        "Color Id {0} at position {1}, {2} is not associated with a prefab",
                        colorId, x, z
                    );
                    continue;
                }

                Vector3 position = new Vector3(
                    x * m_builder.TileScale.x, 
                    prefab.transform.position.y, 
                    z * m_builder.TileScale.y
                );
                GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                go.transform.position = position;
                go.name = string.Format("{0}_{1}_{2}", prefab.name, x, z);
                go.transform.SetParent(m_builder.LevelTransform);
            }
        }
    }    

    /// <summary>
    /// Creates the level object container if it does not exist
    /// Deletes all the level objects
    /// </summary>
    public void ClearLevel()
    {
        m_builder.ClearLevel();
    }
}