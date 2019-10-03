using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamBullet : Bullet
{
    private int damage = 10;
    private float velocity = 5;
    private Vector2 direction = Vector2.right;

    public override void Initialize(int damage, float velocity, Vector2 direction)
    {
        this.damage = damage;
        this.velocity = velocity;
        this.direction = direction;
        if (direction == Vector2.left) transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void OnEnable()
    {
        Destroy(gameObject, 15);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost")) {
            collision.GetComponent<Ghost>().TakeDamage(damage);
            Destroy(gameObject, 0);
        }
    }
}
