using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {
    
    //dictionary of the event manager: name of the event, function to call
    private Dictionary<string, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance {
        get {
            if (!eventManager) {
                //event manager must be attached to a GameObject
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
            } else {
                eventManager.Init();
            }
            return eventManager;
        }
    }

    private void Awake() {
        instance.Init();
    }

    //initiate the new dictionary
    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    /// <summary>
    /// start listening to events
    /// </summary>
    /// <param name="eventName">name of the event</param>
    /// <param name="listener">function to call when the event is fired</param>
    public static void StartListening(string eventName, UnityAction listener) {

        UnityEvent thisEvent = null;

        //if the event is already in the dictionary we add a listener to it otherwise we create a new event
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// stop listening to an event
    /// </summary>
    /// <param name="eventName">name of the event</param>
    /// <param name="listener">fucntion to call when the event is fired</param>
    public static void StopListening(string eventName, UnityAction listener) {

        if(eventManager == null) {
            return;
        }

        UnityEvent thisEvent = null;

        //if the event is found the listener is removed
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// function that triggers the event eventName
    /// </summary>
    /// <param name="eventName">name of the event</param>
    public static void TriggerEvent(string eventName) {

        UnityEvent thisEvent = null;
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent.Invoke();
        }
    }
}
