using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

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

    public static void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void Coroutine(IEnumerator routine)
    {
        instance.StartCoroutine(routine);
    }

    public static void CreateHealthBar(UnitBehaviour unit)
    {

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
    }

}
