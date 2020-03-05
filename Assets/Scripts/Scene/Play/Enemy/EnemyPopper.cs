///<summary>
/// <filename>EnemyPopper.cs</filename>
///</summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Play;
using Mission;
using UniRx;

public class EnemyPopper : MonoBehaviour
{
    private float elapsed_time;
    private bool is_create_update;
    private List<ReserveData> reserve_pop_list;
    private PlayUIManager parent;

    private Dictionary<PopObjectType, GameObject> prefab_data_dic;

    private class ReserveData
    {
        public PopObjectType type;
        public float next_interval_time;
        public string word;
        public ReserveData(float _interval, PopObjectType _type, string _word)
        {
            type = _type;
            next_interval_time = _interval;
            word = _word;
        }
    }

    private void Awake()
    {
        reserve_pop_list = new List<ReserveData>();
    }

    void Start()
    {
        elapsed_time = 0;
        is_create_update = false;
        UnitManagerSystem.Instance.EnemyBreakNotice.Subscribe((info) =>
        {
            pop_reward(info);
        }).AddTo(this);
    }

    public void SetPrefabData(InitEnemyData[] _prefab_data)
    {
        if (null == prefab_data_dic)
        {
            prefab_data_dic = new Dictionary<PopObjectType, GameObject>();
        }
        for (int _index = 0; _index < _prefab_data.Length; _index++)
        {
            prefab_data_dic.Add(_prefab_data[_index].type, _prefab_data[_index].prefabData);
        }
    }

    public void SetParent(PlayUIManager _ui_manager)
    {
        parent = _ui_manager;
    }

    public void SetPopEnemyData(float _pop_interval, PopObjectType _pop_type, string[] _quest)
    {
        reserve_pop_list.Add(new ReserveData(_pop_interval, _pop_type, _quest[0]));
        reserve_pop_list.Add(new ReserveData(_pop_interval, _pop_type, _quest[1]));
        UpdateStart();
    }

    private void UpdateStart()
    {
        if (is_create_update) { return; }
        is_create_update = true;
    }
    void Update()
    {
        if (false == is_create_update) { return; }

        elapsed_time += Time.deltaTime;
        if (reserve_pop_list[0].next_interval_time < elapsed_time)
        {
            elapsed_time -= reserve_pop_list[0].next_interval_time;
            born_next_enemy();
            if (reserve_pop_list.Count == 0)
            {
                elapsed_time = 0;
                is_create_update = false;
            }
        }
    }

    private int play_area_pos_x_min = -300;
    public int PlayAreaPosXMin
    {
        get { return play_area_pos_x_min; }
        set { play_area_pos_x_min = value; }
    }
    private int play_area_pos_x_max = 480;
    private int play_area_pos_y_min = -80;
    public int PlayAreaPosYMin
    {
        get { return play_area_pos_y_min; }
        set { play_area_pos_y_min = value; }
    }
    private int play_area_pos_y_max = 280;
    private void born_next_enemy()
    {
        var _reserve_data = reserve_pop_list[0];
        PopObjectType _type = _reserve_data.type;
        if (false == prefab_data_dic.ContainsKey(_type))
        {
            return;
        }
        GameObject _prefab = null;
        prefab_data_dic.TryGetValue(_type, out _prefab);

        GameObject _pop_text = GameObject.Instantiate(
            _prefab, transform.position, transform.rotation);
        // TODO Prefabの出現時のステータス設定。どこかに分離したい
        _pop_text.transform.SetParent(parent.transform, false);
        Vector2 pop_pos = new Vector2(
            UnityEngine.Random.Range(play_area_pos_x_min + 10, play_area_pos_x_max),
            UnityEngine.Random.Range(play_area_pos_y_min + 10, play_area_pos_y_max));
        _pop_text.transform.localPosition = pop_pos;
        _pop_text.transform.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
        reserve_pop_list.Remove(_reserve_data);

        EnemyUnitBase _unit_base = _pop_text.GetComponent<EnemyUnitBase>();
        if (null == _unit_base) { Debug.Util.LogError("EnemyUniteBase is null"); }

        Text _text = _pop_text.GetComponent<Text>();
        _text.text = _reserve_data.word;
        _unit_base.Init(_text.text.ToString());

        BoxCollider2D collider = _pop_text.GetComponent<BoxCollider2D>();
        collider.size = new Vector2(_reserve_data.word.ToString().Length * 25, 60);

        UnitManagerSystem.Instance.SetPopEnemyNotification(_unit_base);
    }

    private void pop_reward(MissionNoticeInfo _info)
    {
        GameObject _prefab = null;
        // TODO Random取得をメソッド化して可用性を高めること(+1をｔけないと全種類出せない…)
        int object_type = UnityEngine.Random.Range((int)PopObjectType.ShoppingBag, (int)PopObjectType.ShoppingBag3 + 1);
        prefab_data_dic.TryGetValue((PopObjectType)object_type, out _prefab);
        GameObject shopping_bag = GameObject.Instantiate(
            _prefab, transform.position, transform.rotation);
        // TODO Prefabの出現時のステータス設定。どこかに分離したい
        shopping_bag.transform.SetParent(parent.transform, false);
        shopping_bag.transform.localPosition = new Vector2(
            UnityEngine.Random.Range(0, 400), -162);
        shopping_bag.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

}
