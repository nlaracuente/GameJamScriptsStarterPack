using System;
using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts.Audio
{
    /// <summary>
    /// Configuration settings for when playing a 3D sound
    /// For more information for what each one does visit:
    /// <see href="https://docs.unity3d.com/Manual/class-AudioSource.html">AudioSource Info Page</see>
    /// </summary>
    [Serializable]
    public class Sound3DSettings
    {
        /// <summary>
        /// Volume to play the sound at taking into account that the Master Volume in AudioManager affects this 
        /// </summary>
        public float Volume = 1f;

        /// <summary>
        /// True: causes the audio to play in a loop
        /// </summary>
        public bool Loops = false;

        /// <summary>
        /// Determines how much doppler effect will be applied to this audio source
        /// (if is set to 0, then no effect is applied)
        /// </summary>
        [Range(0f, 5f)]
        public float DopplerLevel = 1f;

        /// <summary>
        /// Sets the spread angle to 3D stereo or multichannel sound in speaker space.
        /// </summary>
        [Range(0f, 360f)]
        public float Spread = 0f;

        /// <summary>
        /// Within the MinDistance, the sound will stay at loudest possible. 
        /// Outside MinDistance it will begin to attenuate. 
        /// Increase the MinDistance of a sound to make it ‘louder’ in a 3d world, 
        /// and decrease it to make it ‘quieter’ in a 3d world.
        /// </summary>
        public float MinDistance = 1f;

        /// <summary>
        /// The distance where the sound stops attenuating at. 
        /// Beyond this point it will stay at the volume it would be at MaxDistance 
        /// units from the listener and will not attenuate any more.
        /// </summary>
        public float MaxDistance = 500f;

        /// <summary>
        /// How fast the sound fades. 
        /// The higher the value, the closer the Listener has to be before hearing the sound. 
        /// (This is determined by a Graph)
        /// 
        ///     - Logarithmic Rolloff: The sound is loud when you are close to the audio source, 
        ///                            but when you get away from the object it decreases significantly fast.
        ///     - Linear Rolloff:      The further away from the audio source you go, the less you can hear it.
        ///     - Custom Rolloff:      The sound from the audio source behaves accordingly to how you set the graph of roll offs.
        ///     
        /// Note:
        ///     For "custom rolloff" it is best to create your own audiosource to manipulate the curve and use that
        ///     to trigger sounds
        /// </summary>
        public AudioRolloffMode VolumeRolloff = AudioRolloffMode.Logarithmic;
    }
}
