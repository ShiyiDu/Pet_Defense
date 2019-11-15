using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public GameObject following;
    public float offset = 0.7f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        PetUtility.Coroutine(PetUtility.LinearZoom(Vector3.zero, Vector3.one, 0.1f, transform));
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 desired = following.transform.position;
        desired.y += offset * following.transform.localScale.y;
        transform.position = desired;
    }
}
