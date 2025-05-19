using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClosetLightSwitch : MonoBehaviour
{
    public Light closetLight;
    public AudioSource Sound;
    public Renderer lampRenderer;

    public Color lightOnColor = Color.yellow;
    public Color lightOffColor = Color.gray;

    public Camera camaraJugador;
    public float range;
    public string activator = "Boton supplyCloset";

    public GameObject textoInteraccion;
    public Transform textoTransform; // para cambiar posición/escala directamente

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
    private void Update()
    {
        MostrarIndicador();
    }

    private void IntentarActivarLuzInput(InputAction.CallbackContext context)
    {
        Ray ray = camaraJugador.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range)) // metros de alcance
        {
            if (hit.collider.CompareTag(activator))
            {
                closetLight.enabled = !closetLight.enabled;
                isOn = !isOn;
                ChangeLampColor(isOn);
                Sound.Play();

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
