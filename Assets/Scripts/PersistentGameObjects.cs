using UnityEngine;

public class PersistentGameObjects : MonoBehaviour
{
    ///  -----------
    /// UNITY METHODS
    ///  -----------
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
