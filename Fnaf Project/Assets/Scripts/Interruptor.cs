using UnityEngine;
using UnityEngine.InputSystem;

public class LuzControlRaycast : MonoBehaviour
{
    public Light luz;
    public AudioSource sonido;
    private void Awake()
    {
        luz.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        luz.enabled = true;
    }
     
}