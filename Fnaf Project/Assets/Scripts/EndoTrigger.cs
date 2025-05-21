using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class EndoTrigger : MonoBehaviour
{

    public Animator EndoMove;

    public AudioSource[] Sounds;

    public float delay = 3f; // Tiempo entre luces (en segundos)

    private bool oneTime = false;

    private void Awake()
    { 
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TurnOnEndo());
    }
    IEnumerator TurnOnEndo()
    {
        if (!oneTime) {
            oneTime = true;
            Sounds[0].Play();
            EndoMove.Play("EndoMove");
            yield return new WaitForSeconds(delay);  // Espera 1 segundo
            
            Sounds[1].Play();
            EndoMove.Play("EndoShutDown");

        }
    }
}