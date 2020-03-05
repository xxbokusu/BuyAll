///<summary>
/// <filename>EnemyPopManager.cs</filename>
///</summary>

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Play;
using DG.Tweening;

public enum PopObjectType
{
    Text,
    ShoppingBag,
    ShoppingBag2,
    ShoppingBag3,
}

public class InitEnemyData
{
    public InitEnemyData(PopObjectType _type, GameObject _prefab)
    {
        prefabData = _prefab;
        type = _type;
    }
    public GameObject prefabData;
    public PopObjectType type;
}

public class EnemyPopManager : MonoBehaviour
{
    [SerializeField] private PlayUIManager ui_manager;
    private readonly float PopperLength = 30f;
    private GameObject Camera;
    [SerializeField] private int enemy_num_limit = 10;
    [SerializeField] private Image right_wall;
    [SerializeField] private Image down_wall;

    private EnemyPopper enemy_popper;
    private string[] _quest;
    void Start()
    {
        Initalize();
    }
    private void Initalize()
    {
        if (null != ScoreManager.Instance)
        {
            _quest = new string[ScoreManager.Instance.ShoppingMenu.Keys.Count];
        }
        else
        {
            _quest = new string[10]; // Scene単位で開発時用の仮パラメータ
        }
        ScoreManager.Instance.ShoppingMenu.Keys.CopyTo(_quest, 0);
        enemy_pop_born_initalize(init_enemy_prefab_data());
    }

    private InitEnemyData[] init_enemy_prefab_data()
    {
        List<InitEnemyData> _list_data = new List<InitEnemyData>();
        foreach (var _enum in Enum.GetValues(typeof(PopObjectType)))
        {
            GameObject _prefab = ResourceManager.Instance.Prefab.GetResource((int)_enum);
            if (null == _prefab)
            {
                Debug.Util.LogErrorFormat("Error get resources : type:{0}", _enum);
                return null;
            }
            else
            {
                _list_data.Add(new InitEnemyData((PopObjectType)_enum, _prefab));
            }
        }
        return _list_data.ToArray();
    }
    private void enemy_pop_born_initalize(InitEnemyData[] _prefab_data)
    {
        GameObject _obj = new GameObject("EnemyPopper");
        _obj.transform.SetParent(this.transform, false);
        var _enemy_popper = _obj.AddComponent<EnemyPopper>();
        _enemy_popper.SetPrefabData(_prefab_data);
        _enemy_popper.SetParent(ui_manager);
        enemy_popper = _enemy_popper;

        right_wall.transform.DOLocalMove(new Vector2(100, 120), 30.0f).OnUpdate(() =>
        {
            enemy_popper.PlayAreaPosXMin = (int)right_wall.transform.localPosition.x;
        });
        down_wall.transform.DOLocalMove(new Vector2(100, 50), 30.0f).OnUpdate(() =>
        {
            enemy_popper.PlayAreaPosYMin = (int)down_wall.transform.localPosition.y;
        });
    }

    private float enemy_pop_interval = 0.0f;
    void Update()
    {
        if (UnitManagerSystem.Instance.getEnemyCount >= enemy_num_limit) { return; }

        Quaternion _rotate = this.transform.localRotation;
        _rotate = _rotate * Quaternion.Euler(0, 1, 0);
        this.transform.localRotation = _rotate;
        float _pop_time = 0.5f;
        enemy_pop_interval += Time.deltaTime;
        if (enemy_pop_interval < 2.0f)
        {
            return;
        }
        enemy_pop_interval = 0.0f;
        int rand = UnityEngine.Random.Range(0, _quest.Length);
        string[] quest = _quest[rand].Split(' ');
        enemy_popper.SetPopEnemyData(_pop_time, PopObjectType.Text, quest);
    }
}
