using UnityEngine;
using UnityEngine.InputSystem;

public class BotonPuerta : MonoBehaviour
{
    public Camera camaraJugador;   // arrástrala desde el inspector
    public bool doorState;
    public float range;
    private Controles input;
    public Animator Door;
    public string activator;

    private void Awake()
    {
        input = new Controles();
        doorState = true;
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
                doorState = !doorState;
            }
            if (doorState) Door.Play("Cerrar");
            else Door.Play("Abrir");
        }
    }
}