using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Bullet
{
    public int jumps = 4;
    public float damage = 10;
    public bool hitGhost = true; //is it hitting ghost or pets?
    public float jumpGap = 0.5f;
    public float jumpRadius = 2f;
    //public float hitRadius = 4f; //the radius of first hit

    private HashSet<UnitBehaviour> hit = new HashSet<UnitBehaviour>();
    private float timer;
    private UnitBehaviour nextTarget;

    private void OnEnable()
    {
        //GetComponent<CircleCollider2D>().radius = hitRadius;
        GetComponent<CircleCollider2D>().radius = jumpRadius;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = jumpGap;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) {
            if (jumps > 0) Jump();
            else Destroy(gameObject);
            timer = jumpGap;
        }
        timer -= Time.deltaTime;
    }

    void Jump()
    {
        if (nextTarget != null) {
            transform.position = nextTarget.transform.position;
            jumps -= 1;
            nextTarget.TakeDamage(damage);
            hit.Add(nextTarget);
            nextTarget = null;
        } else {
            jumps = 0; //if no enemy nearby, stop jumping and wait to die.
        }

        GetComponent<CircleCollider2D>().radius = jumpRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (nextTarget != null) return;
        if (collision.CompareTag(hitGhost ? "Ghost" : "Pet")) {
            nextTarget = collision.GetComponent<UnitBehaviour>();
            if (hit.Contains(nextTarget)) nextTarget = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (nextTarget != null) return;
        if (collision.CompareTag(hitGhost ? "Ghost" : "Pet")) {
            nextTarget = collision.GetComponent<UnitBehaviour>();
            if (hit.Contains(nextTarget)) nextTarget = null;
        }
    }

    public override void Initialize(int damage, float velocity, Vector2 direction, UnitBehaviour enemy)
    {
        this.damage = damage;
        this.nextTarget = enemy;
    }
}
