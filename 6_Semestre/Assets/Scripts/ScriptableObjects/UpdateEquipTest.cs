using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEquipTest : MonoBehaviour
{
    public StaticInterface equipamentInterface;
    public PlayerStats playerStats;
    private GameObject slotItemLamp;

    // Start is called before the first frame update
    void Start()
    {
        slotItemLamp = equipamentInterface.slots[0];
        //slotInventory = equipamentInterface.slots[0].GetComponent<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (equipamentInterface.slotsOnInterface[slotItemLamp].item.Id == 1)
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
