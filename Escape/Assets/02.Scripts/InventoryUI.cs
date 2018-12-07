using UnityEngine;

/* 인벤토리 UI 관리하는 코드 */
public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;       // 인벤토리 slot들의 부모 
    public GameObject inventoryUI;      // 인벤토리 UI object
    Inventory inventory;                // 인벤토리 인스턴스

    InventorySlot[] slots;              // 인벤토리 각 slot들

	/* 초기 설정 */
	void Start () {
        inventory = Inventory.instance;

        // UpdateUI를 참조하게 함
        inventory.OnItemChangedCallback += UpdateUI;

        // 인벤토리 slot들의 컴포넌트를 가져옴
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	/* Update is called once per frame */
	void Update () {
        // inventory로 설정된 키I를 누를 경우
        if (Input.GetButtonDown("inventory"))
        {
            // 인벤토리 UI 활성화 or 비활성화
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
	}

    /* 인벤토리 UI 업데이트 */
    void UpdateUI()
    {
        for(int i=0;i<slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                // 인벤토리 slot에 아이템 추가
                slots[i].AddItem(inventory.items[i]);
            }else
            {
                // 인벤토리 slot에 아이템 제거
                slots[i].ClearSlot();
            }
        }
    }
}
