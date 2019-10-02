using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the parent class for all bullets
public abstract class Bullet : MonoBehaviour
{
    public abstract void Initialize(int damage, float velocity, Vector2 direction);
}
