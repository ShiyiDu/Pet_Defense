using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Pet
{
    //it faces the same direction as ghost
    public override void UpdataFaceDirection()
    {
        facingDirection = PetUtility.GetFloorDirection(transform.position);
        transform.rotation = Quaternion.Euler(0, facingDirection == Vector2.left ? 180 : 0, 0);
    }
}
