using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItsInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //This script prevents that other related UI in the game makes some other UI's dissapears
    //Attach to the UI gameobject with a Event Trigger to make it works

    private void Start()
    {
        //UtilsClass.AddEventTriggerListener(this.GetComponent<EventTrigger>(), EventTriggerType.Deselect, OnDeselect);
        UtilsClass.AddEventTriggerListener(this.GetComponent<EventTrigger>(), EventTriggerType.Select, OnSelect);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (UnityEngine.EventSystems.EventSystem.current.alreadySelecting == false) EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseData.mouseIsOverUI = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseData.mouseIsOverUI = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
