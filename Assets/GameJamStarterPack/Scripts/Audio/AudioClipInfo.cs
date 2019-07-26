using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts.Audio
{
    /// <summary>
    /// Maps the name of a clip with an audio clip
    /// </summary>
    [System.Serializable]
    public class AudioClipInfo
    {
        [SerializeField, Tooltip("A name to identify the clip by")]
        protected AudioClipName m_clipName;

        [SerializeField, Tooltip("The aduio clip to play")]
        protected AudioClip m_audioClip;

        public AudioClipName Name { get { return m_clipName; } }
        public AudioClip Clip { get { return m_audioClip; } }
    }
}
