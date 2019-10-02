using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject bar; //shrinking bar object
    public GameObject fullBar; //health bar parent

    private UnitBehaviour unit; //will have to change
    private float status; //current health
    private Vector2 target; //position underneath pet or ghost

    //GetComponent<Unit>().GetHealth();

    public void Initialize(UnitBehaviour unit)
    {
        this.unit = unit;
    }

    void Update()
    {

        if(unit == null) {
            Destroy(gameObject, 0);
            return;
        }
        status = unit.GetHealth();
        bar.transform.localScale = new Vector2(status / (float)unit.GetMaxHealth(), bar.transform.localScale.y);

        target = new Vector2(unit.transform.position.x, unit.transform.position.y - .65f);
        fullBar.transform.position = target;
    }

}


