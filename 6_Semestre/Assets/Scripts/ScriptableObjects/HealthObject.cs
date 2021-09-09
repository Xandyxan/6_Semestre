using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Health Object", menuName = "Inventory System/Items/Health")]

public class HealthObject : ItemObject
{
    [SerializeField] private int healthValue;

    private void Awake()
    {
        type = ItemType.Food;
    }
}
