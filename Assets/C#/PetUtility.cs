using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

//just some common functions of the game
public class PetUtility : MonoBehaviour
{
    private static PetUtility petUtility;
    private static bool touchPressed = false;
    private static bool mousePressed = false;//if anything is pressed right now

    //returns the coordinates in screen space for the input
    public static Vector2 InputCoor()
    {
        //Touch touch = Input.touches[0];
        Vector2 result = Vector2.zero;

        if (touchPressed) {
            //result = touch.position;
        }

        if (mousePressed) {
            result = Input.mousePosition;
        }

        return result;

    }

    public static bool InputIsPressing()
    {
        return touchPressed || mousePressed;
    }

    public static bool InputPush()
    {
        return Input.GetMouseButtonDown(0) || Input.touches[0].phase == TouchPhase.Began;
        //when a mouse or a finger is held in the screen
    }

    public static bool InputRelease()
    {
        return Input.GetMouseButtonUp(0) || Input.touches[0].phase == TouchPhase.Ended;
    }

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

    public static IEnumerator LinearMove(Vector2 from, Vector2 to, float duration, Transform target)
    {
        float timer = 0f;
        while (timer <= duration) {
            Vector2 current = (to - from) * (timer / duration) + from;
            target.position = current;
            timer += Time.deltaTime;
            yield return null;
        }
        target.position = to;
    }

    //linearly fade a vector3 object
    public static IEnumerator LinearScaleFade(Vector3 start, Vector3 end, float duration, Transform target)
    {
        float timer = 0f;
        while (timer <= duration) {
            Vector3 current = (end - start) * (timer / duration) + start;
            target.localScale = current;
            timer += Time.deltaTime;
            yield return null;
        }
        target.localScale = end;
    }

    //this is public just in case we want to get the coroutine object to stop it
    public static IEnumerator Wait(float time, UnityAction method)
    {
        yield return new WaitForSeconds(time);
        method.Invoke();
    }

    public static void UnimplementedWarning(string name)
    {
        Debug.Log(name + " function not implemented");
    }

    private void Update()
    {
        //if (Input.touches[0].phase == TouchPhase.Began) {
        //    touchPressed = true;
        //}

        //if (Input.touches[0].phase == TouchPhase.Ended) {
        //    touchPressed = false;
        //}

        if (Input.GetMouseButtonDown(0)) {
            mousePressed = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            mousePressed = false;
        }
    }
}
