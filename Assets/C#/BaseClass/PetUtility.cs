using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

    private static Ghost RaycastGhost(Vector2 start, Vector2 direction)
    {
        int ghost = LayerMask.GetMask("Ghost");
        Ghost result;
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(start, direction, float.PositiveInfinity, ghost);
        if (hit2D) {
            if ((result = hit2D.collider.GetComponent<Ghost>()) != null) return result;
        }
        return null;
    }

    public static Ghost GetUpstairGhost(Vector2 from)
    {
        int currentFloor = GetFloorNumber(from);
        //Debug.Log("current Floor: " + currentFloor);
        int sampleSize = 3;
        float floorHeight = instance.floorMarker[1].position.y - instance.floorMarker[0].position.y;
        float offSet = floorHeight / (sampleSize + 1);
        Ghost result;

        //first lets check the same floor

        result = RaycastGhost(from, GetFloorDirection(from));
        if (result != null) {
            Debug.Log("enemy found: " + result.name);
            return result;
        }
        for (int i = currentFloor + 1; i < instance.floorMarker.Length; i++) {
            //Debug.Log("try find ghost on top");
            for (int j = 0; j < sampleSize; j++) {
                //shot 5 ray to make sure no one is missing
                Vector2 currentShooter = instance.floorMarker[i].position + Vector2.up * j * offSet;
                if ((result = RaycastGhost(currentShooter, Vector2.right)) != null) return result;
            }
        }

        //if the raycast is missing something
        result = (Ghost)FindObjectOfType(typeof(Ghost));
        if (result == null) return null;
        if (GetFloorNumber(result.transform.position) > currentFloor) return result;
        else if (GetFloorNumber(result.transform.position) == currentFloor) {
            float dx = result.transform.position.x - from.x;
            //Debug.Log(dx + ", " + GetFloorDirection(from).x);
            //Debug.Log(dx * GetFloorDirection(from).x > 0);

            if (dx * GetFloorDirection(from).x > 0) return result;
            else return null;
        } else return null;
    }

    public static Ghost GetDownstairGhost(Vector2 from)
    {
        Ghost result;
        int currentFloor = GetFloorNumber(from);
        int sampleSize = 3;
        float floorHeight = instance.floorMarker[1].position.y - instance.floorMarker[0].position.y;
        float offSet = floorHeight / (sampleSize + 1);

        result = RaycastGhost(from, -GetFloorDirection(from));
        if (result != null) return result;

        for (int i = currentFloor - 1; i >= 0; i--) {
            for (int j = 0; j < sampleSize; j++) {
                //shot 5 ray to make sure no one is missing
                Vector2 currentShooter = instance.floorMarker[i].position + Vector2.up * j * offSet;
                if ((result = RaycastGhost(currentShooter, Vector2.right)) != null) return result;
            }

        }
        result = (Ghost)FindObjectOfType(typeof(Ghost));
        if (result == null) return null;
        if (GetFloorNumber(result.transform.position) > currentFloor) return result;
        else if (GetFloorNumber(result.transform.position) == currentFloor) {
            float dx = result.transform.position.x - from.x;
            if (dx * GetFloorDirection(from).x < 0) return result;
            else return null;
        } else return null;
    }



    /// <summary>
    /// returns the nearest ghost on floor basis
    /// return null if nothing is found
    /// </summary>
    public static Ghost GetNearestGhost(Vector2 from)
    {
        //cast a ray to find out?
        Ghost result;
        if ((result = GetDownstairGhost(from)) == null) result = GetUpstairGhost(from);
        return result;
        //return null;
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

    public static IEnumerator SublinearMove(Vector2 from, Vector2 to, float duration, Transform target)
    {
        float timer = 0f;
        while (timer <= duration) {
            Vector2 current = (to - from) * Mathf.Sqrt(timer / duration) + from;
            target.position = current;
            timer += Time.deltaTime;
            yield return null;
        }
        target.position = to;
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

    public void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
    }
}
