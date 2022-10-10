using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject fireEffects;
    [SerializeField] private GameObject cannon;

    [SerializeField] private float fireRate;
    private float nextShot;

    [SerializeField] private List<AudioClip> sfxSounds;

    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Status status;

    [SerializeField] private Patrol patrol;

    public void Initialization()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        Initialization();
    }

    private void Update()
    {
        DamageControl();

        if (GetComponent<Status>().currentHealth != 0)
        {
            ShootingTime();
        }
       
    }

    private void ShootingTime()
    {
        if (Time.time >= nextShot && patrol.isPlayer && GameManager.instance.isGameActive)
        {
            nextShot = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(sfxSounds[0]);

        ObjectPooler.Instance.SpawnFromPool("CannonBall", firePoint.transform.position, firePoint.transform.rotation);


        //Instantiate(ballPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

    private void DestroyBoat() 
    {
        patrol.speed = 0;
        //Destroy(gameObject, 1.5f);
        StartCoroutine(ObjectPooler.Instance.ReturnToPoolAfterSeconds("Shooter", gameObject, 1f));
        fireEffects.SetActive(false);
        
    }

    public void AddScore()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Status>().healthBarObject.SetActive(false);
        cannon.SetActive(false);
        audioSource.PlayOneShot(sfxSounds[1], 0.2f);
        GameManager.instance.UpdateScore(1);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            
            //Destroy(cannonBall.gameObject);

            ObjectPooler.Instance.ReturnToPool("CannonBall", cannonBall.gameObject);
        }

        if (collision.gameObject.TryGetComponent(out TripleBall tripleBall))
        {
            Destroy(tripleBall.gameObject);
        }
    }

    private void DamageControl()
    {
        switch (status.currentHealth)
        {
            case 2:
                enemyAnimator.SetInteger("Transition", 1);
                break;
            case 1:
                enemyAnimator.SetInteger("Transition", 2);
                fireEffects.SetActive(true);
                break;
            case 0:
                
                enemyAnimator.SetInteger("Transition", 3);
                DestroyBoat();
                break;
        }
    }

    private void OnEnable()
    {
        GetComponent<Patrol>().speed = 3;
        cannon.SetActive(true);
    }
}
