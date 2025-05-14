using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;
    public float flickerSpeed = 0.1f;  // Velocidad del parpadeo
    public float minIntensity = 0.3f;  // Intensidad mínima
    public float maxIntensity = 1.0f;  // Intensidad máxima
    private float nextFlickerTime = 0.0f;

    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    void Update()
    {
        // Solo cambia la intensidad de la luz en intervalos aleatorios
        if (Time.time > nextFlickerTime)
        {
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);
            nextFlickerTime = Time.time + Random.Range(0.05f, 0.2f); // Intervalo aleatorio entre parpadeos
        }
    }
}
