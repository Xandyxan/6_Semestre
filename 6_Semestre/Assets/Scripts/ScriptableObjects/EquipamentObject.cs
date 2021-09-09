using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipament Object", menuName = "Inventory System/Items/Equipament")]

public class EquipamentObject : ItemObject
{
    [SerializeField] private int speedValue;
    [SerializeField] private int defenseValue;

    private void Awake()
    {
        type = ItemType.Equipament;
    }
}
