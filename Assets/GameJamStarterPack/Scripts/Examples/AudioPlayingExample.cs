using Assets.GameJamStarterPack.Scripts.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
