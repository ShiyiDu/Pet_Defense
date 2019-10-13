using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitGhost : Ghost
{
    public GameObject blueGhost; //the small ghost you split into
    public GameObject redGhost;
    public float splitDistance = 1f;
    private void Split()
    {
        Debug.Log("try to split");
        //split into 2 small ghosts, move from center to 2 sides.
        GameObject leftGhost, rightGhost;
        leftGhost = Instantiate(blueGhost, transform.position, Quaternion.identity);
        rightGhost = Instantiate(redGhost, transform.position, Quaternion.identity);

        float heightDifference = 0.3f;
        Vector2 from = (Vector2)transform.position + Vector2.down * heightDifference;
        Vector2 leftDes = from + Vector2.left * (splitDistance / 2f);
        Vector2 rightDes = from + Vector2.right * (splitDistance / 2f);

        PetUtility.Coroutine(PetUtility.SublinearMove(from, leftDes, 0.25f, leftGhost.transform));
        PetUtility.Coroutine(PetUtility.SublinearMove(from, rightDes, 0.25f, rightGhost.transform));

    }

    public override void Kill()
    {
        Split();
        base.Kill();
    }
}
