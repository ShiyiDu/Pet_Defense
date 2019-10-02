using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public float status; //current health
    public GameObject bar; //shrinking bar object
    public Ghost ghost; //will have to change

    public GameObject fullBar; //health bar parent
    private Vector2 target; //position underneath pet or ghost

    //GetComponent<Unit>().GetHealth();

    void Update()
    {
        status = ghost.GetHealth();
        bar.transform.localScale = new Vector2(status / 100, bar.transform.localScale.y);


        target = new Vector2(ghost.transform.position.x, ghost.transform.position.y - .65f);
        fullBar.transform.position = target;

    }

}


