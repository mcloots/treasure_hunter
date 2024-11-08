using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class TargetLocation : MonoBehaviour
{
    class Position
    {
        public float latitude;
        public float longitude;

        public Position(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }

    [System.Serializable]
    public class Location
    {
        public int id;
        public string name;
        public string modelName;
        public float latitude;
        public float longitude;
    }

    private Dictionary<string, Position> targets = new Dictionary<string, Position>();
    public float latitude;
    public float longitude;

    public string targetName;
    public int locationId;

    // Start is called before the first frame update
    void Start()
    {
        // 
        // targets.Add("arktos", new Position(51.177985f, 4.985418f));
        // targets.Add("herman", new Position(51.177787f, 4.983353f));

        // targets.Add("cafecampus", new Position(51.164076f, 4.961670f));
        // targets.Add("kerksintamands", new Position(51.161812f, 4.990378f));
        // targets.Add("axion", new Position(51.162210f, 4.957257f));
        // targets.Add("kiefhoeve", new Position(51.155207f, 4.961499f));
        // targets.Add("mystery", new Position(51.16322f, 4.961238f));

        // var position = targets["mystery"];
        // latitude = position.latitude;
        // longitude = position.longitude;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // We get the target location from Flutter
    void SetTargetLocation(string targetLocationJsonString)
    {
        // Decode the JSON string into a Location object
        Location location = JsonConvert.DeserializeObject<Location>(targetLocationJsonString);

        locationId = location.id;
        latitude = location.latitude;
        longitude = location.longitude;
        targetName = location.name;
    }
}
