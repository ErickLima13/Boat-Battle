using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject bulletPrefab;
    public GameObject fireEffects;

    public int lives;

    public float fireRate;
    private float nextShot;

    private Animator enemyAnim;
    private GameObject gameManager;


    public void Initialization()
    {
        enemyAnim = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager");
    }

    private void Start()
    {
        Initialization();
    }

    void LateUpdate()
    {
        if(lives != 0)
        {
          ShootingTime();
        }              
        DamageControl();
    }

    void ShootingTime()
    {
        if (Time.time >= nextShot && GetComponent<Movement>().direction.magnitude < GetComponent<Movement>().distance)
        {                    
          nextShot = Time.time + 1f / fireRate;
          Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

    public void DestroyBoat()
    {
        gameManager.GetComponent<GameManager>().score++;
        GetComponent<Movement>().speed = 0;
        Destroy(gameObject, 1f);
        fireEffects.SetActive(false);
        print("ponto");
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {            
            lives--;                      
        }
    }

    private void DamageControl()
    {
        if (lives == 2)
        {
            enemyAnim.SetInteger("Transition", 1);
        }

        if (lives == 1)
        {
            enemyAnim.SetInteger("Transition", 2);
            fireEffects.SetActive(true);
        }

        if (lives == 0)
        {
            enemyAnim.SetInteger("Transition", 3);
            DestroyBoat();
        }
    }
}
