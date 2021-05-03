using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;

    public float smooth = 10.0f;

    Vector3 dollyDir;
    private Vector3 dollyDirAdjusted;

    public float distance;


    // Start is called before the first frame update
    void Awake()
    {
        //normalize the vector
        dollyDir = transform.localPosition.normalized;
        //return the legth of the vector
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        //set the desired camera position
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);

        RaycastHit hit;
        
        //linecast to check for a collision

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            //clamps the camera to stop clipping issues 
            distance = Mathf.Clamp((hit.distance * 0.87f), minDistance, maxDistance);

        }
        else 
        {
            //set the distance to the max distance
            distance = maxDistance;
        }
        //Lerp to a better position
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
