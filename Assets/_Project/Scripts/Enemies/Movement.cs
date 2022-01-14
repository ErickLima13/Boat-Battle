using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform player;

    public Vector3 direction;
    public float speed;
    public int distance;
    public float distancePlayer;

    public Transform[] PatrolTargets;
    public float waitTime;
    

    private int random;
    private float time;


    public void Initialization()
    {
        player = GameObject.Find("Player").transform;
        
        random = Random.Range(0, PatrolTargets.Length);
    }
        
    void Start()
    {
        Initialization();   
    }
    
    void Update()
    {
        CalculateDistance();
        
    }

    private void CalculateDistance()
    {      
        distancePlayer = Vector2.Distance(transform.position, player.position);

        if(distancePlayer > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }


        //controla a rotação
        direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
    }    

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, PatrolTargets[random].position, speed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, PatrolTargets[random].position);

        if(distance <= .2f)
        {
            if(time <= 0)
            {
                random = Random.Range(0, PatrolTargets.Length);
                time = waitTime;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }
}
