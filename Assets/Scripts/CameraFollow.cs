using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 1f;
    public Vector3 currentVelocity = Vector3.zero;

    public float verticalSafeZone = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return; // Exit the function if player does not exist
        }


        Vector3 cameraPosition = transform.position;
        Vector3 playerPosition = player.position;

        // cameraPosition.x = player.position.x;

        playerPosition.z = cameraPosition.z;

        Vector3 distance = cameraPosition - playerPosition;

        /*        if(distance.magnitude > 7.0f)
                {
                    cameraPosition = Vector3.SmoothDamp(cameraPosition, playerPosition, ref currentVelocity, smoothTime);
                }
                else
                {
                    currentVelocity = Vector3.zero;
                }
        */

        if (player.position.x > cameraPosition.x)
        {
            cameraPosition.x = Mathf.SmoothDamp(cameraPosition.x, player.position.x, ref currentVelocity.x, smoothTime);

        }

        float verticalDelta = player.position.y - cameraPosition.y;
        float adjustSmoothTime = smoothTime;

        // Speed up the camera's vertical follow if the player is falling quickly
        if (verticalDelta < 0)
        {
            adjustSmoothTime *= 0.01f;
        }

        if (Mathf.Abs(verticalDelta) > verticalSafeZone)
        {
            // Apply smooth vertical movement only when the player is outside the safe zone
            cameraPosition.y = Mathf.SmoothDamp(cameraPosition.y,
                player.position.y - Mathf.Sign(verticalDelta) * verticalSafeZone, ref currentVelocity.y, adjustSmoothTime);
        }



        transform.position = cameraPosition;
    }
}
// 53-353 F23 W7