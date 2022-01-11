using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{    
    public GameObject firePoint;
    public GameObject bulletPrefab;

    public float fireRate;
    private float nextShot;      
      
    void Update()
    {
        ShootingTime();        
    }

    void ShootingTime()
    {
        if (Time.time >= nextShot)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                nextShot = Time.time + 1f / fireRate;
            }

        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().lives--;
        }
    }
}
