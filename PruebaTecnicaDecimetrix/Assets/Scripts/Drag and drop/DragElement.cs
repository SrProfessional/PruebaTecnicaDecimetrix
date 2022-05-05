using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

/// <summary>
/// Este script permite mover los objetos cuando se están tocando.
/// </summary>
public class DragElement : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        if(Touch.activeFingers.Count == 1)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Touch.activeFingers[0].currentTouch.screenPosition);
            RaycastHit raycastHit;

            if(Physics.Raycast(raycast, out raycastHit))
            {
                if(raycastHit.transform.gameObject == gameObject)
                {
                    transform.position = new Vector3(transform.position.x + Touch.activeTouches[0].delta.x * speed, transform.position.y + Touch.activeTouches[0].delta.y * speed,
                    transform.position.z);
                }
            }
            
            /*if (Touch.activeTouches[0].isInProgress)
            {
                transform.position = new Vector3(transform.position.x + Touch.activeTouches[0].delta.x * speed, transform.position.y + Touch.activeTouches[0].delta.y * speed,
                    transform.position.z);
            }*/
        }
    }
}
