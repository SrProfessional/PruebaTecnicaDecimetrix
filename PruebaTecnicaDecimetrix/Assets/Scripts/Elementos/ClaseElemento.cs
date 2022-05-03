using Mapbox.CheapRulerCs;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

/// <summary>
/// Este script crea la clase elemento para poder llamar sus valores desde cualquier lista de elementos y calcula la distancia actual entre ese elemento y el player.
/// </summary>
public class ClaseElemento : MonoBehaviour
{
    public string nombreElemento;
    public double distToPlayer;
    
    public ClaseElemento()
    {

    }

    public ClaseElemento(string nombreElemento, double distToPlayer)
    {
        this.nombreElemento = nombreElemento;
        this.distToPlayer = distToPlayer;
    }

    public string NombreElemento { get; set; }
    public double DistToPlayer { get; set; }


    //SE IMPLEMENTA EL CÓDIGO PARA CALCULAR LA DISTANCIA ENTRE EL ELEMENTO Y EL PLAYER
    public Transform transformPlayer;
    public Transform transformElemento;
    public Sprite imgElemento;

    private CheapRuler crPlayer;

    private void Start()
    {
        //SE DECLARA UN CHEAP RULER DEL PLAYER EN METROS PARA LUEGO PODER CALCULAR LA DISTANCIA CON RESPECTO A LA LATITUD
        crPlayer = new CheapRuler(transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, CheapRulerUnits.Meters);
    }

    private void Update()
    {
        double[] puntoPlayer = { transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };
        double[] puntoCuboA = { transformElemento.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).x, transformElemento.GetGeoPosition(new Vector2d(0f, 0f), 0.15f).y };
        distToPlayer = crPlayer.Distance(puntoPlayer, puntoCuboA);
    }
}
