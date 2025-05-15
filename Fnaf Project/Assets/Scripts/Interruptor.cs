using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class LuzControlRaycast : MonoBehaviour
{
    public Light luz_1;
    public Light luz_2;
    public Light luz_3;

    public float delay = 1f; // Tiempo entre luces (en segundos)

    private void Awake()
    {
        luz_1.enabled = false;
        luz_2.enabled = false;
        luz_3.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TurnOnLightsSequentially());
    }
    IEnumerator TurnOnLightsSequentially()
    {
        luz_1.enabled = true;
        yield return new WaitForSeconds(delay);  // Espera 1 segundo

        luz_2.enabled = true;
        yield return new WaitForSeconds(delay);  // Espera 1 segundo

        luz_3.enabled = true;
    }
}