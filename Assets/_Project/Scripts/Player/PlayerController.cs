using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float fireRate;

    private float nextShot;

    public GameObject firePoint;
    public GameObject ballPrefab;
    public GameObject tripleBallPrefab;
    public GameObject fireEffects;
    public GameObject tripleEffects;
    public GameObject crossHair;
    public List<GameObject> triplePoints;
    

    public List<AudioClip> sfxSounds;
    
    public bool isLoadingTripleCannon = true;    

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private Status status;

    [SerializeField] private AudioSource AudioSource;

    private void Start()
    {
        
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

        //Movement();
    }

    private void Movement()
    {        
        float horizontalInputs = Input.GetAxisRaw("Horizontal");
        float verticalInputs = Input.GetAxisRaw("Vertical");

        transform.Translate(speed * Time.deltaTime * verticalInputs * Vector3.up,Space.World);
        transform.Translate(speed * Time.deltaTime * horizontalInputs * Vector3.right,Space.World);

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos );

        crossHair.transform.position = new(mousePos.x, mousePos.y, transform.position.z);
        Vector2 direction = new(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;

    }

    private void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.StartGame();
            Cursor.visible = false;
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
                AudioSource.PlayOneShot(sfxSounds[0]);
            }
        }

        if (Input.GetButtonDown("Fire2") && isLoadingTripleCannon)
        {
            StartCoroutine(AttackSpecial());
            AudioSource.PlayOneShot(sfxSounds[1]);
        }
    }

    void Shoot()
    {
        ObjectPooler.Instance.SpawnFromPoolWithReturn("CannonBall", firePoint.transform.position, firePoint.transform.rotation);
        //Instantiate(ballPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

    IEnumerator AttackSpecial()
    {
        TripleShoot();
        isLoadingTripleCannon = false;
        yield return new WaitForSeconds(5.5f);        
        tripleEffects.SetActive(false);
        isLoadingTripleCannon = true;
        StopAllCoroutines();
    }

    void TripleShoot()
    {
        tripleEffects.SetActive(true);       
        
        for(int count = 0; count < 3; count++)
        {
            Instantiate(
                tripleBallPrefab,
                triplePoints[count].transform.position,
                triplePoints[count].transform.rotation);
        }  
    }  

    private void DamageControl()
    {
        switch (status.currentHealth)
        {
            case 2:
                playerAnimator.SetInteger("Transition", 1);
                break;
            case 1:
                playerAnimator.SetInteger("Transition", 2);
                fireEffects.SetActive(true);
                break;
            case 0:
                AudioSource.PlayOneShot(sfxSounds[2],0.2f);
                playerAnimator.SetInteger("Transition", 3);
                fireEffects.SetActive(false);
                GameManager.instance.GameOver();                
                break;
        }       
    } 
}
