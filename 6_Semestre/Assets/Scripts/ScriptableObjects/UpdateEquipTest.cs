using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEquipTest : MonoBehaviour
{
    public StaticInterface equipamentInterface;
    public PlayerStats playerStats;
    private GameObject slotItemLamp;
    public int lamparinaID;

    private void Start()
    {
        slotItemLamp = equipamentInterface.slots[0];
        //slotInventory = equipamentInterface.slots[0].GetComponent<InventorySlot>();
    }

    private void Update()
    {
        if (equipamentInterface.isActiveAndEnabled)
        {
            if (equipamentInterface.slotsOnInterface[slotItemLamp].item.Id == lamparinaID)
            {
                equipamentInterface.lamparinaItem.SetActive(true);
                playerStats.isUsingLamparina = true;
            }
            else
            {
                equipamentInterface.lamparinaItem.SetActive(false);
                playerStats.isUsingLamparina = false;
            }
        }
    }
}
