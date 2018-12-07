using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 장비 장착 관리 */
public class EquipmentManager : MonoBehaviour {

    /* EquipmentManager을 싱글톤으로 구성 */
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;       // 현재 장착중인 장비 아이템 배열
    Inventory inventory;                // 인벤토리 인스턴스
    GameObject rightHand;               // 캐릭터 오른손
    GameObject leftHand;                // 캐릭터 왼손
    GameObject[] curEquippedObj;        // 현재 장착중인 장비 object 배열

    private void Start()
    {
        inventory = Inventory.instance;

        // 캐릭터에 장비 장착 가능한 갯수
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        // currentEquipment, currentEquippedObj 배열 선언
        currentEquipment = new Equipment[numSlots];
        curEquippedObj = new GameObject[numSlots];

        // 캐릭터 오른손, 왼손 찾기
        rightHand = GameObject.FindGameObjectWithTag("right_thumb");
        leftHand = GameObject.FindGameObjectWithTag("left_thumb");
    }

    /* 장비 장착 */
    public void Equip(Equipment newItem)
    {
        // 새로운 아이템의 장비번호(왼손, 오른손)
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;   // 이전 착용 장비

        // 이미 장착 중일 때 새로운 장비 장착하고 이전에 착용했던 장비는 인벤토리칸에 추가
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            Destroy(curEquippedObj[slotIndex]);
        }

        // currentEquipment에 현재 장착중인 장비 추가
        currentEquipment[slotIndex] = newItem;

        // 왼손 장착일 때 현재 장착중인 object 배열에 추가하고 캐릭터 왼손에 object 생성
        if(slotIndex == 0)
        {
            curEquippedObj[slotIndex] = Instantiate(currentEquipment[slotIndex].equippedObject, leftHand.transform.position, Quaternion.identity);
            curEquippedObj[slotIndex].transform.parent = leftHand.transform;
            curEquippedObj[slotIndex].transform.position = leftHand.transform.position;
        }
        // 오른손 장착일 때 현재 장착중인 object 배열에 추가하고 캐릭터 오른손에 object 생성
        else
        {
            curEquippedObj[slotIndex] = Instantiate(currentEquipment[slotIndex].equippedObject, rightHand.transform.position, Quaternion.identity);
            curEquippedObj[slotIndex].transform.parent = rightHand.transform;
            curEquippedObj[slotIndex].transform.position = rightHand.transform.position;
        }
        
    }

    /* 장비 탈착 */
    public void Unequip(int slotIndex)
    {
        // 현재 장착중인 장비가 있으면
        if (currentEquipment[slotIndex] != null)
        {
            // 인벤토리 리스트에 다시 추가하고 게임화면에서 object 파괴
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            Destroy(curEquippedObj[slotIndex]);

            currentEquipment[slotIndex] = null;
        }
    }

    /* 모든 장비 탈착 */
    public void UnequipAll()
    {
        for(int i=0; i< currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    /* 아이템 사용완료 */
    public void UsedItem(int slotIndex)
    {
        // 인벤토리에서 사용한 아이템 삭제하고 currentEquipment 초기화
        Equipment usedItem = currentEquipment[slotIndex];
        inventory.Remove(usedItem);
        currentEquipment[slotIndex] = null;
    }

    private void Update()
    {
        /* U키를 눌렀을 때 모든 장비 탈착 */
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
