using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Net.NetworkInformation;
using TMPro;
using System.Runtime.CompilerServices;

public class PetCard : MonoBehaviour, Selectable
{
    public GameObject unit;

    public GameObject petIcon;
    public TextMeshPro petName;
    public TextMeshPro petAttack;
    public GameObject heart;

    public GameObject greyedOut;

    public float heartGap = 0.1f;
    public float iconRatio = 0.7f;

    private Vector2 position; //the position of the current press
    private GameObject newUnit;

    private bool selected = false;
    private bool checkingInput = false;//check the direction of input first
    private bool scrolling = false;
    private bool scrollable = true;

    private bool iAmPet = true;
    private CardScroller scroller;

    private static List<GameObject> beds = new List<GameObject>();

    private float cardOffSet = 0.7f; //the offset ratio so that your stupid finger doesn't block the pet.
    private float cardSelectRatio = 1.2f; //how big the pet should look when pressed?

    void UpdateInput()
    {
        if (selected) {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(InputManager.GetPos());
            newUnit.transform.position = newPos + Vector2.up * (newUnit.transform.localScale.y * cardOffSet);
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
        newUnit.transform.localScale = newUnit.transform.localScale * cardSelectRatio;
        //newUnit.GetComponent<SpriteRenderer>().color = transParent;

        foreach (SpriteRenderer rend in newUnit.GetComponentsInChildren<SpriteRenderer>()) {
            rend.sortingOrder += 15;
        }

        newUnit.GetComponent<Rigidbody2D>().simulated = false;
        newUnit.GetComponent<UnitBehaviour>().enabled = false;
        newUnit.GetComponent<Collider2D>().enabled = false;
        newUnit.GetComponent<Animator>().enabled = false;
    }

    void Release()
    {
        selected = false;
        if (iAmPet && TryFindOffer()) {
            ActiveUnit();
            Destroy(gameObject);
            PetUtility.WaitAndDo(0.1f, scroller.RerangeCards);
        }
        //put down the pet once its released
    }

    bool TryFindOffer()
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
            return true;
        } else {
            Destroy(newUnit);
            return false;
        }

    }

    void ActiveUnit()
    {
        Color fullColor = newUnit.GetComponent<SpriteRenderer>().color;
        fullColor.a = 2f * fullColor.a;
        foreach (SpriteRenderer rend in newUnit.GetComponentsInChildren<SpriteRenderer>()) {
            rend.sortingOrder -= 15;
        }
        newUnit.transform.localScale = newUnit.transform.localScale / cardSelectRatio;
        newUnit.GetComponent<SpriteRenderer>().color = fullColor;
        newUnit.GetComponent<Rigidbody2D>().simulated = true;
        newUnit.GetComponent<UnitBehaviour>().enabled = true;
        newUnit.GetComponent<Collider2D>().enabled = true;
        newUnit.GetComponent<Animator>().enabled = true;
    }

    void CreateIcon()
    {
        GameObject icon = Instantiate(unit, petIcon.transform.position, Quaternion.identity);
        foreach (SpriteRenderer rend in icon.GetComponentsInChildren<SpriteRenderer>()) {
            if (rend.gameObject.layer == 1) rend.enabled = false; //layer 1 is the transparentfx
            rend.sortingOrder += 10;
            rend.gameObject.layer = LayerMask.NameToLayer("MaskedUI");
        }
        Destroy(icon.GetComponent<Rigidbody2D>());
        Destroy(icon.GetComponent<UnitBehaviour>());
        foreach (Collider2D col in icon.GetComponentsInChildren<Collider2D>()) {
            Destroy(col);
        }

        //while (icon.GetComponent<Collider2D>() != null) Destroy(icon.GetComponent<Collider2D>());

        Destroy(icon.GetComponent<Animator>());
        icon.transform.SetParent(transform);
        icon.name = "Pet Icon";
        icon.transform.localScale = Vector3.one * iconRatio;
        icon.transform.rotation = Quaternion.Euler(0, 180, 0);
        Destroy(petIcon);
    }

    void CreateHearts()
    {
        int hearts = Mathf.RoundToInt(unit.GetComponent<UnitBehaviour>().GetHealth() / 20f);
        Debug.Log("hearts:" + hearts);
        hearts--;
        for (int i = 0; i < hearts; i++) {
            GameObject newHeart = Instantiate(heart, heart.transform.position, Quaternion.identity, transform);
            Vector2 pos = newHeart.transform.position;
            pos.x += heartGap;
            newHeart.transform.position = pos;
            heart = newHeart;
        }
    }

    void ChangeText()
    {
        //change the attack value and card name
        petName.text = unit.name;
        petAttack.text = (unit.GetComponent<UnitBehaviour>().damage).ToString();
    }

    //this value should be decided based on resolution?
    private float totalTravelRequire = 16f;//must travel atleast 16 pixel to make a decision
    private Vector2 pressPosition;
    //decide if we are creating pets or scroll 
    public void CheckInput()
    {
        //one sample could be only one pixel move, let's pick multiple samples!
        Vector2 delta = InputManager.GetPos() - pressPosition; //the delta position since press event happened
        if (delta.magnitude >= totalTravelRequire) {
            if (Mathf.Abs(delta.x) * 1.7 >= Mathf.Abs(delta.y) || !scrollable) { //tan(30) is about 1.7, so prioritize placing pets maybe
                selected = true;
                EventManager.TriggerEvent(GameEvent.selectBedStart);
                Press();
            } else {
                scrolling = true;
                scroller.StartScroll();
            }

            checkingInput = false;
        }
    }

    public void Selected()
    {
        checkingInput = true;
        pressPosition = InputManager.GetPos();
        ShrinkCard();
        greyedOut.SetActive(true);
    }

    public void Unselected()
    {
        checkingInput = false;
        if (selected) Release();
        EventManager.TriggerEvent(GameEvent.selectBedFinish);
        scroller.EndScroll();
        UnShrinkCard();
        greyedOut.SetActive(false);
    }

    float shrinkRatio = 0.85f;
    float shrinkDuration = 0.05f;
    private void ShrinkCard()
    {
        PetUtility.Coroutine(PetUtility.LinearZoom(transform.localScale, shrinkRatio * Vector3.one, shrinkDuration, transform));
        //Debug.Log("trying to shrink card");
    }

    private void UnShrinkCard()
    {
        PetUtility.Coroutine(PetUtility.LinearZoom(transform.localScale, Vector3.one, shrinkDuration, transform));
    }

    void FreezeScroll() { scrollable = false; }

    void UnfreezeScroll() { scrollable = true; }

    void OnEnable()
    {
        EventManager.StartListening(GameEvent.scrollFreezed, FreezeScroll);
        EventManager.StartListening(GameEvent.scrollUnfreezed, UnfreezeScroll);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (unit.GetComponent<Ghost>() != null) iAmPet = false;
        beds = GameObject.FindGameObjectsWithTag("Bed").ToList();
        CreateIcon();
        CreateHearts();
        ChangeText();
        scroller = FindObjectOfType<CardScroller>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        if (checkingInput) CheckInput(); //check if we are creating pets or scrolling the scroller
    }

    void OnDrawGizmos()
    {
        if (unit) gameObject.name = unit.name;
    }

    void OnDisable()
    {
        EventManager.StopListening(GameEvent.scrollFreezed, FreezeScroll);
        EventManager.StopListening(GameEvent.scrollUnfreezed, UnfreezeScroll);
    }
}
