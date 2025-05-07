using UnityEngine;
using UnityEngine.InputSystem;

public class BotonLuz : MonoBehaviour
{
    public Camera camaraJugador;   // arrástrala desde el inspector
    public float range;
    private InputSystem_Actions input;
    public Light luz;
    public string activator;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Gameplay.ToggleLight.performed += IntentarActivarLuz;
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.ToggleLight.performed -= IntentarActivarLuz;
        input.Gameplay.Disable();
    }

    private void IntentarActivarLuz(InputAction.CallbackContext context)
    {
        Ray ray = camaraJugador.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range)) // metros de alcance
        {
            if (hit.collider.CompareTag(activator))
            {
                luz.enabled = !luz.enabled;
            }
        }
    
    }
}