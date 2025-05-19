using UnityEngine;
using UnityEngine.InputSystem;

public class PressBotonLuz : MonoBehaviour
{
    public Camera camaraJugador;   // Cámara del jugador
    public float range;            // Rango máximo de interacción
    public Light luz;              // Luz a encender/apagar
    public string activator;       // Tag del botón/interactuable

    public GameObject textoInteraccion;  // UI con la "E"
    public Transform textoTransform;     // RectTransform del texto
    public AudioSource Sound;            // Sonido al encender

    private InputSystem_Actions input;   // Input System personalizado
    private bool estaPresionando = false;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Gameplay.ToggleLight.started += OnBotonPresionado;
        input.Gameplay.ToggleLight.canceled += OnBotonSoltado;
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.ToggleLight.started -= OnBotonPresionado;
        input.Gameplay.ToggleLight.canceled -= OnBotonSoltado;
        input.Gameplay.Disable();
    }

    private void Update()
    {
        MostrarIndicadorYControlarLuz();
    }

    private void OnBotonPresionado(InputAction.CallbackContext ctx)
    {
        estaPresionando = true;
    }

    private void OnBotonSoltado(InputAction.CallbackContext ctx)
    {
        estaPresionando = false;
    }

    private void MostrarIndicadorYControlarLuz()
    {
        Ray ray = camaraJugador.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            float distancia = Vector3.Distance(camaraJugador.transform.position, hit.point);

            if (hit.collider.CompareTag(activator))
            {
                textoInteraccion.SetActive(true);

                // Posición de la UI en el mundo (levemente encima del botón)
                textoTransform.position = hit.point;

                // Hacer que mire hacia la cámara
                textoTransform.LookAt(camaraJugador.transform);
                textoTransform.Rotate(0, 180f, 0); // Corregir orientación

                // Escalar según distancia
                float escala = Mathf.Lerp(1.2f, 0.3f, distancia / range);
                textoTransform.localScale = Vector3.one * escala;

                // Activar o desactivar la luz según si se mantiene presionando
                if (estaPresionando)
                {
                    if (!luz.enabled)
                    {
                        luz.enabled = true;
                        Sound.Play(); // solo suena al encender
                    }
                }
                else
                {
                    if (luz.enabled)
                        luz.enabled = false;
                }

                return;
            }
        }

        // Si no se está mirando al botón
        textoInteraccion.SetActive(false);
        if (luz.enabled)
            luz.enabled = false;
    }
}