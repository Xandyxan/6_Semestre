using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DocumentsInterface : UserInterface
{
    public GameObject inventoryPrefab;
    [SerializeField] private Text transcricaoText;

    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-7.5f, -9, 0);

            //UtilsClass.AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            //UtilsClass.AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            //UtilsClass.AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            //UtilsClass.AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            //UtilsClass.AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });


            UtilsClass.AddEvent(obj, EventTriggerType.PointerClick, delegate { this.OnClickedOnSlotObj(obj); });
            //UtilsClass.AddEventTriggerListener(obj.GetComponent<EventTrigger>(), EventTriggerType.PointerClick, OnClickedOnSlot);

            slotsOnInterface.Add(obj, inventory.Container.Items[i]);
        }
    }

    protected override void Update()
    {
        slotsOnInterface.UpdateDocumentSlotDisplay();
    }

    public override void OnClickedOnSlotObj(GameObject obj)
    {
        if (slotsOnInterface[obj] != null)
        {
            if (slotsOnInterface[obj].amount > 0)
            {
                transcricaoText.text = slotsOnInterface[obj].itemObject.description;
                if (MouseData.actualDocumentSelected != null) MouseData.actualDocumentSelected.color = new Color(0.7333333f, 0.7176471f, 0.6392157f, 1);
                MouseData.actualDocumentSelected = obj.GetComponent<Image>();
                MouseData.actualDocumentSelected.color = Color.black;
            }
            else
            {

            }
        }
    }

    public void DeselectAllDocuments()
    {
        if (MouseData.actualDocumentSelected != null) MouseData.actualDocumentSelected.color = new Color(0.7333333f, 0.7176471f, 0.6392157f, 1);
        MouseData.actualDocumentSelected = null;
        transcricaoText.text = "";
    }
}
