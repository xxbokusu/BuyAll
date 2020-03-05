///<summary>
/// <filename>ScoreManager.cs</filename>
///</summary>

using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Mission;

public class TextJudgeResult
{
    private bool is_match = false;
    private bool is_first = false;
    public TextJudgeResult(bool _is_match, bool _is_first)
    {
        is_match = _is_match;
        is_first = _is_first;
    }
    public bool isMatch() { return is_match; }
    public bool isFirst() { return is_first; }
}

///<summary>取得物を管理する。スコアおよび獲得アイテムはここへ</summary>
public class ScoreManager : MonoBehaviour, IDisposable
{
    private Dictionary<string, int> shopping_menu = new Dictionary<string, int>
        {
            {"lip stick" , 100},
            {"cafe latte", 200},
            {"bo ots", 300},
            {"ear phone", 500},
        };
    public Dictionary<string, int> ShoppingMenu
    {
        get { return shopping_menu; }
    }
    public TextJudgeResult ContainsTextPair(string first, string second)
    {
        if (shopping_menu.ContainsKey(first + " " + second))
        {
            return new TextJudgeResult(true, true);
        }
        else if (shopping_menu.ContainsKey(second + " " + first))
        {
            return new TextJudgeResult(true, false);
        }
        return new TextJudgeResult(false, false);
    }

    private static ScoreManager manager = null;

    private PlayScore score;
    public PlayScore Score
    {
        get { return score; }
    }

    public static ScoreManager Instance
    {
        get { return manager; }
    }

    public void Awake()
    {
        manager = this;
        DontDestroyOnLoad(this);
    }

    private Dictionary<string, int> shopping_result_dic;
    public void Start()
    {
        shopping_result_dic = new Dictionary<string, int>();
        UnitManagerSystem.Instance.EnemyBreakNotice.Subscribe((info) => { set_break_enemy(info); });
    }
    public Dictionary<string, int> getShoppingResult
    {
        get { return shopping_result_dic; }
    }

    ///<summary>1ゲーム終了時にスコアをリセット</summary>
    public void ResetOnGameEnd()
    {
        score.reset();
    }

    public void addBoughtItem(string item)
    {
        int value = 100; // TODO リストからスコアを取得して入れる
        Debug.Util.LogFormat("Mission Clear Score: {0} -> {1}", score.get(), score.get() + value);
        this.score.add(value, item);
    }

    public void Dispose()
    {
        Destroy(this);
    }

    private void set_break_enemy(MissionNoticeInfo _info)
    {
        if (_info.NoticeType == MissionNoticeType.BreakEnemy)
        {
            Debug.Util.Log("Get " + _info.Name + " By Shopping");
            if (shopping_result_dic.ContainsKey(_info.Name))
            {
                shopping_result_dic[_info.Name]++;
            }
            else
            {
                shopping_result_dic.Add(_info.Name, 1);
            }
        }
    }
}