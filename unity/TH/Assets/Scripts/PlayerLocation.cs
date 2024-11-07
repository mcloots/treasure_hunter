using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    public float latitude;
    public float longitude;
    public float startLatitude;
    public float startLongitude;
    private bool atStartPosition = true;

    public bool isStartUpFinished = false;
    public int errorCode = 0;
    public float startDirection; // Compass direction when starting the App


    // Call this coroutine to start GPS services
    IEnumerator Start()
    {
        // Check if the user has location services enabled
        if (!Input.location.isEnabledByUser)
        {
            errorCode = 1;
            Debug.Log("Location service is not enabled");
            yield break; // Location service is not enabled on the device
        }

        Input.compass.enabled = true;
        // Start location service
        Input.location.Start();




        // Wait until the service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If it didn't initialize within the timeout, stop
        if (maxWait <= 0)
        {
            errorCode = 2;
            Debug.Log("Timed out");
            yield break;
        }

        // If the connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            errorCode = 3;
            Debug.Log("Unable to determine device location");
            yield break;
        }

        yield return new WaitForSeconds(2); // Extra 2 seconds to initialize correctly
        isStartUpFinished = true;
        startDirection = Input.compass.trueHeading;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running && isStartUpFinished)
        {
            if (atStartPosition)
            {
                startLatitude = Input.location.lastData.latitude;
                startLongitude = Input.location.lastData.longitude;
                atStartPosition = false;
            }

            // Update current location
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }
    }
}