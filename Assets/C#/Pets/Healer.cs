using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Pet
{
    public float healPerSec = 5;
    public float healRange = 3.5f;
    public bool iAmPet = true;
    public GameObject rangeDetector;

    private float healGap = 0.4f;
    private float myTimer;
    private LinkedList<UnitBehaviour> friendInRange = new LinkedList<UnitBehaviour>();

    private void OnDrawGizmos()
    {
        rangeDetector.transform.localScale = new Vector3(healRange, healRange, 0);
    }

    // Start is called before the first frame update
    protected override void OnStart()
    {
        rangeDetector.SetActive(true);
        myTimer = healGap;
        rangeDetector.transform.localScale = new Vector3(healRange, healRange, 0);
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        if (myTimer <= 0) {
            foreach (UnitBehaviour unit in friendInRange) {
                Debug.Log("try to heal");
                unit.RestoreHealth(healPerSec * healGap);
            }
            myTimer = healGap;
        }
        myTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("something entered: " + collision.name);
        if (collision.CompareTag(iAmPet ? "Pet" : "Ghost")) {
            Debug.Log("friend entered");
            friendInRange.AddLast(collision.GetComponent<UnitBehaviour>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("something exited");
        if (collision.CompareTag(iAmPet ? "Pet" : "Ghost")) {
            friendInRange.Remove(collision.GetComponent<UnitBehaviour>());
        }
    }
}
