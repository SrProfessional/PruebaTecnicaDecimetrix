using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MostrarElementosAR : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager arPlaneManager;

    private List<ARPlane> planes = new List<ARPlane>();
    public GameObject elementoPlaced;

    public GameObject arCamera;
    public GameObject bActivarCamara;
    public GameObject bCerrarCamara;

    public void AgregarElemento()
    {
        arPlaneManager.planesChanged += PlanesFound;
        arCamera.SetActive(true);
        bActivarCamara.SetActive(false);
        bCerrarCamara.SetActive(true);
    }

    private void PlanesFound(ARPlanesChangedEventArgs planeData)
    {
        if (planeData.added != null && planeData.added.Count > 0)
        {
            planes.AddRange(planeData.added);
        }

        foreach (var plane in planes)
        {
            if (plane.extents.x * plane.extents.y > 0.4f && elementoPlaced == null)
            {
                float yOffset = elementoPlaced.transform.localScale.y / 2f;
                elementoPlaced.transform.position = new Vector3(plane.center.x, plane.center.y + yOffset, plane.center.z);
                elementoPlaced.transform.forward = plane.normal;
                StopPlaneDetection();
            }
        }
    }

    public void StopPlaneDetection()
    {
        arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;

        foreach(var plane in planes)
        {
            plane.gameObject.SetActive(false);
        }
    } 
}
