using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInteractForTutorial : MonoBehaviour
{
    public GameObject blocker;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform obj in transform.GetComponentInChildren<Transform>()) {
            if (obj.gameObject.activeSelf && obj != transform && obj != blocker.transform) {
                blocker.SetActive(false);
                return;
            }
        }
        blocker.SetActive(true);
    }
}
