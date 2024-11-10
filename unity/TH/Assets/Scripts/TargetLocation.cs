using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class TargetLocation : MonoBehaviour
{
    public Location location;
    // Reference to the GameObject that has the ColorChanger script
    public GameObject arrowObject;

    private ColorChanger colorChanger;

    [System.Serializable]
    public class Location
    {
        public int id;
        public string name;
        public string texture;
        public float latitude;
        public float longitude;
    }

    // Start is called before the first frame update
    void Start()
    {
         // Get the ColorChanger component from the target object
        colorChanger = arrowObject.GetComponent<ColorChanger>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // We get the target location from Flutter
    void SetTargetLocation(string targetLocationJsonString)
    {
        // Decode the JSON string into a Location object
        location = JsonConvert.DeserializeObject<Location>(targetLocationJsonString);

        // Change color of arrow material
         if (colorChanger != null)
        {
            // Call the ChangeColor method and pass the color name (this will change color for arrow & ufo!)
            colorChanger.ChangeColor(location.texture);
        }

    }
}
