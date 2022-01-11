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
        if (collision.gameObject.TryGetComponent(out ControlMovement controlmovement))
        {
            enemyAnim.SetInteger("Transition", 1);
            gameManager.GetComponent<GameManager>().lives--;
            GetComponent<Movement>().speed = 0;
            Destroy(this.gameObject, 1f);
            print("Inimigo explodiu");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            enemyAnim.SetInteger("Transition", 1);
            Destroy(this.gameObject, 1f);
            gameManager.GetComponent<GameManager>().score++;
            GetComponent<Movement>().speed = 0;
            print("destruiu inimigo");
        }
    }
}
