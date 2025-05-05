using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneInteract : MonoBehaviour
{
    public Camera camaraJugador;   // arrástrala desde el inspector
    public float range;
    private Controles input;
    private bool firstClick;

    public AudioSource Ring;
    public AudioSource PhoneGuy;

    private void Awake()
    {
        input = new Controles();
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
            if (hit.collider.CompareTag("Phone"))
            {
                if (!firstClick) {
                    firstClick = true;
                    Ring.Stop();
                    PhoneGuy.Play();
                }
            }
        
        }

    }
}