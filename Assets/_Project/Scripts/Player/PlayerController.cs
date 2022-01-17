using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;


    public GameObject firePoint;
    public GameObject ballPrefab;
    public GameObject tripleBallPrefab;
    public GameObject fireEffects;
    public GameObject tripleEffects;

    public List<GameObject> triplePoints;

    public float fireRate;
    public float coolDown;
    public float nextShot;

    [SerializeField] private Animator playerAnim;
    [SerializeField] private Status status;  


    public void Initialization()
    {
        
    }

    
    void Start()
    {
        Initialization();        
    }

    void Update()
    {
        StartGame();

        if (GameManager.instance.isGameActive)
        {
            Movement();            
            ShootingTime();
            DamageControl();
        }       
    }

    private void Movement()
    {        
        float horizontalInputs = Input.GetAxisRaw("Horizontal");
        float verticalInputs = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.down * speed * verticalInputs * Time.deltaTime);
        transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed * horizontalInputs);
    }

    private void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.StartGame();
            return;
        }
    }

    void ShootingTime()
    {
        if (Time.time >= nextShot)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                nextShot = Time.time + 1f / fireRate;
            }

            if(Input.GetButtonDown("Fire2"))
            {
                StartCoroutine(AttackSpecial());
                nextShot = Time.time + 1f / coolDown;
            }
        }
    }

    void Shoot()
    {
        Instantiate(ballPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

    IEnumerator AttackSpecial()
    {
        TripleShoot();
        yield return new WaitForSeconds(5.5f);        
        tripleEffects.SetActive(false);
        //StopAllCoroutines();
    }

    void TripleShoot()
    {
        tripleEffects.SetActive(true);
        Instantiate(tripleBallPrefab, triplePoints[0].transform.position, triplePoints[0].transform.rotation);
        Instantiate(tripleBallPrefab, triplePoints[1].transform.position, triplePoints[1].transform.rotation);
        Instantiate(tripleBallPrefab, triplePoints[2].transform.position, triplePoints[2].transform.rotation);      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CannonBall cannonBall))
        {
            status.TakeDamage(1);
        }
    }

    private void DamageControl()
    {
        switch (status.currentHealth)
        {
            case 2:
                playerAnim.SetInteger("Transition", 1);
                break;
            case 1:
                playerAnim.SetInteger("Transition", 2);
                fireEffects.SetActive(true);
                break;
            case 0:
                playerAnim.SetInteger("Transition", 3);
                fireEffects.SetActive(false);
                GameManager.instance.GameOver();
                print("game over");
                break;
        }       
    }

   
}
