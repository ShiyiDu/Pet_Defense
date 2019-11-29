using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDistributor : MonoBehaviour
{
    public CardScroller cardScroller;

    public PetCard ham;
    public PetCard pom;
    public PetCard snake;
    public PetCard wizard;
    public PetCard fish;
    public PetCard bird;
    public PetCard turtle;

    public PetOwned[] ownedData;

    private static List<PetCard> cardLookUp;

    private static PetDistributor distributor;

    private static PetDistributor instance
    {
        get
        {
            if (distributor == null) {
                distributor = FindObjectOfType<PetDistributor>();
            }
            if (cardLookUp == null) {
                cardLookUp = new List<PetCard>();
                cardLookUp.Add(distributor.pom);
                cardLookUp.Add(distributor.ham);
                cardLookUp.Add(distributor.snake);
                cardLookUp.Add(distributor.wizard);
                cardLookUp.Add(distributor.fish);
                cardLookUp.Add(distributor.bird);
                cardLookUp.Add(distributor.turtle);
            }
            return distributor;
        }
    }

    public static void Distribute(int levelNum)
    {
        PetOwned petOwned = instance.ownedData[levelNum];
        instance.cardScroller.ClearCard();

        CreateCard(0, petOwned.pom);
        CreateCard(1, petOwned.ham);
        CreateCard(2, petOwned.snake);
        CreateCard(3, petOwned.wizard);
        CreateCard(4, petOwned.fish);
        CreateCard(5, petOwned.bird);
        CreateCard(6, petOwned.turtle);

        instance.cardScroller.RerangeCards();
    }

    private static void CreateCard(int cardIndex, int cardCount)
    {
        if (cardCount == 0) return;
        PetCard target = cardLookUp[cardIndex];
        for (int i = 0; i < cardCount; i++) {
            PetCard newCard = Instantiate(target);
            instance.cardScroller.AddCard(newCard);
        }
    }

    private static void CleanUp()
    {
        instance.cardScroller.ClearCard();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
