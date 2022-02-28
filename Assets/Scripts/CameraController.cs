using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTarget;
    private Vector3 targetPosition;
    public float lerpSpeed;
    void Start()
    {
        transform.parent = null;
        transform.position = new Vector3(cameraTarget.position.x, cameraTarget.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        targetPosition = new Vector3(cameraTarget.position.x, cameraTarget.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.fixedDeltaTime);
    }
}
