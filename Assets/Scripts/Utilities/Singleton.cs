using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton Settings")]
    [SerializeField] private bool m_dontDestroyOnLoad;

    public static T I { get; private set; }

    protected virtual void Awake()
    {
        if (I == null)
        {
            I = this as T;
        }
        else if (I != null && I != this)
        {
            Debug.LogError("There's more than one singleton " + gameObject.name + " of type " + GetType());
            Destroy(gameObject);
        }

        if (m_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}