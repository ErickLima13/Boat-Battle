using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private AudioSource audioSource;

    public AudioClip destroyedSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            audioSource.PlayOneShot(destroyedSound, 0.2f);
            enemyAnimator.SetInteger("Transition", 1);
            GetComponent<Status>().TakeDamage(1);
            collision.gameObject.GetComponent<Status>().TakeDamage(1);
            GetComponent<Movement>().speed = 0;
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            audioSource.PlayOneShot(destroyedSound, 0.2f);
            enemyAnimator.SetInteger("Transition", 1);
            Destroy(cannonBall.gameObject);
            Destroy(this.gameObject, 2f);
            GameManager.instance.UpdateScore(1);
            GetComponent<Movement>().speed = 0;
        }

        if (collision.gameObject.TryGetComponent(out TripleBall tripleBall))
        {
            audioSource.PlayOneShot(destroyedSound, 0.2f);
            enemyAnimator.SetInteger("Transition", 1);
            Destroy(tripleBall.gameObject);
            Destroy(this.gameObject, 2f);
            GameManager.instance.UpdateScore(1);
            GetComponent<Movement>().speed = 0;
        }
    }
}

