using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//this is a basic state machine for both pets and ghost to control AIs
public abstract class UnitStateMachine : MonoBehaviour, Unit
{
    public float velocity = 1f;
    public int health = 100;

    //how close is considered "at the point"
    public float tolerance = 0.1f;
    public float enterDoorTime = 0.5f;
    public float exitDoorTime = 0.5f;

    //the white ghost patroling through all these points

    public float attackInterval = 2f;
    public int damage = 5;
    public float attackRange = 10f; //the default attack range is 10 unit.

<<<<<<< HEAD:Assets/C#/UnitStateMachine.cs
<<<<<<< HEAD:Assets/C#/BaseClass/UnitBehaviour.cs
    public Vector2[] routePoints;
    [HideInInspector]
    public GameObject enemy;

    protected Direction facingDirection = Direction.right;

=======
=======
>>>>>>> parent of d628923... Working on AI stuff:Assets/C#/UnitStateMachine.cs
    protected GameObject enemy;
>>>>>>> parent of d628923... Working on AI stuff:Assets/C#/UnitStateMachine.cs
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

    protected virtual void Walk() { }

    protected virtual void EnterDoor() { }

    protected virtual void ExitDoor() { }

    protected virtual void Attack() { }

    protected virtual void Idle() { }

    protected virtual void Die() { }

    protected virtual void Respawn()
    {
        rigid.velocity = Vector2.zero;
        state = UnitState.idle;
    }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    private void AddStateAction()
<<<<<<< HEAD:Assets/C#/UnitStateMachine.cs
    {
        actions.Add(UnitState.walk, Walk);
        actions.Add(UnitState.attack, Attack);
        actions.Add(UnitState.idle, Idle);
        actions.Add(UnitState.die, Die);
        actions.Add(UnitState.enterDoor, EnterDoor);
        actions.Add(UnitState.exitDoor, ExitDoor);
        actions.Add(UnitState.respawn, Respawn);
    }

    void StateMachine()
    {
        actions[state].Invoke();
    }

    public Direction GetFaceDirection()
    {
        facingDirection = transform.rotation.eulerAngles.y > 0 ? Direction.left : Direction.right;

        return facingDirection;
=======
    {
        actions.Add(UnitState.walk, Walk);
        actions.Add(UnitState.attack, Attack);
        actions.Add(UnitState.idle, Idle);
        actions.Add(UnitState.die, Die);
        actions.Add(UnitState.enterDoor, EnterDoor);
        actions.Add(UnitState.exitDoor, ExitDoor);
        actions.Add(UnitState.respawn, Respawn);
    }

    void StateMachine()
    {
        actions[state].Invoke();
>>>>>>> parent of d628923... Working on AI stuff:Assets/C#/UnitStateMachine.cs
    }

    public virtual UnitState GetState()
    {
        return state;
    }


    public int GetMaxHealth()
    {
        return health;
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

    // Start is called before the first frame update
    private void Start()
    {
        AddStateAction();
        maxHealth = health;
        renderer = gameObject.GetComponent<SpriteRenderer>();
        origin = renderer.color;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        OnStart();
    }

    // Update is called once per frame
    private void Update()
    {
        StateMachine();
        OnUpdate();
    }
}
