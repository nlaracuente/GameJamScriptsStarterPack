using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField, Tooltip("Plays music only when enabled")]
        bool m_musicEnabled = true;

        [SerializeField, Tooltip("Plays music only when enabled")]
        bool m_fxEnabled = true;

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
                        GameObject clipGO = new GameObject("SampleFxClip", typeof(SingleShot2DAudio));
                        m_sampleFxClip = clipGO.GetComponent<SingleShot2DAudio>();
                        m_sampleFxClip.PlaySound(m_fxSampleClip, m_fxVolume);
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
        SingleShot2DAudio m_sampleFxClip;

    }
}
