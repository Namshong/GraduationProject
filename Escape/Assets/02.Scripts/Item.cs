using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
/* scriptable Object : 인스턴스로부터 독립한 대량의 공유 데이터를 저장 */
public class Item : ScriptableObject {

    new public string name = "New Item";        // 아이템 이름
    public Sprite icon = null;                  // 아이템 아이콘
    public bool isDefaultItem = false;          // 기본 아이템인지

    /* 아이템 사용 */
    public virtual void Use()
    {
        // Use the item
        Debug.Log("Using " + name);
    }

    /* 사용한 아이템 인벤토리에서 삭제 - Equipment에서 사용 */
    public void RemoveFromInventory()
    {
        // 인벤토리에서 삭제
        Inventory.instance.Remove(this);
    }
}
