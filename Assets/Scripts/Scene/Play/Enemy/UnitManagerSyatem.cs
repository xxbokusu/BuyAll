///<summary>
/// <filename>UnitManagerSyatem.cs</filename>
///</summary>

using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Mission;

public class UnitManagerSystem : Singleton<UnitManagerSystem>
{
    /// <summary>現在の出現数</summary>
    private int count = 0;

    private List<EnemyUnitBase> enemy_unit_list = new List<EnemyUnitBase>();
    public List<EnemyUnitBase> EnemyUnitList { get { return enemy_unit_list; } }
    public int getEnemyCount
    {
        get
        {
            if (null == enemy_unit_list)
            {
                return 0;
            }
            return enemy_unit_list.Count;
        }
    }

    private Subject<MissionNoticeInfo> enemy_break_notice;
    /// <summary>敵撃破通知</summary>
    public Subject<MissionNoticeInfo> EnemyBreakNotice
    {
        get
        {
            if (null == enemy_break_notice)
            {
                enemy_break_notice = new Subject<MissionNoticeInfo>();
            }
            return enemy_break_notice;
        }
    }
    void Update()
    {
        remove_dead_enemy_data();
    }

    public void ResetEnemyList()
    {
        enemy_unit_list.RemoveAll((x) => null != x);
    }

    public void DeadEnemy(GameObject _enemy_object)
    {
        Vector2 _pos = _enemy_object.transform.position;
        enemy_unit_list.Remove(_enemy_object.GetComponent<EnemyUnitBase>());
        UnityEngine.Object.Destroy(_enemy_object);
    }

    private void remove_dead_enemy_data()
    {
        enemy_unit_list.RemoveAll((x) => null == x);
    }

    public void SetPopEnemyNotification(EnemyUnitBase _enemy_unit)
    {
        if (null == enemy_unit_list)
        {
            return;
        }
        enemy_unit_list.Add(_enemy_unit);
    }

    public void Init()
    {
        enemy_unit_list = new List<EnemyUnitBase>();
    }

    public void UpdateMine()
    {
        if (null == enemy_unit_list)
        {
            return;
        }
        enemy_unit_list.RemoveAll((x) => (x == null));

    }

    public void Destroy() { }
}
