using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceExtension : MonoBehaviour
{
    public void ConsumeItem()
    {
        MouseData.descriptionWindow.SetActive(false);
        MouseData.interfaceSlot.ConsumeItem(MouseData.interfaceSlot.parent.playerStats, -1);
    }
}
