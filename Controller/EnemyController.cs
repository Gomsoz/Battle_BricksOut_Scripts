using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public static Action<Vector3> MoveEnemy = null;

    Defines.EnemyState _enemyState = Defines.EnemyState.Stop;

    Vector3 _defaultPos;
    Vector3 _destPos;

    int _enemySpeed = 1;

    private void Awake()
    {
        GameManager.GameMgr.ChangeEnemyLevel += ChangeEnemyLevelAtEnemySpeed;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        EnemyControl();
    }

    void Init()
    {
        _defaultPos = this.transform.position;
        MoveEnemy += SetEnemyDest;
    }

    void EnemyControl()
    {
        switch (_enemyState)
        {
            case Defines.EnemyState.Stop:
                EnemyStateStop();
                break;
            case Defines.EnemyState.Move:
                EnemyStateMove();
                break;
            case Defines.EnemyState.Default:
                EnemyStateDefault();
                break;
        }
    }

    void EnemyStateStop()
    {
    }
    void EnemyStateMove()
    {
        EnemyMove(_destPos);
    }

    void EnemyStateDefault()
    {
        EnemyMove(_defaultPos);
    }

    void EnemyMove(Vector3 destPos)
    {
        Vector3 destDir = destPos - transform.position;
        if (destDir.magnitude < 0.0001f)
            _enemyState = Defines.EnemyState.Stop;

        transform.position += destDir.normalized * Mathf.Clamp(_enemySpeed * Time.deltaTime, 0, destDir.magnitude);
    }

    void SetEnemyDest(Vector3 destPos)
    {
        if (destPos == Vector3.zero)
        {
            _enemyState = Defines.EnemyState.Default;
            return;
        }         
        _destPos = destPos;
        _enemyState = Defines.EnemyState.Move;
    }

    public void ChangeEnemyLevelAtEnemySpeed(Defines.EnemyLevel enemyLevel)
    {
        _enemySpeed = (int)enemyLevel + 1;
    }
}
