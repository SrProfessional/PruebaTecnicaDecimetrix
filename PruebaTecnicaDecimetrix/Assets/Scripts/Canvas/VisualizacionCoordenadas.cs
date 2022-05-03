using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

/// <summary>
/// Este script implementa la lógica para la visualización de las distancias de cada elemento en metros en la UI,
/// alerta sobre elementos a menos de 4m de distancia a una escala de 0.15f del player por medio de la vibración del dispositivo móvil, activa la camara al tocarlos
/// y los recoge al hacer doble tap con la camara activada.
/// </summary>
public class VisualizacionCoordenadas : MonoBehaviour
{
    //TEXTOS DE LA INTERFAZ DE USUARIO
    public TextMeshProUGUI txtDistCuboA;
    public TextMeshProUGUI txtDistPrismaB;
    public TextMeshProUGUI txtDistCilindroC;

    public CtrMenu ctrMenu;

    public List<GameObject> listaElementos;
    public CtrSlotsInventario ctrSlotsInventario;

    public bool activarCamara;
    public bool desactivarCamara;
    private bool mostrarDistancias;
    private bool vibrarDispositivo;

    //SONIDOS
    public AudioSource sourceSeleccionarElemento;
    public AudioClip soundSeleccionarElementoCorrecto;
    public AudioClip soundSeleccionarElementoIncorrecto;

    protected void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    protected void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Awake()
    {
        for (int i = 0; i < listaElementos.Count; i++)
        {
            PlayerPrefs.SetInt(listaElementos[i].tag + "Recogido", 0);
        }
    }

    void Start()
    {
        activarCamara = true;
        desactivarCamara = true;
        mostrarDistancias = true;
        vibrarDispositivo = true;
    }

    void Update()
    {
        //LOS CONDICIONALES PARA LAS CORUTINAS HACEN QUE ESTAS SE REPRODUZCAN UNA ÚNICA VEZ EN EL MÉTODO DE UPDATE Y ASI EVITAN SOBRECARGAS
        //--------------------------------------------------------------------------------------------------------------------------------------
        if (mostrarDistancias)
        {
            StartCoroutine(MostrarDistanciasEnInterfaz()); //CORUTINA QUE MUESTRA LAS DISTANCIAS DEL PLAYER HACIA LOS ELEMENTOS CONSTANTEMENTE
        }

        if(vibrarDispositivo)
        {
            StartCoroutine(VibrarDispositivo());
        }

        if (activarCamara) //ESTE CONDICIONAL ACTÚA CUANDO LA CAMARA AR SE ENCUENTRA DESACTIVADA
        {
            StartCoroutine(ActivarCamara()); //CORUTINA QUE ACTIVA LA AR CAMERA PARA PODER VISUALIZAR EL OBJETO SELECCIONADO EN AR Y PODERLO RECOGER
        }
        else if (!activarCamara && desactivarCamara) //ESTE CONDICIONAL ACTÚA CUANDO LA CAMARA AR SE ENCUENTRA ACTIVADA
        {
            StartCoroutine(TomarElemento()); //CORUTINA QUE RECIBE EL DOBLE TAP PARA RECOGER EL ELEMENTO
        }
    }

    IEnumerator ActivarCamara()
    {
        if (Touch.activeFingers.Count == 1 && Touch.activeFingers[0].currentTouch.isTap)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Touch.activeFingers[0].currentTouch.screenPosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                for (int i = 0; i < listaElementos.Count; i++)
                {
                    if (raycastHit.transform.tag == listaElementos[i].tag)
                    {
                        if (listaElementos[i].GetComponent<ClaseElemento>().distToPlayer <= 4f)
                        {
                            activarCamara = false;
                            sourceSeleccionarElemento.PlayOneShot(soundSeleccionarElementoCorrecto);
                            ctrMenu.ActivarCamara();

                            //DESACTIVA TODOS LOS ELEMENTOS PARA LUEGO ACTIVAR EL QUE SE TOCÓ Y PODERLO MOSTRAR SOLO
                            for (int j = 0; j < listaElementos.Count; j++)
                            {
                                listaElementos[j].SetActive(false);
                            }

                            //MUESTRA SOLO EL ELEMENTO QUE SE TOCÓ
                            listaElementos[i].SetActive(true);
                        }
                        else
                        {
                            sourceSeleccionarElemento.PlayOneShot(soundSeleccionarElementoIncorrecto);
                        }
                    }
                }
            }
        }

        yield return null;
    }

    IEnumerator TomarElemento()
    {
        if (Touch.activeFingers.Count == 1 && Touch.activeTouches[0].tapCount == 2)
        {
            desactivarCamara = false;

            for (int i = 0; i < listaElementos.Count; i++)
            {
                if (listaElementos[i].activeSelf == true)
                {
                    listaElementos[i].SetActive(false);
                    ctrSlotsInventario.listaSlots[ctrSlotsInventario.listaElementosRecogidos.Count].sprite = listaElementos[i].GetComponent<ClaseElemento>().imgElemento;
                    ctrSlotsInventario.listaElementosRecogidos.Add(listaElementos[i]);
                    PlayerPrefs.SetInt(listaElementos[i].tag + "Recogido", 1);
                    listaElementos.RemoveAt(i);

                    break;
                }
            }
        }

        yield return null;
    }

    IEnumerator MostrarDistanciasEnInterfaz()
    {
        mostrarDistancias = false;

        for (int i = 0; i < listaElementos.Count; i++)
        {
            if (listaElementos[i].GetComponent<ClaseElemento>().nombreElemento == "CuboA")
            {
                txtDistCuboA.text = listaElementos[i].GetComponent<ClaseElemento>().distToPlayer.ToString("n2");
            }
            else if (listaElementos[i].GetComponent<ClaseElemento>().nombreElemento == "PrismaB")
            {
                txtDistPrismaB.text = listaElementos[i].GetComponent<ClaseElemento>().distToPlayer.ToString("n2");
            }
            else if (listaElementos[i].GetComponent<ClaseElemento>().nombreElemento == "CilindroC")
            {
                txtDistCilindroC.text = listaElementos[i].GetComponent<ClaseElemento>().distToPlayer.ToString("n2");
            }

            yield return null;

            mostrarDistancias = true;
        }
    }

    IEnumerator VibrarDispositivo()
    {
        vibrarDispositivo = false;

        for (int i = 0; i < listaElementos.Count; i++)
        {
            if (listaElementos[i].GetComponent<ClaseElemento>().distToPlayer <= 4f)
            {
                Handheld.Vibrate();
            }

            yield return null;
        }

        yield return null;

        vibrarDispositivo = true;
    }
}
