using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script implementa la lógica para que la MainCamera persiga al player de forma continua.
/// </summary>
public class CamaraPerseguidora : MonoBehaviour
{

    public GameObject player;
    public Transform transformCamera;
    private Vector3 offset;

    [Tooltip("Este valor se le resta al eje Y del player para acomodar la cámara a la distancia que se desee en el eje Y.")]
    public float valorModificadorEjeY;

    [Tooltip("Este valor se le resta al eje Z del player para acomodar la cámara a la distancia que se desee en el eje Z.")]
    public float valorModificadorEjeZ;

    void Start()
    {
        StartCoroutine(CargarSegundo());
    }

    void LateUpdate()
    {
        float newXPosition = player.transform.position.x - offset.x;
        float newZPosition = player.transform.position.z + offset.z;

        transformCamera.position = new Vector3(newXPosition, transformCamera.position.y, newZPosition);
    }

    IEnumerator CargarSegundo()
    {
        yield return new WaitForSeconds(1f);
        transformCamera.position = new Vector3(player.transform.position.x, valorModificadorEjeY, player.transform.position.z - valorModificadorEjeZ);
        offset = transformCamera.position - player.transform.position;
    }
}
