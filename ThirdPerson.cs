using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    public Transform target;
    public Vector3 thirdPersonOffset = new Vector3(0, 7, -15);
    public float smoothSpeed = 0.125f;
    public float downwardAngle = 20f;
    public float minZoomDistance = 1f;
    public float maxZoomDistance = 15f;
    public float minHeight = 2f;
    public float maxHeight = 5f;
    public LayerMask collisionMask;

    private Vector3 currentOffset;

    void Start()
    {
        currentOffset = thirdPersonOffset;
    }

    void LateUpdate()
    {
        AdjustCameraForCollision();

        Vector3 desiredPosition = target.position + target.rotation * currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        Quaternion targetRotation = Quaternion.Euler(15f, target.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }


    void AdjustCameraForCollision()
    {
        Vector3 rawDesiredPosition = target.position + target.rotation * thirdPersonOffset;
        Vector3 directionToCamera = (rawDesiredPosition - target.position).normalized;

        RaycastHit hit;

        if (Physics.Raycast(target.position, directionToCamera, out hit, thirdPersonOffset.magnitude, collisionMask))
        {
            float distanceToWall = hit.distance;
            float adjustedDistance = Mathf.Clamp(distanceToWall, minZoomDistance, maxZoomDistance);
            float adjustedHeight = Mathf.Lerp(minHeight, maxHeight, adjustedDistance / maxZoomDistance);
            currentOffset = new Vector3(thirdPersonOffset.x, adjustedHeight, -adjustedDistance);
        }
        else
        {
            currentOffset = Vector3.Lerp(currentOffset, thirdPersonOffset, smoothSpeed);
        }
    }
}
