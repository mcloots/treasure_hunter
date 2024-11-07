using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUFO : MonoBehaviour
{
    Vector3 movement;
    public int RotateX = 0, RotateY = 90, RotateZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(RotateX, RotateY, RotateZ); //x, y, z
    }

    // Update is called once per frame
    void Update()
    {
        //Per frame called, we execute the Rotate method, so 30 is added to the Y rotation of the game object
        transform.Rotate(movement * Time.deltaTime);
    }
}

