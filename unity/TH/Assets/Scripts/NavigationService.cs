using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationService : MonoBehaviour
{
    private PlayerLocation playerLocation;
    private TargetLocation targetLocation;
    public Text myText;
    public GameObject arrow;
    public GameObject ufo;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Player/Target Location component in the scene
        playerLocation = FindObjectOfType<PlayerLocation>();

        targetLocation = FindObjectOfType<TargetLocation>();

        arrow.SetActive(false);
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetLocation != null && playerLocation != null && myText != null)
        {
            var distance = Distance(playerLocation.latitude, playerLocation.longitude,
    targetLocation.latitude, targetLocation.longitude);

            if (playerLocation.isStartUpFinished)
            {
                myText.text = "Lat (p): " + playerLocation.latitude
                    + "\nLong (p): " + playerLocation.longitude
                    + "\nLat (t): " + targetLocation.latitude
                    + "\nLong (t): " + targetLocation.longitude
                    + "\nStartrichting: " + playerLocation.startDirection
                    + "\nM: " + Math.Round(distance, 3);

                if (distance > 10)
                {
                    arrow.SetActive(true);
                    ufo.SetActive(false);
                }
                else
                {
                    arrow.SetActive(false);
                    RepositionUFOtoCurrentPosition();
                    ufo.SetActive(true);
                }
            }
            else
            {
                if (playerLocation.errorCode != 0)
                {
                    myText.text = "Errorcode: " + playerLocation.errorCode;
                }
                else
                {
                    myText.text = "Starting Location Service...\n\nPlease wait";
                }

            }
        }
    }

    // Function to calculate distance in meters between two points
    private double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        // Radius of the Earth in meters
        const double R = 6371000;

        // Convert degrees to radians
        double lat1Rad = ToRadians(lat1);
        double lon1Rad = ToRadians(lon1);
        double lat2Rad = ToRadians(lat2);
        double lon2Rad = ToRadians(lon2);

        // Haversine formula
        double deltaLat = lat2Rad - lat1Rad;
        double deltaLon = lon2Rad - lon1Rad;

        double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                   Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        // Distance in meters
        double distance = R * c;

        return distance;
    }

    // Function to convert degrees to radians
    private double ToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    private void RepositionUFOtoCurrentPosition()
    {
        var latitude = playerLocation.latitude - playerLocation.startLatitude;
        var longitude = playerLocation.longitude - playerLocation.startLongitude;

        ufo.transform.position = LatLongToUnity(latitude, longitude);
    }

    private Vector3 LatLongToUnity(float latitude, float longitude)
    {
        const float metersPerDegree = 111320f;

        // Calculate the offsets from the reference point
        float latDistance = latitude * metersPerDegree;
        float lonDistance = longitude * metersPerDegree;

        // Convert to Unity's X and Z coordinates (Y is usually height)
        var vector = new Vector3(lonDistance, 0, latDistance);
        // Rotate over the same angle (cfr. camera direction at start)
        var angle = -playerLocation.startDirection;
        return Quaternion.AngleAxis(angle, Vector3.up) * vector;
    }


}