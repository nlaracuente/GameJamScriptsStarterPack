using Assets.GameJamStarterPack.Scripts.Audio;
using UnityEngine;

/// <summary>
/// An example on how an object might request the AudioManager to play a sound that originates 
/// at a certain position
/// </summary>
public class AudioTrigger3D : MonoBehaviour
{
    [SerializeField, Tooltip("Which clip to demo")]
    AudioClipName m_clipName = AudioClipName.Cancel;

    /// <summary>
    /// Play sound originating from this object
    /// </summary>
    void OnMouseDown()
    {
        // Camera.main.transform.TransformPoint(transform.position)
        AudioManager.Instance.PlaySoundAt(m_clipName, transform.position);
    }
}
