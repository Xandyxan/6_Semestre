using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;

    [SerializeField] private float X_START_POINT;
    [SerializeField] private float Y_START_POINT;
    [SerializeField] private float X_SPACE_BETWEEN_ITEM;
    [SerializeField] private float Y_SPACE_BETWEEN_ITEM;
    [SerializeField] private int NUMBER_OF_COLUMN;
    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.tag = "SlotInterface";

            UtilsClass.AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            UtilsClass.AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            UtilsClass.AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            UtilsClass.AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            UtilsClass.AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            UtilsClass.AddEventTriggerListener(obj.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, OnClickedOnSlot);
            UtilsClass.AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClickedOnSlotObj(obj); });

            slotsOnInterface.Add(obj, inventory.Container.Items[i]);
        }
    }
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START_POINT + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START_POINT + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
