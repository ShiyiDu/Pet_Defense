using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//this is a basic state machine for both pets and ghost to control AIs
public abstract class UnitBehaviour : MonoBehaviour
{
    public float velocity = 1f;
    public int health = 100;

    //how close is considered "at the point"
    public float enterDoorTime = 0.5f;
    public float exitDoorTime = 0.5f;

    //the white ghost patroling through all these points

    public float attackInterval = 2f;
    public int damage = 5;
    public float attackRange = 10f; //the default attack range is 10 unit.

    public Vector2[] routePoints;
    [HideInInspector]
    public GameObject enemy;
    public Bullet bullet;
    public float bulletVelocity;

    protected Vector2 facingDirection = Vector2.right;

    protected bool enemyEntered = false;
    protected Direction enemyDirection = Direction.right;
    protected float timer = 1f;

    protected Rigidbody2D rigid;
    protected new SpriteRenderer renderer;
    protected bool nearDoor = false;

    protected DoorControl door = null;
    protected UnitState state = UnitState.respawn;

    protected int maxHealth;

    private Color origin;

    private Dictionary<UnitState, UnityAction> actions = new Dictionary<UnitState, UnityAction>();

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    public Vector2[] GetRoute()
    {
        return routePoints;
    }

    /// <summary>
    /// where exactly do you want the bullet to shoot from
    /// </summary>
    /// <returns></returns>
    public Vector2 GetShootPosition()
    {
        return (Vector2)transform.position + facingDirection * 0.2f;
    }

    public Vector2 GetFaceDirection()
    {
        facingDirection = transform.rotation.eulerAngles.y > 0 ? Vector2.left : Vector2.right;

        return facingDirection;
    }

    public virtual UnitState GetState()
    {
        return state;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public virtual int GetHealth()
    {

        return health;
    }

    public virtual void TakeDamage(int damage)
    {
        renderer.color = Color.red;
        void restoreColor()
        {
            if (renderer != null) renderer.color = origin;
        }
        PetUtility.WaitAndDo(0.1f, restoreColor);
        health -= damage;
        if (health <= 0) Destroy(gameObject, 0f);
    }

    public virtual void Kill()
    {
        Destroy(gameObject, 0f);
    }

    //this is called when the ghost enterd a door
    public void DoorEntered(DoorControl door)
    {
        nearDoor = true;
        this.door = door;
    }

    public void DoorExited(DoorControl door)
    {
        nearDoor = false;
        if (this.door = door) this.door = null;
    }

    //void StateMachine()
    //{
    //    actions[state].Invoke();
    //}

    // Start is called before the first frame update
    private void Start()
    {
        maxHealth = health;
        renderer = gameObject.GetComponent<SpriteRenderer>();
        origin = renderer.color;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        OnStart();
    }

    // Update is called once per frame
    private void Update()
    {
        //StateMachine();
        OnUpdate();
    }
}
