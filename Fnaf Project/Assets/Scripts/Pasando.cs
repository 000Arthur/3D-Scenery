using UnityEngine;
using UnityEngine.InputSystem;

public class BotonPuerta : MonoBehaviour
{
    public Camera camaraJugador;   // arrástrala desde el inspector
    public bool doorState;
    public float range = 2;
    private InputSystem_Actions input;
    public Animator Door;
    public string activator;

    public GameObject textoInteraccion;
    public Transform textoTransform; // para cambiar posición/escala directamente

    public AudioSource Sound;

    private void Awake()
    {
        input = new InputSystem_Actions();
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
                doorState = !doorState;
                Sound.Play();
            }
            if (doorState) Door.Play("Cerrar");
            else Door.Play("Abrir");
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

