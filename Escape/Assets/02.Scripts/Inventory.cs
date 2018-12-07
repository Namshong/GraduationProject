using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 인벤토리의 저장소 구성 */
public class Inventory : MonoBehaviour {

    // 인벤토리 싱글톤으로 구성
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There's more than one instance.");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();           // 아이템 변경 감지하는 delegate 선언
    public OnItemChanged OnItemChangedCallback;     // 위에 선언한 delegate변수 인스턴스화

    public int space = 5;                           // 인벤토리 공간

    public List<Item> items = new List<Item>();     // 아이템 리스트

    /* 인벤토리 리스트에 아이템 추가 */
    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            // 아이템 리스트에 새로운 아이템 추가
            items.Add(item);

            // UpDate UI 호출
            if(OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }

        return true;
    }

    /* 인벤토리 리스트에 아이템 제거 */
    public void Remove(Item item)
    {
        // 아이템 리스트에서 아이템 제거
        items.Remove(item);

        // UpDate UI 호출
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
