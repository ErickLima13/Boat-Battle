using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;        
    public void Initialization()
    {
        
        Destroy(gameObject, 1.5f);
    }

    void Start()
    {
        Initialization();        
    }

    // Update is called once per frame
    void Update()
    {
        ShootBall();
    }

    private void ShootBall()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            Destroy(gameObject);
            collision.GetComponent<Status>().TakeDamage(1);

        }

        if(collision.gameObject.TryGetComponent(out Movement movement))
        {
            collision.GetComponent<Status>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

}
