using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCard : MonoBehaviour
{
    public GameObject pet;
    private Vector3 position; //the position of the current press
    private GameObject newPet;

    void UpdateInput()
    {

        RaycastHit2D hit2D;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hit2D = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit2D) {
            Debug.Log("something is hit");
            if (hit2D.transform.GetInstanceID() == transform.GetInstanceID()) Debug.Log("My object is clicked by mouse");
        }

        if (PetUtility.InputIsPressing()) {
            position = Camera.main.ScreenToWorldPoint((PetUtility.InputCoor()));
            position.z = 0;
        }
    }

    void Press()
    {
        if (PetUtility.InputPush()) {
            newPet = Instantiate(pet, position, Quaternion.identity);
        }
        //creating the pet once its pressed
    }

    void Release()
    {
        //put down the pet once its released
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }
}
