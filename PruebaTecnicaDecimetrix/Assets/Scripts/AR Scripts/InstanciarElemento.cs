using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InstanciarElemento : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager arPlaneManager;

    [SerializeField]
    private GameObject elemento3D;

    private List<ARPlane> planes = new List<ARPlane>();
    private GameObject elementoPlaced;

    private void OnEnable()
    {
        arPlaneManager.planesChanged += PlanesFound;
    }

    private void OnDisable()
    {
        arPlaneManager.planesChanged -= PlanesFound;
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
                elementoPlaced = Instantiate(elemento3D);
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

        foreach (var plane in planes)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
