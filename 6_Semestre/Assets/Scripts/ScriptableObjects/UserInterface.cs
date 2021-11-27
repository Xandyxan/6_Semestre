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

    [Header("UI GameObjects")]
    [SerializeField] private GameObject _descriptWindow;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _itemName;

    [SerializeField] private PlayerStats _playerStats;

    public PlayerStats playerStats { get => _playerStats; set => _playerStats = value; }
    private bool isDragging;

    public AcucenaPuzzle acucenaPuzzle;
    public FruteiraInteraction fruteiraInteraction;
    public VirgemMariaInteraction mariaInteraction;


    private void Awake()
    {
        //EventSystem.current.SetSelectedGameObject(gameObject);
        isDragging = false;
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
        UtilsClass.AddEventTriggerListener(this.GetComponent<EventTrigger>(), EventTriggerType.Deselect, OnDeselect);
        UtilsClass.AddEventTriggerListener(this.GetComponent<EventTrigger>(), EventTriggerType.Select, OnSelect);
        UtilsClass.AddEventTriggerListener(this.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, OnClickedOutSlot);
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
        

        if (slotsOnInterface[obj].itemObject != null)
        {
            _descriptionText.text = slotsOnInterface[obj].itemObject.description;
            _itemName.text = slotsOnInterface[obj].itemObject.data.Name;
        }
    }

    public void OnClickedOnSlot(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;

        if (MouseData.interfaceSlot != null)
        {
            if (MouseData.interfaceSlot.itemObject.stackbable)
            {
                if (pointerEventData.button == PointerEventData.InputButton.Right)
                {
                    MouseData.descriptionWindow = _descriptWindow;
                    MouseData.descriptionWindow.SetActive(true);
                    MouseData.descriptionWindow.transform.position = Input.mousePosition + new Vector3(1, 1, 1);
                }
                else
                {
                    if (MouseData.descriptionWindow.activeSelf == true)
                        MouseData.descriptionWindow.SetActive(false);
                }
            }
        }
    }

    public virtual void OnClickedOnSlotObj(GameObject obj)
    {
        if (slotsOnInterface[obj].amount > 0)
        {
            MouseData.currentItemID = slotsOnInterface[obj].item.GetItemID();
            MouseData.interfaceSlot = slotsOnInterface[obj];

            //Debug.LogError(slotsOnInterface[obj].amount);
        }
        else
        {
            Debug.LogError("Do not have itens on this slot");
        }
    }

    public void OnClickedOutSlot(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;

        if (pointerEventData.button == PointerEventData.InputButton.Left || pointerEventData.button == PointerEventData.InputButton.Right)
        {
            MouseData.descriptionWindow.SetActive(false);
            MouseData.interfaceSlot = null;
        }
    }

    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
        if (!isDragging)
        {
            _descriptionText.text = "";
            _itemName.text = "";
        }
    }

    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
        isDragging = true;
    }

    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;

        if (slotsOnInterface[obj].item.Id >= 0)
        {
            tempItem = new GameObject();

            RectTransform rectTransform = tempItem.AddComponent<RectTransform>();
            rectTransform.rect.Set(rectTransform.position.x, rectTransform.position.y, 64, 64);
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
        if (MouseData.interfaceMouseIsOver == null && GameManager.instance.isOnPuzzle == false)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        else if(MouseData.interfaceMouseIsOver == null && GameManager.instance.isOnPuzzle)
        {
            if(GameManager.instance.puzzleNumber == 0)
            {
                if(slotsOnInterface[obj].GetItemIDFromSlot() == 9)
                {
                    acucenaPuzzle.ActivateFlowers(0);
                    slotsOnInterface[obj].RemoveItem();
                }
                else if(slotsOnInterface[obj].GetItemIDFromSlot() == 13)
                {
                    acucenaPuzzle.ActivateFlowers(1);
                    slotsOnInterface[obj].RemoveItem();
                }
                else if(slotsOnInterface[obj].GetItemIDFromSlot() == 12)
                {
                    acucenaPuzzle.ActivateFlowers(2);
                    slotsOnInterface[obj].RemoveItem();
                }
                acucenaPuzzle.CheckFlowers();
            }
            else if(GameManager.instance.puzzleNumber == 1)
            {
                if (slotsOnInterface[obj].GetItemIDFromSlot() == 14)
                {
                    fruteiraInteraction.FinishPuzzle();
                    slotsOnInterface[obj].RemoveItem();
                }
            }
            else if(GameManager.instance.puzzleNumber == 2)
            {
                if (slotsOnInterface[obj].GetItemIDFromSlot() == 8)
                {
                    mariaInteraction.RunCoroutine();
                    slotsOnInterface[obj].RemoveItem();
                }
            }
            
        }


        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];  //get the DATA of the current Slot where the mouse is
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
        }

        isDragging = false;
        _descriptionText.text = "";
        _itemName.text = "";
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
            if (MouseData.descriptionWindow != null) MouseData.descriptionWindow.SetActive(false);

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
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
        Debug.Log("Mouse SAIU NA UI");
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

    public static Image actualDocumentSelected;
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            Image slotImage = _slot.Key.transform.GetChild(0).GetComponent<Image>();
            Text slotCountItem = _slot.Key.transform.GetChild(0).GetChild(0).GetComponent<Text>();

            if (_slot.Value.item.Id >= 0)
            {
                slotImage.sprite = _slot.Value.itemObject.uiDisplay;
                slotImage.color = new Color(1, 1, 1, 1);
                slotCountItem.text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                slotImage.sprite = null;
                slotImage.color = new Color(1, 1, 1, 0);
                slotCountItem.text = "";
            }
        }
    }

    public static void UpdateDocumentSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            Image slotImage = _slot.Key.transform.GetChild(0).GetComponent<Image>();
            Text slotCountItem = _slot.Key.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            Text documentName = _slot.Key.transform.GetChild(1).GetComponent<Text>();
            Text authorName = _slot.Key.transform.GetChild(2).GetComponent<Text>();

            if (_slot.Value.item.Id >= 0)
            {
                slotImage.sprite = _slot.Value.itemObject.uiDisplay;
                slotImage.color = new Color(1, 1, 1, 1);
                slotCountItem.text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                documentName.text = _slot.Value.itemObject.data.Name;
                authorName.text = _slot.Value.itemObject.author;
            }
            else
            {
                slotImage.sprite = null;
                slotImage.color = new Color(1, 1, 1, 0);
                slotCountItem.text = "";
                documentName.text = "";
                authorName.text = "";
            }
        }
    }
}
