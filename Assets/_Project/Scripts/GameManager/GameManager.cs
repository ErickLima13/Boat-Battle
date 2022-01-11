using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    private Animator playerAnim;

    public GameObject fireEffects;

    public void Initialization()
    {
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();   
    }

    // Update is called once per frame
    void Update()
    {
        DamageControl();
    }
    
    private void DamageControl()
    {
        if(lives == 2)
        {
            playerAnim.SetInteger("Transition", 1);
        }

        if(lives == 1)
        {
            playerAnim.SetInteger("Transition", 2);
            fireEffects.SetActive(true);
        }

        if (lives == 0)
        {
            playerAnim.SetInteger("Transition", 3);
            fireEffects.SetActive(false);
        }
    }
}
