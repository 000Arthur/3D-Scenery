using UnityEngine;
using UnityEngine.InputSystem;

public class Linterna : MonoBehaviour
{
    public Camera camaraJugador;   // arrástrala desde el inspector
    private InputSystem_Actions input;
    public Light luz;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Player.Light.performed += IntentarActivarLuz;
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Light.performed -= IntentarActivarLuz;
        input.Player.Disable();
    }

    private void IntentarActivarLuz(InputAction.CallbackContext context)
    {
        luz.enabled = !luz.enabled;
    }
}