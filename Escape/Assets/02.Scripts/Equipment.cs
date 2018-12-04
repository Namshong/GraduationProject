using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public GameObject equippedObject;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        // Remove the item from inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { LeftHand, RightHand }
