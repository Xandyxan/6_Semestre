using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UserInterface : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryObject inventory;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    public GameObject descriptWindow;
    public Text descriptionText;


    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    protected virtual void Start()
    {
        if (inventory != null)
        {
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                inventory.Container.Items[i].parent = this;
            }
        }

        CreateSlots();
        //Important
        UtilsClass.AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        UtilsClass.AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });

        //Plus
        UtilsClass.AddEventTriggerListener(this.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, OnClickedOutSlot);
        UtilsClass.AddEventTriggerListener(this.GetComponent<EventTrigger>(), EventTriggerType.Deselect, OnDeselect);
        UtilsClass.AddEventTriggerListener(this.GetComponent<EventTrigger>(), EventTriggerType.Select, OnSelect);
    }

    protected virtual void Update()
    {
        slotsOnInterface.UpdateSlotDisplay();       //necessita refatoração
    }

    public abstract void CreateSlots();

    #region Inventory Slots Event Methods (StaticInterface & DynamicInterface)
    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
        descriptionText.text = slotsOnInterface[obj].itemObject.description;
    }

    public void OnClickedOnSlot(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            MouseData.descriptionWindow = descriptWindow;
            MouseData.descriptionWindow.SetActive(true);
            MouseData.descriptionWindow.transform.position = Input.mousePosition + new Vector3(1,1,1);
        }
        else
        {
            if(MouseData.descriptionWindow.activeSelf == true)
                MouseData.descriptionWindow.SetActive(false);
        }
    }

    public void OnClickedOnSlotObj(GameObject obj)
    {
        if (slotsOnInterface[obj].amount > 0)
        {
            MouseData.currentItemID = slotsOnInterface[obj].item.GetItemID();
            MouseData.interfaceSlot = slotsOnInterface[obj];
            //Debug.Log(slotsOnInterface[obj].amount);
        }
    }

    public void OnClickedOutSlot(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;

        if(pointerEventData.button == PointerEventData.InputButton.Left || pointerEventData.button == PointerEventData.InputButton.Right)
        {
            MouseData.descriptionWindow.SetActive(false);
        }
    }

    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
        descriptionText.text = "";
    }

    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }

    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;

        if (slotsOnInterface[obj].item.Id >= 0)
        {
            tempItem = new GameObject();

            RectTransform rectTransform = tempItem.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(90, 90);
            tempItem.transform.SetParent(transform.parent);

            Image image = tempItem.AddComponent<Image>();
            image.preserveAspect = true;
            image.sprite = slotsOnInterface[obj].itemObject.uiDisplay;
            image.color = new Color(1, 1, 1, 0.8f);
            image.raycastTarget = false;
        }

        return tempItem;
    }
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
        if (MouseData.interfaceMouseIsOver == null)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];  //get the DATA of the current Slot where the mouse is
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }

    }
    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    #endregion

    #region Other Event Methods Functionality
    public void OnSelect(BaseEventData eventData)
    {
        if (UnityEngine.EventSystems.EventSystem.current.alreadySelecting == false) EventSystem.current.SetSelectedGameObject(gameObject);
        //Debug.Log("Mouse is inside");
    }
    public void OnDeselect(BaseEventData eventData)
    {
        if (!MouseData.mouseIsOverUserUI && !MouseData.mouseIsOverUI)
            if(MouseData.descriptionWindow !=null) MouseData.descriptionWindow.SetActive(false);

        //Debug.Log("Mouse was clicked outside");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseData.mouseIsOverUserUI = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseData.mouseIsOverUserUI = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    #endregion

    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        //MouseData.interfaceMouseIsOverDynamic = obj.GetComponent<DynamicInterface>();
        //Debug.Log("Mouse ESTA NA UI");
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
        //MouseData.interfaceMouseIsOverDynamic = null;
        //if(MouseData.descriptionWindow != null) MouseData.descriptionWindow.SetActive(false);
        Debug.Log("Mouse SAIU NA UI");

        //MouseData.teste = false;
    }
}
public static class MouseData
{
    public static UserInterface interfaceMouseIsOver;
    public static InventorySlot interfaceSlot;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;
    public static GameObject descriptionWindow = new GameObject();

    public static bool mouseIsOverUserUI;
    public static bool mouseIsOverUI;

    public static int currentItemID;

    public static void Teste()
    {
        interfaceSlot.ConsumeItem(-1);
        MouseData.descriptionWindow.SetActive(false);
    }
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            if (_slot.Value.item.Id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.itemObject.uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<Text>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<Text>().text = "";
            }
        }
    }
}
