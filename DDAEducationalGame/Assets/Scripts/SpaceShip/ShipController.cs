using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //
    // Ship Movement

    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float rotationRate = 30.0f;

    //
    // Ship States
    
    //
    // ** CORE FUNCTIONS ** //

    private void Awake()
    {
        // throw new NotImplementedException();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * (moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.up * (moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, rotationRate * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, -rotationRate * Time.deltaTime));
        }
    }
}
