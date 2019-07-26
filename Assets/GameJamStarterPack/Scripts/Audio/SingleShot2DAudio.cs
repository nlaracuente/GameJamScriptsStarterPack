using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts.Audio
{
    /// <summary>
    /// Behaves like an AudioSource.PlayClipAtPoint instances but for 2D sounds
    /// This means that the audio's spatial blend is always set to 0 to not play as 3D
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SingleShot2DAudio : MonoBehaviour
    {
        /// <summary>
        /// A reference to the audio source
        /// </summary>
        AudioSource m_source;
        AudioSource Source
        {
            get {
                if (m_source == null) {
                    m_source = GetComponent<AudioSource>();
                }

                return m_source;
            }
        }

        /// <summary>
        /// True while the audio source is playing
        /// </summary>
        public bool IsPlaying { get { return Source.isPlaying; } }

        /// <summary>
        /// True once the sound has been triggered to play
        /// </summary>
        bool m_soundPlayed = false;

        /// <summary>
        /// Ensures the audio is destroyed even when Time.timeScale is 0
        /// We could have used clip.length to destroy later but that is affected by Time.timeScale
        /// which means that the audio source will continue to exist in the hierachy and take up resources
        /// until the Time.timescale is no longer 0
        /// </summary>
        void Update()
        {
            if (m_soundPlayed && !Source.isPlaying) {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Plays the given sound as a 2D sound
        /// The object self destroys during Update once it is done playing
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        public void PlaySound(AudioClip clip, float volume = 1f)
        {
            if (clip == null) {
                Destroy(gameObject);
                return;
            }

            m_soundPlayed = true;

            // Ensure volume is within range
            volume = Mathf.Clamp01(volume);
                
            gameObject.name = clip.name + "_AudioSource";

            Source.volume = volume;
            Source.clip = clip;
            Source.loop = false;
            Source.spatialBlend = 0; // makes it 2D
            Source.Play();
        }
    }
}
