using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

/// <summary>
/// Este script le asigna coordenadas latitud  y longitud a los elementos en el mapa para ubicarlos a 5 metros negativos en el eje x de las coordenadas iniciales del player
/// y los separa 1,5m en el eje z entre sí.
/// </summary>
public class GeolocalizacionElementos : MonoBehaviour
{
    public Transform transformPlayer;

    [Tooltip("Esta es una lista de los elementos modelados en probuilder.")]
    public List<GameObject> elementos;

    //COORDENADAS GEOGRÁFICAS
    //[Tooltip("Estas son las coordenadas geográficas, x representa latitud e y representa longitud.")]
    //public Vector2d latlon;

    [SerializeField]
    private Vector2d coordenadasActualesPlayer;

    public GameObject PCargando;

    void Start()
    {
        StartCoroutine(MoverElementosACoordenadas());
    }

    IEnumerator MoverElementosACoordenadas()
    {
        //AVISA QUE ESTÁ CARGANDO MIENTRAS OBTIENE LA UBICACIÓN INICIALMENTE
        PCargando.SetActive(true);

        //CONVERTIR 2 METROS EN EL EJE Z EN COORDENADAS LAT Y LON
        var distanciaSeparacion = Conversions.MetersToLatLon(new Vector2d(0f, 2f));

        //CONVERTIR -5 METROS EN EL EJE X DE DISTANCIA EXTRA PARA UBICAR LOS ELEMENTOS
        var distanciaExtraACoordenadas = Conversions.MetersToLatLon(new Vector2d(-5f, 0f));

        yield return new WaitForSeconds(5f);

        //TERMINA DE CARGAR
        PCargando.SetActive(false);

        //SE TOMAN LAS COORDENADAS DEL PLAYER LUEGO DE QUE EL GPS OBTENGA LA UBICACIÓN
        coordenadasActualesPlayer = transformPlayer.GetGeoPosition(new Vector2d(0f, 0f), 0.15f);

        for (int i = 0; i < elementos.Count; i++)
        {
            //MOVER ELEMENTOS CON RESPECTO A UNAS COORDENADAS Y SUMARLES LOS 1,5 METROS DEL PASO ANTERIOR
            elementos[i].transform.MoveToGeocoordinate(coordenadasActualesPlayer + (distanciaExtraACoordenadas + distanciaSeparacion * i), new Vector2d(0f, 0f), 0.15f);
        }
    }
}
