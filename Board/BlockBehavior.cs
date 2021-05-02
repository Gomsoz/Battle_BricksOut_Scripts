using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    public GameObject _itemInstance;

    public int _blockSpeed = 100;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            DestroyBlock();
        }
    }

    public void DestroyBlock()
    {
        if (_itemInstance != null)
        {
            _itemInstance.GetComponent<Rigidbody2D>().velocity = Vector2.down * _blockSpeed * Time.deltaTime;
            _itemInstance.transform.SetParent(null);
        }
        this.gameObject.SetActive(false);
    }
}
