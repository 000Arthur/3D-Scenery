using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;
    public float flickerSpeed = 0.1f;  
    public float minIntensity = 10.0f;  
    public float maxIntensity = 10.0f;  
    private float nextFlickerTime = 0.0f;

    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    void Update()
    {
        if (Time.time > nextFlickerTime)
        {
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);
            nextFlickerTime = Time.time + Random.Range(0.05f, 0.2f); 
        }
    }
}
