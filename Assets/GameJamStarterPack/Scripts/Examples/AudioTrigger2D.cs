using Assets.GameJamStarterPack.Scripts.Audio;
using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts.Examples
{
    /// <summary>
    /// Examples of how to use the AudioManager for playing 2D sounds
    /// </summary>
    public class AudioTrigger2D : MonoBehaviour
    {
        void Update()
        {
            // Play sound on space bar pressed
            if (Input.GetKeyDown(KeyCode.Space)) {
                AudioManager.Instance.Play2DSound(AudioClipName.Confirm);
            }
        }
    }
}
