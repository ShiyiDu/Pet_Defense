using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//just some common functions of the game
public class PetUtility : MonoBehaviour
{
    private static PetUtility petUtility;

    public static PetUtility instance
    {
        get
        {
            if (!petUtility) {
                petUtility = FindObjectOfType(typeof(PetUtility)) as PetUtility;

                if (!petUtility) {
                    Debug.LogError("no PetUtility");
                }
            }

            return petUtility;
        }
    }

    //wait for a while and do something
    public static void WaitAndDo(float time, UnityAction method)
    {
        instance.StartCoroutine(Wait(time, method));
    }

    private static IEnumerator Wait(float time, UnityAction method)
    {
        yield return new WaitForSeconds(time);
        method.Invoke();
    }
}
