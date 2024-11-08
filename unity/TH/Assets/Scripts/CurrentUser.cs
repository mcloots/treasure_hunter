using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentUser : MonoBehaviour
{
    public string username;

    // We get the username from Flutter
    void SetUsername(string username)
    {
        this.username = username;
    }
}
