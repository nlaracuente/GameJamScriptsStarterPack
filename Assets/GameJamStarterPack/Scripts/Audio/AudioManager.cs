using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField, Tooltip("Enables/Disables the playing on music by default")]
        bool m_musicEnabled = true;

        /// <summary>
        /// Turns the music aduio source ON/OFF 
        /// </summary>
        public bool MusicEnabled
        {
            get { return m_musicEnabled; }
            set {
                m_musicEnabled = value;
                MusicAudioSource.enabled = value;
            }
        }

        [SerializeField, Tooltip("Enables/Disables the playing on sound fxs by default")]
        bool m_fxEnabled = true;

        /// <summary>
        /// Turns the playing of sounds effects ON/OFF  
        /// When OFF, all currently playing sounds will be stopped
        /// </summary>
        public bool FxsEnabled
        {
            get { return m_fxEnabled; }

            set {
                m_fxEnabled = value;
                m_fxSources.ForEach(source => {
                    if(source != null && !value) {
                        source.Stop();
                    }
                });
            }
        }

        [SerializeField, Range(0f, 1f), Tooltip("Master music volume level")]
        float m_musicVolume = 1f;

        /// <summary>
        /// Returns or sets the current music volume setting
        /// </summary>
        public float MusicVolume
        {
            get { return m_musicVolume; }
            set {

                m_musicVolume = Mathf.Clamp01(value);
                MusicAudioSource.volume = m_musicVolume;
            }
        }

        [SerializeField, Range(0f, 1f), Tooltip("Master sound fxs volume level")]
        float m_fxVolume = 1f;

        /// <summary>
        /// Get/Sets the current sound fx volume
        /// If a sound fx sample clip is available then it plays it as a 2D Sound
        /// </summary>
        public float FxVolume
        {
            get { return m_fxVolume; }
            set {
                m_fxVolume = Mathf.Clamp01(value);

                // A sample clip is available to play
                if (m_fxSampleClip != null) {

                    // Only play when the previous sample clip is done playing
                    if (m_sampleFxClip == null || !m_sampleFxClip.IsPlaying) {
                        GameObject clipGO = new GameObject("SampleFxClip", typeof(SingleShotAudio));
                        m_sampleFxClip = clipGO.GetComponent<SingleShotAudio>();
                        m_sampleFxClip.Play2DSound(m_fxSampleClip, m_fxVolume);
                    }
                }
            }
        }

        /// <summary>
        /// Current game music clip
        /// </summary>
        [SerializeField, Tooltip("Music to play when the AudioManager is loaded")]
        AudioClip m_musicClip; 

        /// <summary>
        /// A reference to the attached audio source for playing music
        /// </summary>
        AudioSource m_musicAudioSource;
        AudioSource MusicAudioSource
        {
            get {
                if (m_musicAudioSource == null) {
                    m_musicAudioSource = GetComponent<AudioSource>();
                    m_musicAudioSource.loop = true;
                    m_musicAudioSource.clip = m_musicClip;
                    m_musicAudioSource.volume = m_musicVolume;

                    // Ensures the music always plays from the beginning
                    m_musicAudioSource.Stop();
                    m_musicAudioSource.Play();
                }
                return m_musicAudioSource;
            }
        }

        /// <summary>
        /// The audio clip to play to show sound fx volume change
        /// </summary>
        [SerializeField, Tooltip("The audio clip to play when changing the sound fx volume")]
        AudioClip m_fxSampleClip;

        /// <summary>
        /// A reference to the current fx sample clip playing
        /// This is to ensure we don't playing it too many times
        /// </summary>
        SingleShotAudio m_sampleFxClip;

        /// <summary>
        /// A collection of AudioSources of all currently playing audio clips
        /// </summary>
        List<AudioSource> m_fxSources = new List<AudioSource>();

        /// <summary>
        /// Plays the given clip as 2D sound which means it will be heard equally from all speakers
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        public AudioSource Play2DSound(AudioClip clip, float volume = 1f)
        {
            SingleShotAudio fx = CreateNewFxSource();
            fx.Play2DSound(clip, Mathf.Clamp01(volume * FxVolume));
            return fx.Source;
        }

        /// <summary>
        /// Plays the given clip as a 3D sound by making the sound originate from the given position
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="position"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        public AudioSource PlaySoundAt(AudioClip clip, Vector3 position, float volume = 1f)
        {
            SingleShotAudio fx = CreateNewFxSource();
            fx.PlaySoundAt(clip, position, Mathf.Clamp01(volume * FxVolume));
            return fx.Source;
        }

        /// <summary>
        /// Returns a new instance of a SingleShotAudio
        /// AudioSources created are stored in <see cref="m_fxSources"/>
        /// </summary>
        /// <returns></returns>
        SingleShotAudio CreateNewFxSource()
        {
            SingleShotAudio audio = new GameObject("SingleShotAudio", typeof(SingleShotAudio)).GetComponent<SingleShotAudio>();
            
            // Keeps the hierarchy a little cleaner by putting all spawned audio under the manager
            audio.gameObject.transform.SetParent(transform);

            m_fxSources.Add(audio.Source);            
            return audio;
        }

        /// <summary>
        /// Triggers the music to play or pause 
        /// </summary>
        /// <param name="play"></param>
        void ToggleMusic(bool play)
        {
            if (!play && MusicAudioSource.isPlaying) {
                MusicAudioSource.Pause();
            } else if (play && !MusicAudioSource.isPlaying) {
                MusicAudioSource.Play();
            }
        }
    }
}
