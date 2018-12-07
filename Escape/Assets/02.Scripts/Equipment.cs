using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
/* item을 상속받는 Equipment */
public class Equipment : Item {

    public EquipmentSlot equipSlot;     // 착용하는 장비 slot
    public GameObject equippedObject;   // 착용할 장비 object

    public override void Use()
    {
        base.Use();
        
        // EquipmentManager에 있는 착용하는 함수 호출
        EquipmentManager.instance.Equip(this);

        // 인벤토리에서 장비 착용했을 경우, 인벤토리에서 삭제
        RemoveFromInventory();
    }
}

// 장비가 어디에 착용되는지 구분할 열거형 (왼손, 오른손)
public enum EquipmentSlot { LeftHand, RightHand }