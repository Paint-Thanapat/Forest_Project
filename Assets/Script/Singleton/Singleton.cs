using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }

            return _instance;
        }
    }


    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR
            Debug.Log("I am Singleton. (" + gameObject.name + ")");
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("I have illusion. Destroy me. (" + gameObject.name + ")");
#endif

            Destroy(gameObject);
        }
    }
}
