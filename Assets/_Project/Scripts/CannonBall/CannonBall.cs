using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;

    public void Initialization()
    {
        Destroy(gameObject, 2f);
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
        Destroy(gameObject);
    }
}
