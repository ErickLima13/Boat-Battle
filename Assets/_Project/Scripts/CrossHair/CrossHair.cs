using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public List<Color> colors;

    public float radius;

    public LayerMask enemies;

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius,enemies);

        if (hit)
        {
            GetComponent<SpriteRenderer>().color = colors[1];
        }
        else
        {
            GetComponent<SpriteRenderer>().color = colors[0];
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
