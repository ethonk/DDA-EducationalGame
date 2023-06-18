using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : MonoBehaviour
{
    public float velocity = 10.0f;
    public Vector3 directon = Vector3.zero;
    
    //
    // bullet destruction
    
    [SerializeField] private float currDestroyTimer;
    [SerializeField] private float maxDestroyTime = 2.0f;

    private void Update()
    {
        //
        // move
        
        directon.Normalize();
        transform.position += directon * (velocity * Time.deltaTime);
        
        //
        // update
        
        currDestroyTimer += Time.deltaTime;
        if (currDestroyTimer >= maxDestroyTime)
            Destroy(gameObject);
    }
}
