using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class DragElement : MonoBehaviour
{
    private bool holding;

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
        holding = false;
    }

    void Update()
    {
        // One finger
        if (Touch.activeFingers.Count == 1)
        {
            // Tap on Object
            if (Touch.activeTouches[0].isInProgress)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Touch.activeFingers[0].currentTouch.screenPosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.transform.gameObject == gameObject)
                    {
                        transform.position = new Vector3(Touch.activeTouches[0].screenPosition.x, Touch.activeTouches[0].screenPosition.y, transform.position.z);
                    }
                }
            }

            // Release
            if (Touch.activeTouches[0].ended)
            {
                holding = false;
            }
        }
    }

    /*void Move()
    {
        Ray raycast = Camera.main.ScreenPointToRay(Touch.activeFingers[0].currentTouch.screenPosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(raycast, out raycastHit, 30.0f, LayerMask.GetMask("Elements")))
        {
            transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y, transform.position.z);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        // The GameObject this script attached should be on layer "Surface"
        if (Physics.Raycast(ray, out hit, 30.0f, LayerMask.GetMask("Surface")))
        {
            transform.position = new Vector3(hit.point.x,
                                             transform.position.y,
                                             hit.point.z);
        }
    }*/
}
