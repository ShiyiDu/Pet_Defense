using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusIndicator : MonoBehaviour
{
    public GameObject heal;
    public GameObject damage;

    private Dictionary<UnitBehaviour, float> units = new Dictionary<UnitBehaviour, float>();

    private void OnEnable()
    {
        EventManager.StartListening(ParameterizedGameEvent.unitDead, RemoveUnit);
        EventManager.StartListening(ParameterizedGameEvent.unitHealthChange, HealthChange);
        EventManager.StartListening(ParameterizedGameEvent.unitRespawn, AddUnit);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void HealthChange(object unit)
    {
        float oldHealth = units[((UnitBehaviour)unit)];
        float newHealth = ((UnitBehaviour)unit).GetHealth();
        float difference = newHealth - oldHealth;
        if (difference > 0) {
            GameObject newHeal = Instantiate(heal, ((UnitBehaviour)unit).transform.position, Quaternion.identity);
            newHeal.GetComponent<TextMeshPro>().SetText("+" + difference);
        } else if (difference < 0) {
            GameObject newDamage = Instantiate(damage, ((UnitBehaviour)unit).transform.position, Quaternion.identity);
            newDamage.GetComponent<TextMeshPro>().SetText(difference.ToString());
        }

        units[((UnitBehaviour)unit)] = newHealth;
    }

    void AddUnit(object unit)
    {
        units.Add(((UnitBehaviour)unit), ((UnitBehaviour)unit).GetHealth());
    }

    void RemoveUnit(object unit)
    {
        units.Remove(((UnitBehaviour)unit));
    }

    private void OnDisable()
    {
        EventManager.StopListening(ParameterizedGameEvent.unitDead, RemoveUnit);
        EventManager.StopListening(ParameterizedGameEvent.unitHealthChange, HealthChange);
        EventManager.StopListening(ParameterizedGameEvent.unitRespawn, AddUnit);
    }
}
