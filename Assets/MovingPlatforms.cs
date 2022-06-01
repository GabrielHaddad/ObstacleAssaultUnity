using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    Vector3 startLocation;

    [SerializeField] Vector3 movingVelocity;
    [SerializeField] Vector3 rotationVelocity;
    [SerializeField] float maxMoveDistance;

    [SerializeField] GameObject Player;

    void Start()
    {
        startLocation = transform.position;
    }

    void Update()
    {
        MovePlatform();
        RotatePlatform();
    }

    void MovePlatform()
    {
        if (ShouldPlatformReturn())
        {
            Vector3 moveDirection = movingVelocity.normalized;
            startLocation = startLocation + (moveDirection * maxMoveDistance);
            transform.position = startLocation;
            movingVelocity = -movingVelocity;
        }
        else
        {
            Vector3 currentLocation = transform.position;
            Vector3 newLocation = (movingVelocity * Time.deltaTime) + currentLocation;
            transform.position = newLocation;
        }
    }

    bool ShouldPlatformReturn()
    {
        return GetDistanceMoved() > maxMoveDistance;
    }

    float GetDistanceMoved()
    {
        return Vector3.Distance(startLocation, transform.position);
    }

    void RotatePlatform()
    {
        Vector3 currentRotation = transform.eulerAngles;
        Vector3 newRotation = (rotationVelocity * Time.deltaTime) + currentRotation;
        transform.rotation =  Quaternion.Euler(newRotation);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.parent = null;
        }
    }
}
