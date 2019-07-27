using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts.Audio
{
    /// <summary>
    /// A SingleShotAudio behave very similar to <see href="https://docs.unity3d.com/ScriptReference/AudioSource.PlayClipAtPoint.html">AudioSource.PlayClipAtPoint</see> 
    /// with the added behavior that you can get the AudioSource generated, something that the AudioSource does not do, and you can request
    /// that the sound be played as 2D for when a position is not required such as UI button presses etc.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SingleShotAudio : MonoBehaviour
    {
        /// <summary>
        /// A reference to the audio source
        /// </summary>
        AudioSource m_source;
        public AudioSource Source
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
        /// Plays the given clip positioning the audio source at the give position
        /// so that the sound originates from that position
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        public void PlaySoundAt(AudioClip clip, Vector3 position, float volume = 1f)
        {
            transform.position = position;
            PlaySound(clip, volume);
        }

        /// <summary>
        /// Plays the given clip with a spatial blend of 0 making it be fully 2D
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        public void Play2DSound(AudioClip clip, float volume = 1f)
        {
            PlaySound(clip, volume, 0f);
        }

        /// <summary>
        /// Setups the audio source to play the given sound at the given volume/spatial blend 
        /// A spatial blen higher than 0 makes the sound 3D with the highest value being 1
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="spatialBlend"></param>
        void PlaySound(AudioClip clip, float volume = 1f, float spatialBlend = 1f)
        {
            if (clip == null) {
                Destroy(gameObject);
                return;
            }

            m_soundPlayed = true;

            // Keeps the Hierarchy a little cleaner
            gameObject.name = clip.name + "_AudioSource";

            Source.volume = Mathf.Clamp01(volume);
            Source.clip = clip;
            Source.loop = false;
            Source.spatialBlend = Mathf.Clamp01(spatialBlend);
            Source.Play();
        }
    }
}
