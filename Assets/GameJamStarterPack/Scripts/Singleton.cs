using UnityEngine;

namespace Assets.GameJamStarterPack.Scripts
{
    /// <summary>
    /// A generic singleton base calss
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// An instance of self
        /// </summary>
        static T m_instance;

        /// <summary>
        /// True: prevents the object from being destroyed
        /// </summary>
        [SerializeField, Tooltip("Enable this to prevent the object from being destroyed")]
        bool m_isPersistent = false;

        /// <summary>
        /// Returns the instance of this object
        /// In the case this is not a persistent object we will try to find an existing instance
        /// </summary>
        public static T Instance
        {
            get {
                m_instance = m_instance ?? FindObjectOfType<T>();
                if (m_instance == null) {
                    m_instance = new GameObject(nameof(T), typeof(T)) as T;
                }

                return m_instance;
            }
        }

        /// <summary>
        /// Sets this object as the current instance if one does not exist
        /// Destroys this object if it is not the current instance
        /// Prevents this object from being destroyed if the <see cref="m_isPersistent"/> is enabled
        /// </summary>
        public virtual void Awake()
        {
            if (m_instance == null) {
                m_instance = this as T;

                if (m_isPersistent) {
                    // Object cannot be a child or else Unity won't let us make it persistent
                    transform.SetParent(null);
                    DontDestroyOnLoad(gameObject);
                }

            } else if (m_instance != this) {
                DestroyImmediate(gameObject);
            }
        }
    }
}