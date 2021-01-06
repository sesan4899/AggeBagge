using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject targetPoint;
    private float startZ;
    public float smoothFactor;

    // Update is called once per frame
    private void Start()
    {
        startZ = transform.position.z;
    }
    void FixedUpdate()
    {
        Vector3 targetPosition = targetPoint.transform.position;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, startZ);
        Vector3 endPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = endPosition;
    }
}
