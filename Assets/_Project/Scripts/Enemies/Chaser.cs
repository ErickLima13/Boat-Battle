using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    private Animator enemyAnim;
    private GameObject gameManager;
    
    public void Initialization()
    {
        enemyAnim = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager");
    }
    
    void Start()
    {
        Initialization();
    }           
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            enemyAnim.SetInteger("Transition", 1);
            GetComponent<Status>().TakeDamage(1);
            collision.gameObject.GetComponent<Status>().TakeDamage(1);
            GetComponent<Movement>().speed = 0;
            Destroy(gameObject, 1f);          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            enemyAnim.SetInteger("Transition", 1);            
            Destroy(cannonBall.gameObject);
            Destroy(this.gameObject, 1f);
            GameManager.instance.UpdateScore(1);
            GetComponent<Movement>().speed = 0;            
        }

        if(collision.gameObject.TryGetComponent(out TripleBall tripleBall))
        {
            enemyAnim.SetInteger("Transition", 1);
            Destroy(tripleBall.gameObject);
            Destroy(this.gameObject, 1f);
            GameManager.instance.UpdateScore(1);
            GetComponent<Movement>().speed = 0;
        }
    }
}
