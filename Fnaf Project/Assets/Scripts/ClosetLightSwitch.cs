using UnityEngine;
using UnityEngine.InputSystem;

public class ClosetLightSwitch : MonoBehaviour
{
    public Light closetLight;
    public AudioSource clickSound;
    public Renderer lampRenderer;

    public Color lightOnColor = Color.yellow;
    public Color lightOffColor = Color.gray;

    public Camera camaraJugador;
    public float range;

    public string activatorTag = "Boton supplyCloset";

    private bool isOn = false;
    private InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
      
    }

    private void OnEnable()
    {
        input.Gameplay.ToggleLight.performed += IntentarActivarLuzInput;
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.ToggleLight.performed -= IntentarActivarLuzInput;
        input.Gameplay.Disable();
    }

    private void IntentarActivarLuzInput(InputAction.CallbackContext context)
    {
        IntentarActivarLuz();
    }


    private void Start()
    {
        if (closetLight == null)
            closetLight = GetComponentInChildren<Light>();

        if (lampRenderer == null)
            lampRenderer = GetComponentInChildren<MeshRenderer>();

        closetLight.enabled = isOn;
        ChangeLampColor(isOn);
    }

    void IntentarActivarLuz()
    {
        Ray ray = camaraJugador.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag(activatorTag) || Input.GetKeyDown(KeyCode.E))
            {
                isOn = !isOn;
                closetLight.enabled = isOn;

                if (clickSound != null)
                    clickSound.Play();

                ChangeLampColor(isOn);
            }
        }
    }

    void ChangeLampColor(bool isOn)
    {
        if (lampRenderer != null && lampRenderer.materials.Length > 1)
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
