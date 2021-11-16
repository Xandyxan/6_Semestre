using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Resource Object", menuName = "Scriptbale Objects/Inventory System/Items/Resource")]

public class ResourceObject : ItemObject
{
    [SerializeField] private float amountToFill;

    public override void UseItem(PlayerStats playerStats)
    {
        playerStats.FillLamparina(amountToFill);
    }

    private void Awake()
    {
        type = ItemType.Resource;
    }
}
