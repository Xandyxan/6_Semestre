using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;
    
    [SerializeField] public GameObject lamparinaItem;

    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();       //Confirms that there is no old Dictionary by creating a new one


        for(int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = slots[i];

            UtilsClass.AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            UtilsClass.AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            UtilsClass.AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            UtilsClass.AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            UtilsClass.AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            UtilsClass.AddEventTriggerListener(obj.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, OnClickedOnSlot);

            slotsOnInterface.Add(obj, inventory.Container.Items[i]);
        }
    }
}
