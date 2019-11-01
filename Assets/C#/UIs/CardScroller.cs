using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScroller : MonoBehaviour
{
    public float cardHeights = 0.5f; //the height of each card
    public float cardGap = 0.1f; //the gap between each card
    public float rollerStart = 3;
    public float rollerHeight = 10;
    private float rollerTop; //the limit of the roller, set during start.
    private float rollerBot;
    private int cardNum = 6; //the number of cards we currently have
    private const float bounceConst = 0.2f; //these 2 constant controls the bouncing back speed
    private const float bounceConst2 = 0.05f;
    public PetCard[] allCards;

    private bool isScroll = false;
    //arrange cards
    //beable to move cards
    //restrict the scroll if out of range

    public void StartScroll()
    {
        isScroll = true;
    }

    public void EndScroll()
    {
        isScroll = false;
    }

    private void BounceBack()//bounce back to the normal position
    {
        if (!isScroll) {
            float currentTop = transform.position.y + rollerStart;
            float currentBot = currentTop - (cardHeights + cardGap) * allCards.Length;
            float desiredBot = Mathf.Max(rollerBot, rollerTop - allCards.Length * (cardHeights + cardGap));

            if (currentTop < rollerTop) {
                transform.Translate(Vector3.up * ((rollerTop - currentTop) * bounceConst + bounceConst2));
            }

            if (currentBot > desiredBot && currentTop - rollerTop > bounceConst2) {
                transform.Translate(Vector3.down * ((currentBot - desiredBot) * bounceConst + bounceConst2));
            }

        }
    }

    private void Scroll()
    {
        Vector2 newPosition = transform.position;
        newPosition.y += (Camera.main.ScreenToWorldPoint(InputManager.GetDeltaPos())).y + Camera.main.orthographicSize;
        transform.position = newPosition;
    }

    private void OnDrawGizmosSelected()
    {
        allCards = GetComponentsInChildren<PetCard>();
        RerangeCards();

        Vector2 start = transform.position;
        start.y += rollerStart;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(start.x + 0.2f, start.y), new Vector2(start.x + 0.2f, start.y - rollerHeight));

        for (int i = 0; i < cardNum; i++) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(start, start = Vector2.down * cardGap + start);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(start, start = Vector2.down * cardHeights + start);
        }
    }

    // Start is called before the first frazme update
    void Start()
    {
        rollerTop = transform.position.y + rollerStart;
        rollerBot = rollerTop - rollerHeight;
    }

    void RerangeCards()
    {
        Vector2 start = transform.position;
        start.y += cardNum / 2f * cardHeights + cardNum / 2f * cardGap;

        for (int i = 0; i < allCards.Length; i++) {
            float height = i * (cardHeights + cardGap);
            Vector2 newPos = allCards[i].transform.position;
            newPos.y = start.y - height - cardGap - cardHeights / 2;
            newPos.x = transform.position.x;
            allCards[i].transform.position = newPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isScroll) Scroll();
        BounceBack();
    }
}
