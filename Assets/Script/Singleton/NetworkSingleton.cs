using Photon.Pun;
using UnityEngine;

public class NetworkSingleton<T> : MonoBehaviourPunCallbacks
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

            PV = GetComponent<PhotonView>();

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

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (GetComponent<PhotonView>() == null)
        {
            gameObject.AddComponent<PhotonView>();
        }
    }
#endif

    public PhotonView PV { get; private set; }
}
