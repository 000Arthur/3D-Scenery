using UnityEngine;

public class ClosetLightSwitch : MonoBehaviour
{
    public Light closetLight;
    public AudioSource clickSound;

    private bool isOn = false;

    public Renderer lampRenderer;

    public Color lightOnColor = Color.yellow;
    public Color lightOffColor = Color.gray;

    void Start()
    {
        closetLight = GetComponentInChildren<Light>();
        lampRenderer = GetComponentInChildren<MeshRenderer>();

        if (closetLight != null)
            closetLight.enabled = isOn;

        ChangeLampColor(isOn);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOn = !isOn;
            closetLight.enabled = isOn;

            if (clickSound != null)
                clickSound.Play();

            ChangeLampColor(isOn);
        }
    }

    void ChangeLampColor(bool isOn)
    {
        if (lampRenderer != null)
        {
            lampRenderer.materials[1].SetColor("_BaseColor", isOn ? lightOnColor : lightOffColor);

            if (isOn)
            {
                lampRenderer.materials[1].SetColor("_EmissionColor", lightOnColor);
                lampRenderer.materials[1].EnableKeyword("_EMISSION");
            }
            else
            {
                lampRenderer.materials[1].SetColor("_EmissionColor", lightOffColor);
                lampRenderer.materials[1].DisableKeyword("_EMISSION");
            }
        }
    }
}
