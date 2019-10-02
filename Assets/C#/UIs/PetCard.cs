using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PetCard : MonoBehaviour, Selectable
{
    public GameObject pet;
    private Vector2 position; //the position of the current press
    private GameObject newPet;
    private bool selected = false;

    private static List<GameObject> beds = new List<GameObject>();

    void UpdateInput()
    {
        if (selected) {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(InputManager.GetPos());
            newPet.transform.position = newPos;
            if (InputManager.InputRelease()) Unselected();
        }
    }

    void Press()
    {
        selected = true;
        //creating the pet once its pressed
        newPet = Instantiate(pet, (Vector2)Camera.main.ScreenToWorldPoint(InputManager.GetPos()), Quaternion.identity);
        newPet.GetComponent<UnitBehaviour>().enabled = false;
        newPet.GetComponent<Collider2D>().enabled = false;
    }

    void Release()
    {
        selected = false;
        TryFindOffer();
        //put down the pet once its released
    }

    void TryFindOffer()
    {
        Bed target = null;
        for (int i = 0; i < beds.Count; i++) {
            if (beds[i].GetComponent<Bed>().RequestBed((Vector2)newPet.transform.position)) {
                target = beds[i].GetComponent<Bed>();
                break;
            }
        }
        if (target != null) {
            target.Initialize(newPet);
            newPet.GetComponent<Pet>().OfferBed(target);
            newPet.GetComponent<UnitBehaviour>().enabled = true;
            newPet.GetComponent<Collider2D>().enabled = true;
            newPet.transform.position = target.RequestPos();
        } else {
            Destroy(newPet, 0);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        beds = GameObject.FindGameObjectsWithTag("Bed").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
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
