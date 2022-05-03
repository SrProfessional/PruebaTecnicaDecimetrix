using UnityEngine;
using TMPro;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.CheapRulerCs;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections;

/// <summary>
/// Este script implementa la lógica para la visualización de las distancias de cada elemento en metros en la UI,
/// alerta sobre elementos a menos de 4m de distancia por medio de la vibración del dispositivo móvil y activa la camara al tocarlos.
/// </summary>
public class VisualizacionCoordenadas : MonoBehaviour
{
    //TEXTOS DE LA INTERFAZ DE USUARIO
    public TextMeshProUGUI txtDistCuboA;
    public TextMeshProUGUI txtDistPrismaB;
    public TextMeshProUGUI txtDistCilindroC;

    public Transform transformPlayer;
    public Transform transformCubeA;
    public Transform transformPrismB;
    public Transform transformCylinderC;

    private CheapRuler crPlayer;

    public CtrMenu ctrMenu;
    public GameObject cuboA;
    public GameObject prismaB;
    public GameObject cilindroC;
    public bool activarTouch;

    protected void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    protected void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        //SE DECLARA UN CHEAP RULER DEL PLAYER EN METROS PARA LUEGO PODER CALCULAR LA DISTANCIA CON RESPECTO A LA LATITUD
        crPlayer = new CheapRuler(transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, CheapRulerUnits.Meters);

        activarTouch = true;
    }

    void Update()
    {
        double[] puntoPlayer = { transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };
        double[] puntoCuboA = { transformCubeA.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformCubeA.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };
        double[] puntoPrismaB = { transformPrismB.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformPrismB.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };
        double[] puntoCilindroC = { transformCylinderC.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformCylinderC.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };

        //CALCULA LA DISTANCIAS ENTRE EL PLAYER Y CADA ELEMENTO Y SE MUESTRAN EN LA INTERFAZ DE USUARIO
        double distToCuboA = crPlayer.Distance(puntoPlayer, puntoCuboA);
        double distToPrismaB = crPlayer.Distance(puntoPlayer, puntoPrismaB);
        double distToCilindroC = crPlayer.Distance(puntoPlayer, puntoCilindroC);

        txtDistCuboA.text = distToCuboA.ToString("n2");
        txtDistPrismaB.text = distToPrismaB.ToString("n2");
        txtDistCilindroC.text = distToCilindroC.ToString("n2");

        if (distToCuboA <= 4f)
        {
            Handheld.Vibrate();

            if (activarTouch)
            {
                StartCoroutine(activarCamara());
            }

        }
        else if(distToPrismaB <= 4f)
        {
            Handheld.Vibrate();

            if (activarTouch)
            {
                StartCoroutine(activarCamara());
            }

        }
        else if(distToCilindroC <= 4f)
        {
            Handheld.Vibrate();

            if(activarTouch)
            {
                StartCoroutine(activarCamara());
            }          
        }
    }

    IEnumerator activarCamara()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        if (activeTouches.Count > 0 && activeTouches.Count <= 1)
        {
            Ray raycast = Camera.main.ScreenPointToRay(activeTouches[0].screenPosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.transform.tag == "CuboA" && activarTouch)
                {
                    activarTouch = false;
                    ctrMenu.ActivarCamara();
                    cuboA.SetActive(true);
                    prismaB.SetActive(false);
                    cilindroC.SetActive(false);
                }
                else if(raycastHit.transform.tag == "PrismaB" && activarTouch)
                {
                    activarTouch = false;
                    ctrMenu.ActivarCamara();
                    cuboA.SetActive(false);
                    prismaB.SetActive(true);
                    cilindroC.SetActive(false);
                }
                else if (raycastHit.transform.tag == "CilindroC" && activarTouch)
                {
                    activarTouch = false;
                    ctrMenu.ActivarCamara();
                    cuboA.SetActive(false);
                    prismaB.SetActive(false);
                    cilindroC.SetActive(true);
                }
            }
        }

        yield return null;
    }

    public void tomarCuboA()
    {
        transformCubeA.gameObject.SetActive(false);
    }

    public void tomarPrismaB()
    {
        transformPrismB.gameObject.SetActive(false);
    }

    public void tomarCilindroC()
    {
        transformCylinderC.gameObject.SetActive(false);
    }
}
