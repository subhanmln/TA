using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    private float AxisX;
    private float AxisY;

    public float distance = 25f;
    public float speed = 5f;

	// Use this for initialization
	void Start () {
        SetCameraPosition();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            SetCameraPosition();
        }
	}

    void SetCameraPosition()
    {
        // check level of rotation
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        AxisX += deltaY * speed;
        AxisY += deltaX * speed;
        /*
        Debug.Log(AxisX);
        */
        if(AxisX > 0)
        {
            AxisX = 0;
        }
        if(AxisX < -85f)
        {
            AxisX = -85f;
        }
        /* simpplified version of the above method for AxisX
         
        Mathf.Clamp(AxisX, -85f, 0f);
        */
        var rotation = Quaternion.Euler(AxisX, AxisY, 0);

        transform.position = rotation * Vector3.forward * distance;

        // make sure camera is watching given place
        transform.LookAt(Vector3.up * 5f);
    }
}
