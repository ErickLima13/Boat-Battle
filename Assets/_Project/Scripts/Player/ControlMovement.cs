using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontalInputs = Input.GetAxisRaw("Horizontal");
        float verticalInputs = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.down * speed * verticalInputs * Time.deltaTime);
        transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed * horizontalInputs);                
    }

    
}
