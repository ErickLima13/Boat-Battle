using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBall : MonoBehaviour
{
    public float speed;   

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Movement movement))
        {
            collision.GetComponent<Status>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
