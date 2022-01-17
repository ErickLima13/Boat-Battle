using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform player;

    public Vector3 direction;

    public float speed;
    public float distancePlayer;
    public int distance;     
    
    public void Initialization()
    {
        player = GameObject.Find("Player").transform;       
    }
        
    void Start()
    {
        Initialization();   
    }
    
    void Update()
    {
        FindThePlayer();
        RotateTheEnemy();
        
    }

    private void FindThePlayer()
    {      
        distancePlayer = Vector2.Distance(transform.position, player.position);

        if(distancePlayer > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }       

    }    

    private void RotateTheEnemy()
    {       
        direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
    }

    private void Patrol()
    {
        

    }
}
