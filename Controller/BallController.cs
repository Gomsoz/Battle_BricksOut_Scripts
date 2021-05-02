using UnityEngine;
using Unity;
using System;
using System.Collections;

public class BallController : MonoBehaviour
{
    Rigidbody2D ballRgbd;

    GameObject ballEffect;

    LayerMask collisionMask;

    Vector2 ballDir;
    Vector2 reflectDir;
    Vector2 _curReflectDir;

    Vector3 destPos;

    RaycastHit2D _curHit;

    public int ballSpeed;

    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        ballRgbd = GetComponent<Rigidbody2D>();
        ballDir = new Vector2(21, 17);
        ballRgbd.velocity = ballDir * ballSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Ray2D ray2D = new Ray2D(transform.position, ballDir);
        RaycastHit2D hit = Physics2D.Raycast(ray2D.origin, ray2D.direction, 1f, collisionMask);
        reflectDir = Vector2.Reflect(ballDir, hit.normal);

        if(reflectDir.y > 0)
        {
            if (_curReflectDir != reflectDir)
            {
                _curReflectDir = reflectDir;
                float alpha = (4 - hit.point.y) / _curReflectDir.normalized.y;
                destPos = hit.point + _curReflectDir.normalized * alpha;
                //destPos.y = 4;
                EnemyController.MoveEnemy.Invoke(destPos);
            }
        }
    }

    private void Init()
    {
        ballSpeed = 15;
        collisionMask = LayerMask.GetMask("Obstacles");
        UsingItem.UsingItemListnerList += ListenerOnUsingItemForBallController;
    }

    #region ItemScripts
    public void ListenerOnUsingItemForBallController(Defines.SpeciesofItem _itemName)
    {
        switch (_itemName)
        {
            case Defines.SpeciesofItem.FasterBallSpeed:
                StartCoroutine(FasterBallSpeed(_itemName));
                break;
            case Defines.SpeciesofItem.Boom:
                Boom(_itemName);
                break;
        }
    }

    IEnumerator FasterBallSpeed(Defines.SpeciesofItem itemName)
    {
        Managers.Items.ChangeItemUsed(itemName);
        ChangeBallSpeed(30);
        yield return new WaitForSeconds(3);
        ChangeBallSpeed(15);
        Managers.Items.ChangeItemUsed(itemName);
    }

    void Boom(Defines.SpeciesofItem itemName)
    {
        Managers.Items.ChangeItemUsed(itemName);
        Debug.Log(Managers.Items._isItemUsed[itemName]);

        ballEffect = Managers.Effect.SetItemEffect
            (this.gameObject, Enum.GetName(typeof(Defines.SpeciesofItem), (int)itemName), ballEffect);
    }

    void ChangeBallSpeed(int speed)
    {
        ballSpeed = speed;
    }

    #endregion
    public void OnCollisionEnter2D(Collision2D collision)
    {
        this.ballDir = reflectDir;
        ballRgbd.velocity = ballDir * ballSpeed * Time.deltaTime;

        switch (collision.transform.tag)
        {
            case "Player":
                Managers.Sound.Play("BallSound", 1, Defines.SoundType.Ball);
                EnemyController.MoveEnemy.Invoke(Vector3.zero);
                break;

            case "Block":
                // 폭탄 아이템이 발동중이면
                if (Managers.Items._isItemUsed[Defines.SpeciesofItem.Boom])
                {
                    Managers.Effect.ActiveEffect(ballEffect);
                    Managers.Sound.Play("Boom", 1, Defines.SpeciesofItem.Boom);
                    Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 2f, collisionMask);
                    foreach (Collider2D _collision in collisions)
                    {
                        if (_collision.CompareTag("Block"))
                            _collision.transform.GetComponent<BlockBehavior>().DestroyBlock();
                    }

                    Managers.Items.ChangeItemUsed(Defines.SpeciesofItem.Boom);
                    return;
                }

                Managers.Sound.Play("BlockBreakSound", 1, Defines.SoundType.Block);
                collision.transform.GetComponent<BlockBehavior>().DestroyBlock();
                break;

            case "Wall":
                Managers.Sound.Play("BallSound", 1, Defines.SoundType.Ball);
                break;
        }                    
    }
}
