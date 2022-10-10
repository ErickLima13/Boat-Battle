using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public GameObject[] point;

    public float speed;

    public Vector3 targetPosition;

    public int index;

    public float distanceHit;
    public float distanceView;

    public Transform firePoint;

    public LayerMask playerLayer;

    public bool isPlayer;

    private NavMeshAgent agent;

    private void Initialization()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        

        point = GameObject.FindGameObjectsWithTag("Patrol");
        index = Random.Range(0, point.Length);
        print(index);

        targetPosition = point[index].transform.position;
        
    }

    void Start()
    {
        Initialization();
    }

    void Update()
    {
        //if (GameManager.instance.isGameActive)
        //{
           
        //}

        FindPlayer();

    }

    private void FindPlayer()
    {
        agent.speed = speed;

        Collider2D hit = Physics2D.OverlapCircle(firePoint.position, distanceHit,playerLayer);

        Collider2D view = Physics2D.OverlapCircle(transform.position, distanceView,playerLayer);

        if (view != null)
        {
            Vector3 direction = view.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            Quaternion rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);

            agent.SetDestination(view.transform.position);
            speed = 2f;
        }
        else
        {
            RotateTheEnemy();
            MoveToWayPoint();
        }

        if (hit != null)
        {
            speed = 0;
            isPlayer = true;
        }
        else
        {
            speed = 3f;
            isPlayer = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firePoint.position, distanceHit);
        Gizmos.DrawWireSphere(transform.position, distanceView);
    }

    private void Patrolling()
    {
        for(int i = Random.Range(0,point.Length); i < point.Length; i++)
        {   
            //transform.position == point[i].transform.position
            if (Mathf.Approximately((point[i].transform.position - transform.position).sqrMagnitude, 0))
            {
                index = (i + 1) % point.Length;
                index = Random.Range(0, point.Length);

                targetPosition = point[index].transform.position;
                
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void MoveToWayPoint()
    {
        if (point.Length > 0)
        {
            float distance = Vector2.Distance(point[index].transform.position, transform.position);
            agent.destination = point[index].transform.position;

            if (distance <= 1.5f)
            {
                //parte para o proximo ponto
                index = Random.Range(0, point.Length);
                index++;

                if(index >= point.Length)
                {
                    index = Random.Range(0, point.Length);
                }
            }
        }
    }

    private void RotateTheEnemy()
    {
        Vector3 direction = point[index].transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
    }

    private void OnEnable()
    {
        GetComponent<Animator>().SetInteger("Transition", 0);
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<Status>().healthBarObject.SetActive(true);

    }
}
