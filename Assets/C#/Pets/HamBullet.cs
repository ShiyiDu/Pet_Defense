using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamBullet : MonoBehaviour
{
    public int damage = 10;
    public float velocity = 5;
    public bool toRight = true; //default direction shot to right;

    public void Initialize(int damage, float velocity, bool toRight)
    {
        this.damage = damage;
        this.velocity = velocity;
        this.toRight = toRight;
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
        GetComponent<Rigidbody2D>().velocity = toRight ? Vector2.right * velocity : Vector2.left * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost")) {
            collision.GetComponent<Ghost>().TakeDamage(damage);
            Destroy(gameObject, 0);
        }
    }
}
