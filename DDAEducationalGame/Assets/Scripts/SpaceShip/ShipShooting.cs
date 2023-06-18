using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private ShipBullet bulletPrefab;

    [Header("Can Shoot")]
    [SerializeField] private float currFireDelay;
    [SerializeField] private float maxFireDelay = 1.0f;
    [SerializeField] private bool canShoot = true;

    private void Update()
    {
        //
        // Check can shoot 
        
        if (canShoot && Input.GetKeyDown(KeyCode.Q))
        {
            canShoot = false;
            
            var newBullet = Instantiate(bulletPrefab);
            newBullet.transform.position = transform.position + (transform.up * 0.3f);
            newBullet.directon = transform.up;
        }
        
        //
        // Process cooldown

        if (canShoot) return;
        
        //  - tick
        currFireDelay += Time.deltaTime;
        //  - check if exceeds
        if (currFireDelay >= maxFireDelay)
        {
            currFireDelay = 0.0f;
            canShoot = true;
        }
    }
}
