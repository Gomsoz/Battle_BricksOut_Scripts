using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region Property
    private static Managers s_instance = null;
    public static Managers Instance { get { return s_instance; } }
    // 입력 관리
    private static InputManager _input = new InputManager();
    // 블록생성 보드 관리
    private static BoardManager _boardMgr = new BoardManager();
    // 리소스 관리
    private static ResourcesManager _resources = new ResourcesManager();
    //아이템 관리
    private static ItemManager _item = new ItemManager();
    //이펙트 관리
    private static EffectManager _effect = new EffectManager();
    //사운드 관리
    private static SoundManager _sound = new SoundManager();
    //씬 관리
    private static SceneManagerEx _scene = new SceneManagerEx();

    public static InputManager Input { get { return _input; } }
    public static BoardManager BoardMgr { get { return _boardMgr; } }
    public static ResourcesManager Resources { get { return _resources; } }
    public static ItemManager Items { get { return _item; } }
    public static EffectManager Effect { get { return _effect; } }
    public static SoundManager Sound { get { return _sound; } }
    public static SceneManagerEx Scene { get { return _scene; } }
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        Input.InputUpdate();
    }

    void Init()
    {
        SingletonManager();
        ManagersInit();
    }

    public void SingletonManager()
    {
        if (s_instance == null)
        {
            GameObject findGo = GameObject.Find("@Managers");
            if (findGo == null)
            {
                findGo = new GameObject { name = "@Managers" };
                findGo.AddComponent<Managers>();
            }

            s_instance = findGo.GetComponent<Managers>();
            DontDestroyOnLoad(findGo);
        }
    }
    public void ManagersInit()
    {
        Items.Init();
        BoardMgr.Init();
        Sound.Init();
    }
}
