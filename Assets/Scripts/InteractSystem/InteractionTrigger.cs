using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [HideInInspector] public List<Interaction> interactions;

    public GameObject CurrentObjectInteract { get; set; } = null;

    private void OnTriggerEnter(Collider other)
    {
        if (interactions.Find(x => x.triggerType == TriggerType.ON_OBJECT_ENTER) == null)
            return;
        ObjectTag otherTag = StringTagToEnum(other.tag);
        if (interactions.Find(x => x.concernedObject == otherTag) == null)
            return;
        CurrentObjectInteract = other.gameObject;
        List<Interaction> interactionsTriggered = interactions.FindAll(x => x.triggerType == TriggerType.ON_OBJECT_ENTER);
        interactionsTriggered = interactionsTriggered.FindAll(x => x.concernedObject == otherTag);
        foreach(var item in interactionsTriggered)
        {
            item.triggeredEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsTriggerStayRelatedEvent)
            return;
        ObjectTag otherTag = StringTagToEnum(other.tag);
        if (interactions.Find(x => x.concernedObject == otherTag) == null)
            return;
        CurrentObjectInteract = other.gameObject;
        List<Interaction> interactionsTriggered = interactions.FindAll(x => x.triggerType == TriggerType.ON_OBJECT_STAY);
        interactionsTriggered = interactionsTriggered.FindAll(x => x.concernedObject == otherTag);
        foreach (var item in interactionsTriggered)
        {
            item.triggeredEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactions.Find(x => x.triggerType == TriggerType.ON_OBJECT_EXIT) == null)
            return;
        ObjectTag otherTag = StringTagToEnum(other.tag);
        if (interactions.Find(x => x.concernedObject == otherTag) == null)
            return;
        CurrentObjectInteract = other.gameObject;
        List<Interaction> interactionsTriggered = interactions.FindAll(x => x.triggerType == TriggerType.ON_OBJECT_EXIT);
        interactionsTriggered = interactionsTriggered.FindAll(x => x.concernedObject == otherTag);
        foreach (var item in interactionsTriggered)
        {
            item.triggeredEvent.Invoke();
        }
    }

    public void OnPlayerInteractInput()
    {
        List<Interaction> interactionsTriggered = interactions.FindAll(x => x.triggerType == TriggerType.ON_PLAYER_INTERACT);
        foreach (var item in interactionsTriggered)
        {
            item.triggeredEvent.Invoke();
        }
    }

    private ObjectTag StringTagToEnum(string value)
    {
        if (value == "Player")
            return ObjectTag.Player;
        return ObjectTag.Default;
    }

    private bool IsTriggerStayRelatedEvent
    {
        get
        {
            return (interactions.Find(x => x.triggerType == TriggerType.ON_OBJECT_STAY) != null)
                || (interactions.Find(x => x.triggerType == TriggerType.ON_PLAYER_INTERACT) != null)
                || (interactions.Find(x => x.triggerType == TriggerType.ON_PLAYER_INTERACT_RANDOM) != null);
        }
    }

    public List<Interaction> Interactions { get => interactions; set => interactions = value; }
}

public enum TriggerType
{
    ON_OBJECT_ENTER,
    ON_OBJECT_STAY,
    ON_OBJECT_EXIT,
    ON_PLAYER_INTERACT,
    ON_PLAYER_INTERACT_RANDOM
}

public enum ObjectTag
{
    Player,
    Default
}

[System.Serializable]
public class Interaction
{
    public ObjectTag concernedObject;
    public TriggerType triggerType;

    public UnityEvent triggeredEvent;

    public Interaction()
    {
        concernedObject = ObjectTag.Default;
        triggerType = TriggerType.ON_OBJECT_ENTER;
    }
}
