using Assets.GameJamStarterPack.Scripts.Audio;
using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts.Examples
{
    /// <summary>
    /// Examples of how to use the AudioManager
    /// </summary>
    public class AudioPlayingExample : MonoBehaviour
    {
        void Update()
        {
            // Play sound on Left Mouse Button down
            if (Input.GetMouseButtonDown(0)) {
                AudioManager.Instance.Play2DSound(AudioClipName.Shoot);
            }

            // Play 3D sound on Right Mouse Button down
            if (Input.GetMouseButtonDown(1)) {
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                AudioManager.Instance.PlaySoundAt(AudioClipName.Die, position);
            }
        }
    }
}
