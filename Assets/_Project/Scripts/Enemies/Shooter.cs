using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject bulletPrefab;
    public GameObject fireEffects;    

    public float fireRate;
    private float nextShot;

    private Animator enemyAnim;
    private GameObject gameManager;

    [SerializeField] private Status status;
    [SerializeField] private Movement movement;

    public void Initialization()
    {
        enemyAnim = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager");
    }

    private void Start()
    {
        Initialization();
    }

    void Update()
    {
        if(GetComponent<Status>().currentHealth != 0)
        {
          ShootingTime();
        }
        else
        {
            DestroyBoat();
        }                    
    }

    void ShootingTime()
    {
        if (Time.time >= nextShot && movement.distancePlayer < movement.distance)
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
        GetComponent<Movement>().speed = 0;
        Destroy(gameObject, 1f);
        fireEffects.SetActive(false);        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            GetComponent<Status>().TakeDamage(1);
            Destroy(cannonBall.gameObject);
            DamageControl();
        }
    }

    private void DamageControl()
    {
        switch (status.currentHealth)
        {
            case 2:
                enemyAnim.SetInteger("Transition", 1);
                break;
            case 1:
                enemyAnim.SetInteger("Transition", 2);
                fireEffects.SetActive(true);
                break;
            case 0:
                enemyAnim.SetInteger("Transition", 3);
                GameManager.instance.UpdateScore(1);
                break;
        }     
    }
}
