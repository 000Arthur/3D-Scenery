using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class LuzControl2 : MonoBehaviour
{


    public Light[] Grouplights_1;
    public Light[] Grouplights_2;
    public Light[] Grouplights_3;
    public Light[] Grouplights_4;

    public float delay = 1f; // Tiempo entre luces (en segundos)

    private void Awake()
    { 
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TurnOnLightsSequentially());
    }
    IEnumerator TurnOnLightsSequentially()
    {
        foreach (Light light in Grouplights_1) 
            light.enabled = true;
        yield return new WaitForSeconds(delay);  // Espera 1 segundo

        foreach (Light light in Grouplights_2)
            light.enabled = true;
        yield return new WaitForSeconds(delay);  // Espera 1 segundo

        foreach (Light light in Grouplights_3)
            light.enabled = true;
        yield return new WaitForSeconds(delay);  // Espera 1 segundo

        foreach (Light light in Grouplights_4)
            light.enabled = true;
    }
}