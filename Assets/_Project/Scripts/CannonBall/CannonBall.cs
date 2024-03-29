using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;        
    public void Initialization()
    {
        //Destroy(gameObject, 1.5f);
    }

    void Start()
    {
        Initialization();        
    }

    private void OnEnable()
    {
        StartCoroutine(ObjectPooler.Instance.ReturnToPoolAfterSeconds("CannonBall", gameObject, 1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        ShootBall();
    }

    private void ShootBall()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerController))
        {
            //Destroy(gameObject);
            ObjectPooler.Instance.ReturnToPool("CannonBall", gameObject);
            collision.GetComponent<Status>().TakeDamage(1);

        }

        if(collision.gameObject.TryGetComponent(out Patrol patrol))
        {
            ObjectPooler.Instance.ReturnToPool("CannonBall", gameObject);
            collision.GetComponent<Status>().TakeDamage(1);
            //Destroy(gameObject);
        }
    }

}
