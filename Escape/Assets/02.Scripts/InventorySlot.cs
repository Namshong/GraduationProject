using UnityEngine;
using UnityEngine.UI;

/* 각 인벤토리 slot에서 실행되는 코드 - 화면에 뜨는 아이템 아이콘 추가 or 삭제 */
public class InventorySlot : MonoBehaviour {

    public Image icon;              // 아이템 아이콘
    public Button removeButton;     // 아이템 삭제 버튼

    Item item;                      // 추가될 아이템

    /* 추가된 아이템의 아이콘 추가 */
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        // 삭제 버튼 활성화
        removeButton.interactable = true;
    }

    /* 제거된 아이템의 아이콘 제거 */
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        // 삭제 버튼 비활성화
        removeButton.interactable = false;
    }

    /* 인벤토리 slot의 x버튼 눌렀을 때 item 제거함 */
    public void OnRemoveButton()
    {
        // 인벤토리에서 item 제거 동작
        Inventory.instance.Remove(item);
    }

    /* 인벤토리 slot 버튼 눌렀을 때 아이템 사용 */
    public void UseItem()
    {
        // 아이템이 없지않으면 아이템 사용
        if(item != null)
        {
            item.Use();
        }
    }
}
