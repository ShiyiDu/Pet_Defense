using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Net.NetworkInformation;

public class PetCard : MonoBehaviour, Selectable
{
    public GameObject unit;
    private Vector2 position; //the position of the current press
    private GameObject newUnit;
    private bool selected = false;
    private bool iAmPet = true;

    private static List<GameObject> beds = new List<GameObject>();

    void UpdateInput()
    {
        if (selected) {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(InputManager.GetPos());
            newUnit.transform.position = newPos;
            if (InputManager.InputRelease()) Unselected();
        }
    }

    void Press()
    {
        selected = true;
        //creating the pet once its pressed
        newUnit = Instantiate(unit, (Vector2)Camera.main.ScreenToWorldPoint(InputManager.GetPos()), Quaternion.identity);
        Color transParent = newUnit.GetComponent<SpriteRenderer>().color;
        transParent.a = 0.5f * transParent.a;
        newUnit.GetComponent<SpriteRenderer>().color = transParent;
        newUnit.GetComponent<Rigidbody2D>().simulated = false;
        newUnit.GetComponent<UnitBehaviour>().enabled = false;
        newUnit.GetComponent<Collider2D>().enabled = false;
        newUnit.GetComponent<Animator>().enabled = false;
    }

    void Release()
    {
        selected = false;
        if (iAmPet) TryFindOffer();
        ActiveUnit();
        //put down the pet once its released
    }

    void TryFindOffer()
    {
        Bed target = null;
        for (int i = 0; i < beds.Count; i++) {
            if (beds[i].GetComponent<Bed>().RequestBed((Vector2)newUnit.transform.position)) {
                target = beds[i].GetComponent<Bed>();
                break;
            }
        }
        if (target != null) {
            target.Initialize(newUnit);
            newUnit.GetComponent<Pet>().OfferBed(target);
            newUnit.transform.position = target.RequestPos();
        } else {
            Destroy(newUnit, 0);
        }

    }

    void ActiveUnit()
    {
        Color fullColor = newUnit.GetComponent<SpriteRenderer>().color;
        fullColor.a = 2f * fullColor.a;
        newUnit.GetComponent<SpriteRenderer>().color = fullColor;
        newUnit.GetComponent<Rigidbody2D>().simulated = true;
        newUnit.GetComponent<UnitBehaviour>().enabled = true;
        newUnit.GetComponent<Collider2D>().enabled = true;
        newUnit.GetComponent<Animator>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (unit.GetComponent<Ghost>() != null) iAmPet = false;
        beds = GameObject.FindGameObjectsWithTag("Bed").ToList();

        GameObject icon = Instantiate(unit, transform.position, Quaternion.identity);
        foreach (SpriteRenderer rend in icon.GetComponentsInChildren<SpriteRenderer>()) {
            rend.sortingOrder = 12;
        }
        //icon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 12;
        //icon.GetComponent<SpriteRenderer>().sortingOrder = 12;
        Destroy(icon.GetComponent<Rigidbody2D>());
        Destroy(icon.GetComponent<UnitBehaviour>());
        Destroy(icon.GetComponent<Collider2D>());
        Destroy(icon.GetComponent<Animator>());
        icon.transform.SetParent(transform);
        icon.name = "Pet Icon";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }

    void OnDrawGizmos()
    {
        if (unit) gameObject.name = unit.name;
    }

    public void Selected()
    {
        Press();
    }

    public void Unselected()
    {
        Release();
    }
}
