using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TomarElemento : ARBaseGestureInteractable
{
    /*[SerializeField]
    private GameObject placementPrefab;

    [SerializeField]
    private ARObjectPlacementEvent onObjectPlaced;

    private GameObject placementObject;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private static GameObject trackablesObject;

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if(gesture.targetObject == null)
        {
            return true;
        }
        return false;
    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        if(gesture.WasCancelled)
        {
            return;
        }

        if(gesture.targetObject != null)
        {
            return;
        }

        if(GestureTransformationUtility.Raycast(gesture.startPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hit = hits[0];

            if(Vector3.Dot(Camera.main.transform.position = hit.pose.position, hit.pose.rotation * Vector3.up) > 0)
            {
                return;
            }

            //INSTANCIA UN NUEVO OBJETO
            if(placementObject == null)
            {
                placementObject = Instantiate(placementPrefab, hit.pose.position, hit.pose.rotation);

                var anchorObject = new GameObject("PlacementAnchor");
                anchorObject.transform.position = hit.pose.position;
                anchorObject.transform.rotation = hit.pose.rotation;

                if(trackablesObject == null)
                {
                    trackablesObject = GameObject.Find("Trackables");
                }

                if(trackablesObject != null)
                {
                    anchorObject.transform.parent = trackablesObject.transform;
                }

                //onObjectPlaced?.Invoke(this, placementObject);
            }
        }
    }*/








    /*public int tiempoEspera;
    private int tiempoActual;

    void Start()
    {
        tiempoActual = 0;
    }*/

    /*void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Handheld.Vibrate();
        }
    }*/

    /*IEnumerator esperar()
    {
        for(int i = 0; i < tiempoEspera; i++)
        {
            if(tiempoActual <= tiempoEspera)
            {
                if (Input.GetTouch(0).tapCount == 2)
                {
                    //Double tap
                }
            }
            else
            {
                if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {

                }
            }
        }
        while(tiempoActual <= tiempoEspera)
        {
            yield return new WaitForSeconds(1f);


        }
    }*/
}
