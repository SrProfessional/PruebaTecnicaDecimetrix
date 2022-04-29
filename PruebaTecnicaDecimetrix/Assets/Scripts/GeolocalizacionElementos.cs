using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

/// <summary>
/// Este script le asigna coordenadas latitud  y longitud a los elementos en el mapa y los separa 1,5m en el eje x entre sí.
/// </summary>
public class GeolocalizacionElementos : MonoBehaviour
{
    [Tooltip("Esta es una lista de los elementos modelados en probuilder.")]
    public List<GameObject> elementos;

    //COORDENADAS GEOGRÁFICAS
    [Tooltip("Estas son las coordenadas geográficas, x representa latitud e y representa longitud.")]
    public Vector2d latlon;

    void Start()
    {
        for(int i = 0; i < elementos.Count; i++)
        {
            //CONVERTIR 1,5 METROS EN EL EJE X EN COORDENADAS LAT Y LON
            var metrosACoordenadas = Conversions.MetersToLatLon(new Vector2d(1.5f, 0f));

            //MOVER ELEMENTOS CON RESPECTO A UNAS COORDENADAS Y SUMARLES LOS 1,5 METROS DEL PASO ANTERIOR
            elementos[i].transform.MoveToGeocoordinate(latlon/1000000 + (metrosACoordenadas * i), new Vector2d(0, 0), 1f); 
        }
    }
}
