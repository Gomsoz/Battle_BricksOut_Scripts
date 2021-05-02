using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRgbd;

    public int playerSpeed;
    public Vector2 playerMoveVector;

    void Start()
    {
        Initiate();
    }

    public void Initiate()
    {
        Managers.Input.KeyboardInput += PlayerControl;
        playerRgbd = GetComponent<Rigidbody2D>();
        playerSpeed = 4;
        UsingItem.UsingItemListnerList += ListenerOnUsingItemForPlayerController;
    }

    public void PlayerControl()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
            MoveFunc(Vector2.right);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveFunc(Vector2.left);
    }

    public void MoveFunc(Vector2 vector)
    {
        playerRgbd.velocity = vector * new Vector2(playerSpeed, 0f);
    }

    #region ItemScripts
    public void ListenerOnUsingItemForPlayerController(Defines.SpeciesofItem _itemName)
    {
        switch (_itemName)
        {
            case Defines.SpeciesofItem.FasterMover:
                Managers.Sound.Play("FasterMover", 1, _itemName);
                StartCoroutine(FasterMover(_itemName));
                break;
            case Defines.SpeciesofItem.MoverExpand:
                Managers.Sound.Play("MoverExpand", 1, _itemName);
                StartCoroutine(MoverExpand(_itemName));
                break;
        }
    }

    IEnumerator FasterMover(Defines.SpeciesofItem itemName)
    {
        Managers.Items.ChangeItemUsed(itemName);
        playerSpeed += 2;
        yield return new WaitForSeconds(3);
        playerSpeed -= 2;
        Managers.Items.ChangeItemUsed(itemName);
    }

    IEnumerator MoverExpand(Defines.SpeciesofItem itemName)
    {
        Managers.Items.ChangeItemUsed(itemName);
        Vector2 curScale = transform.localScale;
        transform.localScale *= 2;
        yield return new WaitForSeconds(3);
        transform.localScale = curScale;
        Managers.Items.ChangeItemUsed(itemName);
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Item"))
        {
            Debug.Log("GET ITEM");
            Managers.Items.OnPlayerGetItem.Invoke(collision.gameObject);
        }                                       
    }
}
