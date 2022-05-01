using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.CheapRulerCs;

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

        double[] puntoPlayer = { transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y};
        double[] puntoCuboA = { transformCubeA.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformCubeA.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y};
        double[] puntoPrismaB = { transformPrismB.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformPrismB.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y};
        double[] puntoCilindroC = { transformCylinderC.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformCylinderC.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y};

        //CALCULA LA DISTANCIAS ENTRE EL PLAYER Y CADA ELEMENTO Y SE MUESTRAN EN LA INTERFAZ DE USUARIO
        txtDistCuboA.text = crPlayer.Distance(puntoPlayer, puntoCuboA).ToString();
        txtDistPrismaB.text = crPlayer.Distance(puntoPlayer, puntoPrismaB).ToString();
        txtDistCilindroC.text = crPlayer.Distance(puntoPlayer, puntoCilindroC).ToString();
    }

    void Update()
    { 
        double[] puntoPlayer = { transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };
        double[] puntoCuboA = { transformCubeA.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformCubeA.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };
        double[] puntoPrismaB = { transformPrismB.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformPrismB.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };
        double[] puntoCilindroC = { transformCylinderC.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformCylinderC.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };

        //CALCULA LA DISTANCIAS ENTRE EL PLAYER Y CADA ELEMENTO Y SE MUESTRAN EN LA INTERFAZ DE USUARIO
        txtDistCuboA.text = crPlayer.Distance(puntoPlayer, puntoCuboA).ToString("n2");
        txtDistPrismaB.text = crPlayer.Distance(puntoPlayer, puntoPrismaB).ToString("n2");
        txtDistCilindroC.text = crPlayer.Distance(puntoPlayer, puntoCilindroC).ToString("n2");
    }
}
