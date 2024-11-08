using System.Collections;
using FlutterUnityIntegration;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CheckinHandler : MonoBehaviour
{
    public Button checkinButton; // Reference to your button
    private string apiUrl = "https://geolocation-api-kgkn.onrender.com/checkins"; // Replace with your API URL

    void Start()
    {
        // Ensure the button is set up to call the method when clicked
        checkinButton.onClick.AddListener(SendPostRequest);
    }

    void SendPostRequest()
    {
        StartCoroutine(PostDataCoroutine());
    }

    private IEnumerator PostDataCoroutine()
    {
        TargetLocation targetLocation = FindObjectOfType<TargetLocation>();
        CurrentUser currentUser = FindObjectOfType<CurrentUser>();

        // Create an instance of PostData with the required values
        PostData postData = new PostData(targetLocation.locationId, currentUser.username);

        // Convert the object to JSON
        string jsonData = JsonUtility.ToJson(postData);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

             if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
                // Send error message to Flutter
                UnityMessageManager.Instance.SendMessageToFlutter("Error: " + request.error);
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                // Send success message to Flutter
                UnityMessageManager.Instance.SendMessageToFlutter("Checkin saved successful: " + request.downloadHandler.text);
            }
        }
    }

    [System.Serializable]
    public class PostData
    {
        public int locationId;
        public string username;

        public PostData(int locationId, string username)
        {
            this.locationId = locationId;
            this.username = username;
        }
    }

}
