using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{

    public Transform target;
    public GameObject targetPlayer;
    private GameObject oldTargetPlayer;
    public GameObject targetItem;

    public float speed = 40f;
    public float nextWaypointDistance = 3f;
    public float timeBetweenJumps = 1f;

    public CharacterController2D controller;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    float timeUntilNextJump = 0f;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set targetPlayer to the closest enemy
        findTargetPlayer();
        if (target == null || oldTargetPlayer.GetComponent<HealthSystem>().isDead)
        {
            oldTargetPlayer = targetPlayer;
            target = targetPlayer.transform;
        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) > 10)
            {
                oldTargetPlayer = targetPlayer;
                target = targetPlayer.transform;
            }
        }


        if (path == null)
            return;

        moveTowardTarget();
    }


    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void moveTowardTarget()
    {
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 normalized = direction * speed * Time.deltaTime;

        timeUntilNextJump -= Time.deltaTime;
        if (normalized.y > 0.8 && timeUntilNextJump <= 0)
        {
            jump = true;
            timeUntilNextJump = timeBetweenJumps;
        }

        if (normalized.x > 0.5)
        {
            horizontalMove = 1 * speed;
        }
        else if (normalized.x < -0.5)
        {
            horizontalMove = -1 * speed;
        }
        else
        {
            horizontalMove = 0;
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void findTargetPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int nearestPlayer = -1;

        for (int i = 0; i < players.Length; i++)
        {
            if ((players[i] != gameObject) && (!players[i].GetComponent<HealthSystem>().isDead)) {
                if (nearestPlayer == -1)
                {
                    nearestPlayer = i;
                }
                else if (Vector3.Distance(transform.position, players[i].transform.position) < Vector3.Distance(transform.position, players[nearestPlayer].transform.position))
                {
                    nearestPlayer = i;
                }
            }
        }


        if (nearestPlayer == -1)
        {
            targetPlayer = null;
        }
        else
        {
            targetPlayer = players[nearestPlayer];
        }
    }
}
