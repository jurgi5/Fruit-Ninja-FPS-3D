using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject crosshair;

    public float shootingRange = 100f; 


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            if (hit.collider.CompareTag("Fruit"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
