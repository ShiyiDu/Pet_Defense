using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controls the input of the game
public class InputManager : MonoBehaviour
{
    private static bool touchPressed = false;
    private static bool mousePressed = false;//if anything is pressed right now
    private static Vector2 lastPosition;//the position of the mouse/touch hit
    private static Vector2 deltaPosition;
    private bool paused = false;

    public static Vector2 GetDeltaPos()
    {
        return deltaPosition;
    }

    //returns the coordinates in screen space for the input
    public static Vector2 GetPos()
    {
        Vector2 result = Vector2.zero;

        if (touchPressed) {
            result = Input.touches[0].position;
        } else {
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
        return Input.GetMouseButtonDown(0) || TouchBegan();
        //when a mouse or a finger is held in the screen
    }

    public static bool InputRelease()
    {
        return Input.GetMouseButtonUp(0) || TouchEnded();
    }

    private static bool TouchBegan()
    {
        return InputTouchExist() && Input.touches[0].phase == TouchPhase.Began;
    }

    private static bool TouchEnded()
    {
        return InputTouchExist() && Input.touches[0].phase == TouchPhase.Ended;
    }

    private static bool InputTouchExist()
    {
        return Input.touches.Length > 0;
    }

    private static void InputUpdate()
    {
        deltaPosition = GetPos() - lastPosition;
        lastPosition = GetPos();
        if (InputPush()) {
            Selectable hit = GetHit();
            if (hit != null) hit.Selected();
        } else if (InputRelease()) {
            Selectable hit = GetHit();
            if (hit != null) hit.Unselected();
        }
    }

    private static Selectable GetHit()
    {
        //Debug.Log("input change");

        RaycastHit2D hit2D;

        Ray ray = Camera.main.ScreenPointToRay(GetPos());

        hit2D = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit2D) {
            return hit2D.transform.gameObject.GetComponent<Selectable>();
        } else {
            return null;
        }
    }

    void GamePaused()
    {
        paused = true;
    }

    void GameContinue()
    {
        paused = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (paused) return;
        if (TouchBegan()) {
            touchPressed = true;

        } else if (TouchEnded()) {
            touchPressed = false;
        }

        if (Input.GetMouseButtonDown(0)) {
            mousePressed = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            mousePressed = false;
        }

        InputUpdate();
    }

    private void OnEnable()
    {
        EventManager.StartListening(GameEvent.gamePaused, GamePaused);
        EventManager.StartListening(GameEvent.gameContinued, GameContinue);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameEvent.gamePaused, GamePaused);
        EventManager.StopListening(GameEvent.gameContinued, GameContinue);
    }
}
