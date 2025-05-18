using UnityEngine;
using UnityEngine.InputSystem;

public class BotonLuz : MonoBehaviour
{
    public Camera camaraJugador;   // arrástrala desde el inspector
    public float range;
    private InputSystem_Actions input;
    public Light luz;
    public string activator;

    public GameObject textoInteraccion;
    public Transform textoTransform; // para cambiar posición/escala directamente

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
    private void Update()
    {
        MostrarIndicador();
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

    private void MostrarIndicador()
    {
        Ray ray = camaraJugador.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            float distancia = Vector3.Distance(camaraJugador.transform.position, hit.point);

            if (hit.collider.CompareTag(activator))
            {

                textoInteraccion.SetActive(true);

                // Coloca el texto justo frente al botón
                textoTransform.position = hit.point;

                // Hacer que mire hacia la cámara
                textoTransform.LookAt(camaraJugador.transform);
                textoTransform.Rotate(0, 180f, 0);

                return;

            }
        }

        // Si no estás mirando al botón
        textoInteraccion.SetActive(false);
    }
}