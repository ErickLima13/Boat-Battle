using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Transform healthBar;
    public GameObject healthBarObject;

    private Vector3 healthBarScale;
    private float healthPercent;


    public void Initialization()
    {
        currentHealth = maxHealth;
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / currentHealth;
    }
   
    void Start()
    {
        Initialization();
    }  

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBarScale.x = healthPercent * currentHealth;
        healthBar.localScale = healthBarScale;        
    }
}
