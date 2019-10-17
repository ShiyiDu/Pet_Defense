using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    public float fadingTime = 1f;
    public float moveDistance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, fadingTime + Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * (moveDistance / fadingTime) * Time.deltaTime);
    }
}
