using UnityEngine;
using TMPro;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.CheapRulerCs;

/// <summary>
/// Este script implementa la lógica para la visualización de las distancias de cada elemento en metros en la UI y
/// alerta sobre elementos a menos de 4m de distancia por medio de la vibración del dispositivo móvil.
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

    void Start()
    {
        //SE DECLARA UN CHEAP RULER DEL PLAYER EN METROS PARA LUEGO PODER CALCULAR LA DISTANCIA CON RESPECTO A LA LATITUD
        crPlayer = new CheapRuler(transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, CheapRulerUnits.Meters);
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
            //bCamara.interactable = true;
        }
        else if(distToPrismaB <= 4f)
        {
            Handheld.Vibrate();
        }
        else if(distToCilindroC <= 4f)
        {
            Handheld.Vibrate();
        }
    }

    /*public void tomarCuboA()
    {
        ctrMenu.ActivarCamara();
    }

    public void tomarPrismaB()
    {
        ctrMenu.ActivarCamara();
    }

    public void tomarCilindroC()
    {
        ctrMenu.ActivarCamara();
    }*/
}
