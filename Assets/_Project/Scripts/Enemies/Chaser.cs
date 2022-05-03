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
            Damage(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            Die(cannonBall.gameObject);
        }

        if (collision.gameObject.TryGetComponent(out TripleBall tripleBall))
        {
            Die(tripleBall.gameObject);
        }
    }

    private void Die(GameObject obj)
    {
        audioSource.PlayOneShot(destroyedSound, 0.2f);
        enemyAnimator.SetInteger("Transition", 1);

        ObjectPooler.Instance.ReturnToPool("CannonBall", obj);

        //Destroy(obj.gameObject);
        Destroy(this.gameObject, 0.5f);
        GameManager.instance.UpdateScore(1);
        GetComponent<Patrol>().speed = 0;
    }

    private void Damage(GameObject obj)
    {
        audioSource.PlayOneShot(destroyedSound, 0.2f);
        enemyAnimator.SetInteger("Transition", 1);
        GetComponent<Status>().TakeDamage(1);
        obj.gameObject.GetComponent<Status>().TakeDamage(1);
        GetComponent<Patrol>().speed = 0;
        Destroy(this.gameObject, 0.4f);
    }
    
}

