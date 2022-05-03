using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject fireEffects;

    [SerializeField] private float fireRate;
    private float nextShot;

    public List<AudioClip> sfxSounds;

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

    void Update()
    {
        if (GetComponent<Status>().currentHealth != 0)
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
        if (Time.time >= nextShot && patrol.isPlayer && GameManager.instance.isGameActive)
        {
            nextShot = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(sfxSounds[0], 0.2f);
        Instantiate(ballPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

    public void DestroyBoat()
    {
        patrol.speed = 0;
        Destroy(gameObject, 0.5f);
        fireEffects.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            DamageControl();
            Destroy(cannonBall.gameObject);
        }

        if (collision.gameObject.TryGetComponent(out TripleBall tripleBall))
        {
            DamageControl();
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
                audioSource.PlayOneShot(sfxSounds[1], 0.2f);
                enemyAnimator.SetInteger("Transition", 3);
                GameManager.instance.UpdateScore(1);
                break;
        }
    }
}
