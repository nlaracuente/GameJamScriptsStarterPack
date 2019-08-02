using UnityEngine;

/// <summary>
/// Maps a pixle color to a prefab
/// </summary>
[System.Serializable]
public struct ColorHexToPrefab
{
    /// <summary>
    /// The color that represent the prefab
    /// </summary>
    public Color32 color;

    /// <summary>
    /// The prefab itself
    /// </summary>
    public GameObject prefab;
}
