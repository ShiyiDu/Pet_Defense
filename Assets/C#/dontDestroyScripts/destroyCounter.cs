using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyCounter : MonoBehaviour
{
    private void Awake()
    {
        Destroy(GameObject.Find("CounterCanvas"));
    }
}
