using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Health Object", menuName = "Scriptbale Objects/Inventory System/Items/Health")]

public class HealthObject : ItemObject
{
    [SerializeField] private int healthValue;

    public override void UseItem(PlayerStats playerStats)
    {
        playerStats.insanityValue += healthValue;
        Debug.Log("Item de cura especifico");
    }

    private void Awake()
    {
        type = ItemType.Food;
    }
}
