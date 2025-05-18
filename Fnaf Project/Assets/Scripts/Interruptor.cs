using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class LuzControlRaycast : MonoBehaviour
{
    public Light luz_1;
    public Light luz_2;
    public Light luz_3;
    
    public AudioSource[] Sounds;

    public float delay = 1f; // Tiempo entre luces (en segundos)

    private bool oneTime = false;

    private void Awake()
    { 
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TurnOnLightsSequentially());
    }
    IEnumerator TurnOnLightsSequentially()
    {
        if (!oneTime) {
            oneTime = true;

            luz_1.enabled = true;
            Sounds[0].Play();
            yield return new WaitForSeconds(delay);  // Espera 1 segundo

            luz_2.enabled = true;
            Sounds[1].Play();

            yield return new WaitForSeconds(delay);  // Espera 1 segundo

            luz_3.enabled = true;
            Sounds[2].Play();
        }
    }
}