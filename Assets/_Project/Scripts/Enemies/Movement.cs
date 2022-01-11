using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform player;

    public Vector3 direction;
    public float speed;
    public int distance;

    public void Initialization()
    {
        player = GameObject.Find("Player").transform;
    }
        
    void Start()
    {
        Initialization();   
    }
    
    void LateUpdate()
    {
        CalculateDistance();
    }

    private void CalculateDistance()
    {
        direction = player.position - transform.position;        
        Debug.DrawRay(transform.position, direction, Color.blue);             

        if(direction.magnitude > distance)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        
        Quaternion rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);        
    }    
}
