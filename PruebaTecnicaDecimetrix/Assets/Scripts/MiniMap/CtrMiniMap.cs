using UnityEngine;

public class CtrMiniMap : MonoBehaviour
{
    public Transform transformPlayer;

    private void LateUpdate()
    {
        Vector3 newPosition = transformPlayer.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
