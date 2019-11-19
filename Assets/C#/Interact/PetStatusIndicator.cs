using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStatusIndicator : MonoBehaviour
{
    public GameObject enemyBubble;
    public GameObject bedBubble;

    private class PetInfo
    {
        public Pet pet; public Animator animator;
        public bool goToEnemy, attackMode;//check goToEnemy only when in the attackmode
    }

    private HashSet<Pet> toBeDelete = new HashSet<Pet>();
    private LinkedList<PetInfo> petInfos = new LinkedList<PetInfo>();
    private Dictionary<Pet, GameObject> bubblesCreated = new Dictionary<Pet, GameObject>();
    // Start is called before the first frame update

    void ShowState(PetInfo info)
    {
        GameObject newBubble;
        void ClearBubble()
        {
            //if a new bubble is created, clear out all the old bubbles.
            GameObject oldBubble;
            if (bubblesCreated.TryGetValue(info.pet, out oldBubble)) {
                Destroy(oldBubble);
                bubblesCreated[info.pet] = newBubble;
            } else {
                bubblesCreated.Add(info.pet, newBubble);
            }
            newBubble.GetComponent<SpeechBubble>().following = info.pet.gameObject;
            Vector2 desired = newBubble.transform.position;
            desired.y += newBubble.GetComponent<SpeechBubble>().offset * info.pet.transform.localScale.y;
            newBubble.transform.position = desired;

            Destroy(newBubble, 2);
        }

        if (info.goToEnemy || (info.attackMode && info.pet.enemyInHouse)) {
            newBubble = Instantiate(enemyBubble, info.pet.transform.position, Quaternion.identity);
            ClearBubble();
        } else {
            newBubble = Instantiate(bedBubble, info.pet.transform.position, Quaternion.identity);
            ClearBubble();
        }
    }

    void NewPet(object obj)
    {
        Pet pet = ((UnitBehaviour)obj).GetComponent<Pet>();
        if (pet != null) {
            PetInfo newInfo = new PetInfo();
            newInfo.pet = pet;
            newInfo.animator = pet.GetComponent<Animator>();
            newInfo.goToEnemy = false;
            petInfos.AddLast(newInfo);
        }
    }

    void CheckState(PetInfo info)
    {
        bool enemy = info.animator.GetCurrentAnimatorStateInfo(0).IsName("GoToEnemy");
        bool bed = info.animator.GetCurrentAnimatorStateInfo(0).IsName("GoToBed");
        bool attackMode = !info.pet.rest;
        //when you first exit attack mode, you show bed bubble
        //when you first enter attac kmode, you show enemy bubble if there's no enemy in house

        if (!attackMode && info.attackMode) {
            info.goToEnemy = false;
            info.attackMode = false;
            ShowState(info);
        } else if (attackMode && !info.attackMode) {
            info.attackMode = true;
            //todo: doing this kind of stuff is very unefficient, needs to be optimized
            if (FindObjectOfType<Ghost>() == null) ShowState(info);
        }

        if (enemy && !info.goToEnemy) {
            //its the first time we see it, indicate the change!
            info.goToEnemy = true;
            ShowState(info);
        } else if (bed && info.goToEnemy) {
            info.goToEnemy = false;
            ShowState(info);
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening(ParameterizedGameEvent.unitRespawn, NewPet);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        LinkedListNode<PetInfo> node = petInfos.First;
        LinkedListNode<PetInfo> next;
        if (petInfos.Count < 1) return;
        while (node != null) {
            next = node.Next;
            if (node.Value.pet == null) {
                petInfos.Remove(node);
            } else {
                CheckState(node.Value);
            }

            node = next;
        }
    }

    private void OnDisable()
    {
        EventManager.StopListening(ParameterizedGameEvent.unitRespawn, NewPet);
    }
}
