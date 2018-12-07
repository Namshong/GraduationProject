using UnityEngine;

/* 아이템을 주웠을 때 */
public class ItemPickup : MonoBehaviour {

    public Item item;       // 아이템 변수

    /* trigger중인 object가 캐릭터이고 E버튼 눌렀을 때 */
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && Input.GetKeyDown("e"))
            // 아이템 줍기
            PickUp();
    }

    /* 아이템 줍기 */
    void PickUp()
    {
        Debug.Log("Picking up " + item.name);

        // 주운 아이템 인벤토리에 추가
        bool wasPickedUp = Inventory.instance.Add(item);

        // 주운 아이템을 게임 화면에서 삭제
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
