using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTeleportinator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Shippa")) return;

        col.transform.position = new Vector3(0, 0, 0);
    }
}
