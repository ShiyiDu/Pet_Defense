﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

//just some common functions of the game
public class PetUtility : MonoBehaviour
{
    public GameObject healthBar;

    [Serializable]
    public struct FloorMarker
    {
        public Vector2 position;
        public bool toRight;
    }

    public FloorMarker[] floorMarker;
    public Vector2[] wayPoints;

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

    /// <summary>
    /// returns the nearest ghost on floor basis, ignore the upper floors
    /// return null if nothing is found
    /// </summary>
    public static Ghost GetNearestGhost(Vector2 from)
    {
        //cast a ray to find out?
        int currentFloor = GetFloorNumber(from);
        for (int i = currentFloor; i >= 0; i--) {
            RaycastHit2D hit2D;
            hit2D = Physics2D.Raycast(instance.floorMarker[i].position + Vector2.up * 0.5f, Vector2.right, LayerMask.GetMask("Ghost"));
            Ghost result;
            if (hit2D) {
                if ((result = hit2D.collider.GetComponent<Ghost>()) != null) return result;
            }
        }
        return null;
    }

    public static Vector2 FindNextWayPoint(Vector2 start, Vector2 end)
    {
        //todo: this can be optimized
        return FindRoute(start, end)[1];
    }

    /// <summary>
    /// returns the route found from the start position to the end position
    /// </summary>
    public static Vector2[] FindRoute(Vector2 start, Vector2 end)
    {
        //I think this algorithm only takes linear time to finishe

        //if the end is up, go from lower to upper until in the same floor
        //if the end is down, go from upper to lower until in the same floor
        bool goUp = true;
        if (GetFloorNumber(end) > GetFloorNumber(start)) goUp = true;
        else if (GetFloorNumber(end) < GetFloorNumber(start)) goUp = false;
        else return new Vector2[] { start, end };

        bool SameFloor(Vector2 a, Vector2 b)
        {
            return GetFloorNumber(a) == GetFloorNumber(b);
        }

        List<Vector2> result = new List<Vector2>();
        result.Add(start);
        int from = 0, to = 0;

        //search from top to bot
        for (int i = instance.wayPoints.Length - 1; i >= 0; i--) {
            if (SameFloor(instance.wayPoints[i], goUp ? start : end)) {
                if (goUp) from = i;
                else to = i;
                break;
            }
        }
        //from bot to top
        for (int i = 0; i < instance.wayPoints.Length; i++) {
            if (SameFloor(instance.wayPoints[i], goUp ? end : start)) {
                if (goUp) to = i;
                else from = i;
                break;
            }
        }

        for (int i = from; i != to + (goUp ? 1 : -1); i += goUp ? 1 : -1) {
            result.Add(instance.wayPoints[i]);
        }

        result.Add(end);
        return result.ToArray();
    }

    public static int GetFloorNumber(Vector2 position)
    {
        for (int i = instance.floorMarker.Length - 1; i >= 0; i--) {
            if (position.y > instance.floorMarker[i].position.y)
                return i;
        }

        return -1;
    }

    /// <summary>
    /// returns the default floor direction for ghosts
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Vector2 GetFloorDirection(Vector2 position)
    {
        for (int i = instance.floorMarker.Length - 1; i >= 0; i--) {
            if (position.y > instance.floorMarker[i].position.y)
                return instance.floorMarker[i].toRight ? Vector2.right : Vector2.left;
        }

        return Vector2.zero;
    }

    public static void ContinueGame()
    {
        Time.timeScale = 1;
        EventManager.TriggerEvent(GameEvent.gameContinued);
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        EventManager.TriggerEvent(GameEvent.gamePaused);
    }

    public static void Coroutine(IEnumerator routine)
    {
        instance.StartCoroutine(routine);
    }

    public static void CreateHealthBar(UnitBehaviour unit)
    {
        GameObject newBar = Instantiate(instance.healthBar);
        newBar.GetComponent<HealthBar>().Initialize(unit);
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
