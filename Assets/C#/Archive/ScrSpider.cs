using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrSpider : MonoBehaviour
{
    //this controls the behavior of the spider
    //The spider would patralling around and change behavior whenever the player is in range of attack
    public int damageAmount = 1;
    public Vector3[] patrolPositions;
    public float movingSpeed = 1f;
    public float attackingSpeed = 4f;
    public float idlingTime = 0.4f;
    public float attackWait = 0.4f;
    public float patrolWait = 5f;
    //public Vector2[] maxMoveRange;2
    public float chasingRange = 5f; //the range where the spider starts to chase.
    public float attackingRange = 2.5f;

    private Transform player;
    private Rigidbody2D myRigid;
    private bool onGround = true;

    public bool playerNearby
    {
        set; get;
    }

    private enum SpiderState
    {
        idling,
        patrolling,
        attacking,
        chasing
    }
    private SpiderState state;
    private int currentPatrolPoint = 0; //where is this spider patrolling to at this moment?

    private float errorRange = 0.03f;

    Coroutine spiderBehavior;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 1; i < patrolPositions.Length; i++) {
            Gizmos.DrawLine(patrolPositions[i - 1], patrolPositions[i]);
        }

        Color gizmoColor = Gizmos.color;
        gizmoColor.a = 0.3f;
        Gizmos.color = gizmoColor;

        Gizmos.DrawWireSphere(transform.position, chasingRange);

        gizmoColor.a = 0.6f;
        Gizmos.color = gizmoColor;

        Gizmos.DrawWireSphere(transform.position, attackingRange);
    }

    void OnEnable()
    {
        string eventUpdateRoute = transform.parent.GetInstanceID() + "UpdateRoute";
        EventManager.StartListening(eventUpdateRoute, UpdateRoutes);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        spiderBehavior = StartCoroutine(SpiderBehavior());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpiderBehavior()
    {
        while (true) {
            switch (state) {
                case SpiderState.patrolling:
                    yield return StartCoroutine(Patrolling());
                    break;
                case SpiderState.chasing:
                    yield return StartCoroutine(ChasingPlayer());
                    break;
                case SpiderState.attacking:
                    yield return StartCoroutine(Attacking());
                    break;
                case SpiderState.idling:
                    yield return StartCoroutine(Idling());
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }

    IEnumerator Idling()
    {
        myRigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(idlingTime);
        state = SpiderState.patrolling;
    }

    IEnumerator Attacking()
    {
        myRigid.velocity = Vector2.zero;
        Vector2 attackPosition = (Vector2)player.position + Vector2.up;
        yield return new WaitForSeconds(attackWait);
        MoveTowards(attackPosition, attackingSpeed);

        yield return new WaitForSeconds(0.1f); //give it some time to leave the groundd
        yield return new WaitUntil(() => onGround);
        state = SpiderState.idling;
    }

    IEnumerator ChasingPlayer()
    {
        if (!playerNearby) {
            state = SpiderState.patrolling;
            yield break;
        }
        if (Vector2.Distance(player.position, transform.position) <= attackingRange) {
            //if in attacking range, start attackingg
            state = SpiderState.attacking;
            Debug.Log("launching Attack");
        }
        Vector2 chasingPosition = player.position;
        chasingPosition.y = transform.position.y;
        MoveTowards(chasingPosition, movingSpeed * 1.5f);
        yield return null;
    }

    IEnumerator Patrolling()
    {
        if (playerNearby && Vector2.Distance((Vector2)player.position, (Vector2)transform.position) <= chasingRange) {
            //if it sees the player, start chasing
            Debug.Log("start chasing");
            state = SpiderState.chasing;
            yield break;
        }
        //check if the current position is close enough to the target position, swith to next position if close
        if (InPosition(transform.position, patrolPositions[currentPatrolPoint])) {
            myRigid.velocity = Vector2.zero;
            for (int i = 0; i < 20; i++) {
                yield return new WaitForSeconds(patrolWait / 20);
                if (playerNearby && Vector2.Distance((Vector2)player.position, (Vector2)transform.position) <= chasingRange) {
                    Debug.Log("start chasing");
                    state = SpiderState.chasing;
                    yield break;
                };
            }
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPositions.Length;
        }

        MoveTowards(patrolPositions[currentPatrolPoint], movingSpeed);

        yield return null;
    }

    private void MoveTowards(Vector2 destination, float speed)
    {
        Vector2 newVelocity = destination - (Vector2)transform.position;
        newVelocity.Normalize();
        myRigid.velocity = newVelocity * speed;
    }

    //check if the spider is in position regarding to the patrolling route
    private bool InPosition(Vector2 a, Vector2 b)
    {
        return (a - b).magnitude <= errorRange;
    }

    void UpdateRoutes()
    {
        for (int i = 0; i < patrolPositions.Length; i++) {
            patrolPositions[i].y = transform.position.y;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            //causing damage
            EventManager.TriggerEvent(ParameterizedGameEvent.damageReceive, (object)damageAmount);
        } else if (other.gameObject.CompareTag("TagGround")) {
            string eventUpdateRoute = transform.parent.GetInstanceID() + "UpdateRoute";
            //everytime it hits the ground, the route should be updated
            EventManager.TriggerEvent(eventUpdateRoute);
            onGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("TagGround")) {
            onGround = false;
        }
    }

    void OnDisable()
    {
        string eventUpdateRoute = transform.parent.GetInstanceID() + "UpdateRoute";
        EventManager.StopListening(eventUpdateRoute, UpdateRoutes);
    }
    //use another collider to detect if the player is in range of movement.
}
