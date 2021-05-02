using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehavior : MonoBehaviour
{
    public Queue<GameObject> itemQueue;

    private Vector2 firstItemVector;

    private int maxItem;

    // Start is called before the first frame update
    void Start()
    {
        Initiate();
    }
    
    public void UsingItem()
    {
        if (itemQueue.Count == 0)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetOutItem();
        }
    }

    void GetOutItem()
    {
        // 아이템을 꺼낸다.
        GameObject peekItem = itemQueue.Peek();
        string peekItemName = peekItem.GetComponent<SpriteRenderer>().sprite.name;

        Defines.SpeciesofItem itemName = (Defines.SpeciesofItem)System.Enum.Parse(typeof(Defines.SpeciesofItem), peekItemName);

        // 해당 아이템이 사용되고 있는지 확인한다.
        if (Managers.Items.CheckItemUsed(itemName) == false)
        {
            Debug.Log(itemName);
            // 아이템을 사용한다는 Action 함수를 부른다.
            Managers.Items.OnItemUsed.Invoke(itemName);

            // 아이템을 아이템 큐에서 빼내고, 파괴한다.
            itemQueue.Dequeue();
            Destroy(peekItem);
            int queueCnt = 0;

            // 아이템 인벤토리를 한칸씩 내려 정렬한다.
            foreach (GameObject gameObject in itemQueue)
            {
                gameObject.transform.localPosition = SortingItemPosition(queueCnt);
                queueCnt++;
            }
        }
    }

    private void Initiate()
    {
        itemQueue = new Queue<GameObject>();
        firstItemVector = new Vector2(-0.4f, 0);
        maxItem = 5;
        Managers.Items.OnPlayerGetItem += EnrollForItem;
        Managers.Input.KeyboardInput += UsingItem;
    }

    public void EnrollForItem(GameObject targetItem)
    {
        if (itemQueue.Count < maxItem)
        {
            itemQueue.Enqueue(targetItem);
            targetItem.transform.localScale = new Vector2(0.5f, 0.5f);
            targetItem.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            targetItem.transform.parent = this.transform;
            targetItem.transform.localPosition = SortingItemPosition(itemQueue.Count - 1);
        }
        else
        {
            Destroy(targetItem);
        }
    }

    private Vector2 SortingItemPosition(int extra)
    {
        return firstItemVector + (new Vector2(0.2f,0) * extra);
    }
}
