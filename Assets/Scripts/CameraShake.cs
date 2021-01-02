using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    [SerializeField] UnityEvent shake;

    void Awake()
    {
        instance = this;
    }

    public void Shake()
    {
        shake.Invoke();
    }
}
