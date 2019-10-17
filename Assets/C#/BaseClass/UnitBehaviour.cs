using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//this is a basic state machine for both pets and ghost to control AIs
public abstract class UnitBehaviour : MonoBehaviour
{
    public float velocity = 1f;
    public float health = 100;

    //how close is considered "at the point"
    //public float enterDoorTime = 0.5f;
    //public float exitDoorTime = 0.5f;
    [HideInInspector]
    public bool enterDoor = false;

    //the white ghost patroling through all these points

    public float attackInterval = 2f;
    public int damage = 5;
    public float attackRange = 10f; //the default attack range is 10 unit.

    //[HideInInspector]
    public Vector2 destination;

    public Bullet bullet;
    public float bulletVelocity;
    [HideInInspector]
    public bool launchingAttack;

    [HideInInspector]
    public GameObject enemy;
    [HideInInspector]
    public Vector2[] routePoints;
    [HideInInspector]
    public int doorToken;//if you entered a door, this is the token you got

    [HideInInspector]
    public bool doorAcquired = false;
    [HideInInspector]
    public bool enemyInRange = false;
    [HideInInspector]
    public DoorControl door = null;

    protected Vector2 facingDirection = Vector2.right;

    protected Direction enemyDirection = Direction.right;
    protected float timer = 1f;

    protected Rigidbody2D rigid;
    protected new SpriteRenderer renderer;
    protected bool nearDoor = false;

    protected UnitState state = UnitState.respawn;

    protected float maxHealth;

    private Color origin;

    private Dictionary<UnitState, UnityAction> actions = new Dictionary<UnitState, UnityAction>();

    public virtual Vector2[] GetRoute()
    {
        return routePoints;
    }

    /// <summary>
    /// where exactly do you want the bullet to shoot from
    /// </summary>
    /// <returns></returns>
    public virtual Vector2 GetShootPosition()
    {
        return (Vector2)transform.position + facingDirection * 0.2f;
    }

    public virtual Vector2 GetFaceDirection()
    {
        facingDirection = transform.rotation.eulerAngles.y > 0 ? Vector2.left : Vector2.right;

        return facingDirection;
    }

    public virtual UnitState GetState()
    {
        return state;
    }

    public virtual float GetMaxHealth()
    {
        return maxHealth;
    }

    public virtual float GetHealth()
    {

        return health;
    }

    public virtual void RestoreHealth(float heal)
    {
        if (health < maxHealth) {
            health = Mathf.Min(health + heal, maxHealth);
            EventManager.TriggerEvent(ParameterizedGameEvent.unitHealthChange, this);
        }
    }

    private float lastDamageTime;
    public virtual void TakeDamage(float damage)
    {
        float changeColorTime = 0.08f;
        foreach (SpriteRenderer r in gameObject.GetComponentsInChildren<SpriteRenderer>()) {
            r.color = Color.red;
        }

        lastDamageTime = Time.time;
        void restoreColor()
        {
            if (renderer != null && Time.time - lastDamageTime > changeColorTime) {
                foreach (SpriteRenderer r in gameObject.GetComponentsInChildren<SpriteRenderer>()) {
                    r.color = origin;
                }
            }
        }
        PetUtility.WaitAndDo(changeColorTime, restoreColor);
        health -= damage;
        EventManager.TriggerEvent(ParameterizedGameEvent.unitHealthChange, this);
        if (health <= 0) Kill();
    }

    public virtual void Kill()
    {
        EventManager.TriggerEvent(ParameterizedGameEvent.unitDead, this);
        Destroy(gameObject, 0f);
    }

    //this is called when the ghost enterd a door
    //public virtual void DoorEntered(DoorControl door)
    //{
    //    nearDoor = true;
    //    this.door = door;
    //}

    //public virtual void DoorExited(DoorControl door)
    //{
    //    nearDoor = false;
    //    if (this.door = door) this.door = null;
    //}

    //void StateMachine()
    //{
    //    actions[state].Invoke();
    //}

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

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
