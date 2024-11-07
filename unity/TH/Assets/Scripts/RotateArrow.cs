using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    public Transform arrow;       // Reference to the arrow (3D model) in the scene

    private PlayerLocation playerLocation;
    private TargetLocation targetLocation;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Player/Target Location component in the scene
        playerLocation = FindObjectOfType<PlayerLocation>();
        targetLocation = FindObjectOfType<TargetLocation>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the player's location data is available
        if (targetLocation != null && playerLocation != null && Input.location.status == LocationServiceStatus.Running)
        {
            Vector3 rotation = GetRotationDirection(playerLocation.latitude, playerLocation.longitude, targetLocation.latitude, targetLocation.longitude);
            arrow.rotation = Quaternion.Euler(rotation);
        }
    }

    public Vector3 GetRotationDirection(float currentLatitude, float currentLongitude, float targetLatitude, float targetLongitude)
    {
        // Convert latitude and longitude from degrees to radians
        float currentLatRad = currentLatitude * Mathf.Deg2Rad;
        float currentLonRad = currentLongitude * Mathf.Deg2Rad;
        float targetLatRad = targetLatitude * Mathf.Deg2Rad;
        float targetLonRad = targetLongitude * Mathf.Deg2Rad;

        // Calculate direction using Haversine formula components
        float deltaLon = targetLonRad - currentLonRad;
        float x = Mathf.Cos(targetLatRad) * Mathf.Sin(deltaLon);
        float y = Mathf.Cos(currentLatRad) * Mathf.Sin(targetLatRad) - Mathf.Sin(currentLatRad) * Mathf.Cos(targetLatRad) * Mathf.Cos(deltaLon);

        // Calculate the angle in radians and convert to degrees
        float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;

        // Adjust the angle with the compass heading to north and then to east
        angle = angle - playerLocation.startDirection + 90;

        // Return the rotation as a Vector3
        return new Vector3(0, angle, 0);
    }

}

