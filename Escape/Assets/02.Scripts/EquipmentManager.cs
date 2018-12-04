using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;
    Inventory inventory;
    GameObject rightHand;
    GameObject leftHand;
    GameObject[] curEquippedObj;

    private void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        curEquippedObj = new GameObject[numSlots];

        rightHand = GameObject.FindGameObjectWithTag("right_thumb");
        leftHand = GameObject.FindGameObjectWithTag("left_thumb");
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        // 이미 장착 중일 때 
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            Destroy(curEquippedObj[slotIndex]);
        }

        currentEquipment[slotIndex] = newItem;
        // 왼손 장착일 때
        if(slotIndex == 0)
        {
            curEquippedObj[slotIndex] = Instantiate(currentEquipment[slotIndex].equippedObject, leftHand.transform.position, Quaternion.identity);
            curEquippedObj[slotIndex].transform.parent = leftHand.transform;
            curEquippedObj[slotIndex].transform.position = leftHand.transform.position;
        }
        // 오른손 장착일 때
        else
        {
            curEquippedObj[slotIndex] = Instantiate(currentEquipment[slotIndex].equippedObject, rightHand.transform.position, Quaternion.identity);
            curEquippedObj[slotIndex].transform.parent = rightHand.transform;
            curEquippedObj[slotIndex].transform.position = rightHand.transform.position;
        }
        
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            Destroy(curEquippedObj[slotIndex]);

            currentEquipment[slotIndex] = null;
        }
    }

    public void UnequipAll()
    {
        for(int i=0; i< currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    public void UsedItem(int slotIndex)
    {
        Equipment usedItem = currentEquipment[slotIndex];
        inventory.Remove(usedItem);
        currentEquipment[slotIndex] = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
