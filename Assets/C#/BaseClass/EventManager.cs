using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//use this script as the center of communication between scripts;
//event manager for passing one float/byte/string parameter
using System.Collections.ObjectModel;
using System.Linq;
public class EventManager : MonoBehaviour
{
    private Dictionary<GameEvent, UnityEvent> eventDictionary;
    private Dictionary<string, UnityEvent> customEventDictionary;
    //private Dictionary<EditorEvent, UnityEvent> editorEventDictionary;
    private Dictionary<ParameterizedGameEvent, ObjEvent> objEventDictionary; //just in case if I need to pass some parameters to the event

    private static EventManager eventManager;

    public List<string> GetCustomEvents()
    {

        return instance.customEventDictionary.Keys.ToList();
    }

    public static EventManager instance
    {
        get
        {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) {
                    Debug.LogError("no eventmanager");
                } else {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<GameEvent, UnityEvent>();
        }

        if (customEventDictionary == null) {
            customEventDictionary = new Dictionary<string, UnityEvent>();
        }

        //if (editorEventDictionary == null) {
        //	editorEventDictionary = new Dictionary<EditorEvent, UnityEvent>();
        //}

        if (objEventDictionary == null) {
            objEventDictionary = new Dictionary<ParameterizedGameEvent, ObjEvent>();
        }
    }

    public static void StartListening(ParameterizedGameEvent eventName, UnityAction<object> listener)
    {
        ObjEvent thisEvent = null;
        if (instance.objEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new ObjEvent();
            thisEvent.AddListener(listener);
            instance.objEventDictionary.Add(eventName, thisEvent);
        }
    }

    //no parameter
    public static void StartListening(GameEvent eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.customEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.customEventDictionary.Add(eventName, thisEvent);
        }
    }

    //public static void StartListening(EditorEvent eventName, UnityAction listener)
    //{
    //	UnityEvent thisEvent = null;
    //	if (instance.editorEventDictionary.TryGetValue(eventName, out thisEvent)) {
    //		thisEvent.AddListener(listener);
    //	} else {
    //		thisEvent = new UnityEvent();
    //		thisEvent.AddListener(listener);
    //		instance.editorEventDictionary.Add(eventName, thisEvent);
    //	}

    //}

    public static void StopListening(ParameterizedGameEvent eventName, UnityAction<object> listener)
    {
        if (eventManager == null) return;
        ObjEvent thisEvent = null;
        if (instance.objEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(GameEvent eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.customEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    //public static void StopListening(EditorEvent eventName, UnityAction listener)
    //{
    //	if (eventManager == null) return;
    //	UnityEvent thisEvent = null;
    //	if (instance.editorEventDictionary.TryGetValue(eventName, out thisEvent)) {
    //		thisEvent.RemoveListener(listener);
    //	}

    //}

    public static void TriggerEvent(ParameterizedGameEvent eventName, object objParameter)
    {
        ObjEvent thisEvent = null;
        if (instance.objEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke(objParameter);
        }
    }

    public static void TriggerEvent(GameEvent eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke();
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.customEventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke();
        }
    }


    //public static void TriggerEvent(EditorEvent eventName)
    //{
    //	UnityEvent thisEvent = null;
    //	if (instance.editorEventDictionary.TryGetValue(eventName, out thisEvent)) {
    //		thisEvent.Invoke();
    //	}

    //}
}

public class ObjEvent : UnityEvent<object> { }

public enum GameEvent
{
    interact,
    controlEnable,
    controlDisable,
    abilityOn,
    abilityOff,
    loadArea, //this event must be called AFTER the player is teleported
    fadeOut, //fade out the scene, fade in the black screen
    fadeIn, //fade out the black screen so the player can see the scene
    switchTimeline,
    updatePhysics,
    openInventory, //open up the inventory interface
    levelFinished
}

public enum ParameterizedGameEvent
{
    select, //byte
    switchControl,//Gameobject
    doorOpen, //door type object
    damageReceive, //int
    getObject //(String) object name, tell the inventory manager that we got something
}

//these are all the events happening in the editor mode
public enum EditorEvent
{
    gridChanged,
}