using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

/// <summary>
/// Este script implementa la lógica del movimiento del personaje por medio de un análogo o joystick.
/// </summary>
public class MovimientoPlayer : MonoBehaviour
{
    public PlayerInputActions playerInput;

    [SerializeField]
    public CharacterController controller;
    public Transform transformPlayer;
    private Vector3 playerVelocity;
    public float playerSpeed;
    //[SerializeField]
    //private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    public Vector3 move;
    public Vector2 movementInput;

    //COMPONENTES ANALOGO
    public GameObject analogo;
    public RectTransform transformAnalogo;
    public Image imageAnalogo;
    public Image imageHandle;

    //ANALOGO DE REFERENCIA
    public Image imageAnalogoReferencial;
    public float valorTransicionAnalogoReferencial;


    private void Awake()
    {
        playerInput = new PlayerInputActions();   
    }

    private void OnEnable()
    {
        valorTransicionAnalogoReferencial = 0.7f;
        playerInput.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;
    }

    void FixedUpdate()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (imageAnalogo.enabled == true)
        {
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
            movementInput = playerInput.Player.Move.ReadValue<Vector2>();
            move = new Vector3(movementInput.x, 0f, movementInput.y);
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
        else if (imageAnalogo.enabled == false)
        {
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
            movementInput = Vector2.zero;
            move = Vector3.zero;
        }

        if (move != Vector3.zero)
        {
            transformPlayer.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }


    private void FingerDown(Finger finger)
    {
        if (finger.currentTouch.screenPosition.x >= 0f
            && finger.currentTouch.screenPosition.x <= 1000f
            && finger.currentTouch.screenPosition.y <= 600f
            && finger.currentTouch.screenPosition.y >= 0f
            && imageAnalogo.enabled == false)
        {
            StartCoroutine(DesaparecerAnalogoReferencial());
            transformAnalogo.anchoredPosition = finger.currentTouch.screenPosition;
            imageAnalogo.enabled = true;
            imageHandle.enabled = true;
        }
    }

    private void FingerUp(Finger finger)
    {
        if (finger.lastTouch.startScreenPosition.x >= 0f
            && finger.lastTouch.startScreenPosition.x <= 1000f
            && finger.lastTouch.startScreenPosition.y <= 600f
            && finger.lastTouch.startScreenPosition.y >= 0f
            && imageAnalogo.enabled == true)
        {
            StartCoroutine(AparecerAnalogoReferencial());
            imageAnalogo.enabled = false;
            imageHandle.enabled = false;
        }
    }

    IEnumerator DesaparecerAnalogoReferencial()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return null;
            valorTransicionAnalogoReferencial -= 0.14f;
            imageAnalogoReferencial.color = new Color(1f, 1f, 1f, valorTransicionAnalogoReferencial);
        }
    }

    IEnumerator AparecerAnalogoReferencial()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return null;
            valorTransicionAnalogoReferencial += 0.07f;
            imageAnalogoReferencial.color = new Color(1f, 1f, 1f, valorTransicionAnalogoReferencial);
        }
    }

    private void OnApplicationQuit()
    {
        OnDisable();
    }
}
