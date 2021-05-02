using UnityEngine;

public class BoardSetting
{
    public enum WhosePlayer
    {
        player,
        enemy
    };
    WhosePlayer whosePlayer;

    GameObject _emptyTargetBlock;
    GameObject _itemTargetBlock;
    SpriteRenderer _targetSpriteRenderer;

    GameObject[,] enemyBlocks;
    GameObject[,] playerBlocks;

    Sprite[] blockSprites = new Sprite[7];
    string blockPath = $"BlockImages/";

    Vector2 enemyBlockStartPos;
    Vector2 playerBlockStartPos;

    Transform enemyBlockHolder;
    Transform playerBlockHolder;

    int maxCol;
    int maxRow;

    int _probItem;

    bool _isGetItem;

    public void Init()
    {
        enemyBlockHolder = new GameObject("EnemyBlockHolder").transform;
        playerBlockHolder = new GameObject("MyBlockHolder").transform;

        enemyBlockStartPos = new Vector2(-4.5f, 5.75f);
        playerBlockStartPos = new Vector2(4.5f, -5.75f);

        maxCol = 9;
        maxRow = 9;

        enemyBlocks = new GameObject[maxCol, maxRow];
        playerBlocks = new GameObject[maxCol, maxRow];

        _emptyTargetBlock = Managers.Resources.Load<GameObject>("Prefabs/Block");
        _itemTargetBlock = Managers.Resources.Load<GameObject>("Prefabs/Block");
        _targetSpriteRenderer = _emptyTargetBlock.GetComponent<SpriteRenderer>();

        _probItem = ((int)GameManager.GameMgr.enemyLevel * 30) + 20;



        for (int i = 0; i < 7; i++)
        {
            blockSprites[i] = Managers.Resources.Load<Sprite>($"{blockPath}block{i}");
        }        
    }

    public void SetBoard()
    {
        // Enemy's Blocks
        MakeBlock(enemyBlockStartPos, WhosePlayer.enemy);
        MakeBlock(playerBlockStartPos, WhosePlayer.player);
    }

    public void MakeBlock(Vector2 startPos, WhosePlayer player)
    {
        
        Vector2 targetPos;

        if (player == WhosePlayer.player)
        {
            for (int targetCol = 1; targetCol < maxCol; targetCol++)
            {
                for (int targetRow = 1; targetRow < maxRow; targetRow++)
                {
                    targetPos = startPos - new Vector2(targetRow, 0.5f * targetCol);
                    _targetSpriteRenderer.sprite = blockSprites[Random.Range(0, blockSprites.Length)];
                    if (IsThisBlockHasItem())
                    {
                        GameObject instance = GameObject.Instantiate(_emptyTargetBlock, targetPos, Quaternion.identity);
                        instance.transform.SetParent(playerBlockHolder);
                        instance.GetComponent<BlockBehavior>()._itemInstance = Managers.Items.CreateItem(instance.transform);
                    }
                    else
                    {
                        GameObject instance = GameObject.Instantiate(_emptyTargetBlock, targetPos, Quaternion.identity);
                        instance.transform.SetParent(playerBlockHolder);
                    }
                    
                    //enemyBlocks[targetCol, targetRow] = instance;
                }
            }
        }

        else if (player == WhosePlayer.enemy)
        {
            for (int targetCol = 1; targetCol < maxCol; targetCol++)
            {
                for (int targetRow = 1; targetRow < maxRow; targetRow++)
                {
                    targetPos = new Vector2(targetRow, 0.5f * targetCol) + startPos;
                    _targetSpriteRenderer.sprite = blockSprites[Random.Range(0, blockSprites.Length)];
                    if (IsThisBlockHasItem())
                    {
                        GameObject instance = GameObject.Instantiate(_emptyTargetBlock, targetPos, Quaternion.identity);
                        instance.transform.SetParent(enemyBlockHolder);
                        instance.GetComponent<BlockBehavior>()._itemInstance = Managers.Items.CreateItem(instance.transform);
                    }
                    else
                    {
                        GameObject instance = GameObject.Instantiate(_emptyTargetBlock, targetPos, Quaternion.identity);
                        instance.transform.SetParent(enemyBlockHolder);
                    }
                    //enemyBlocks[targetCol, targetRow] = instance;
                }
            }
        }
    }

    public void ChangeEnemyLevelAtItemCnt(Defines.EnemyLevel enemyLevel)
    {
        _probItem = ((int)enemyLevel * 30) + 10;
    }

    public bool IsThisBlockHasItem()
    {  
        if (Random.Range(0, 100) > _probItem)
            return true;
        return false;
    }
}
