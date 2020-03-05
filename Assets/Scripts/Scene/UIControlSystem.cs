///<summary>
/// <filename>UIControlSystem.cs</filename>
///</summary>

using System.Collections.Generic;
using UnityEngine;
using ClickReceive;

public class UIControlSystem : Singleton<UIControlSystem>
{
    private Dictionary<GameObject, IClickReceive> dic_need_reflection_object;
    private List<ReactionObjectInfo> reaction_obj_list;

    private class ReactionObjectInfo
    {
        public ReactionObjectInfo(int _touch_id, GameObject _obj)
        {
            touch_id = _touch_id;
            reaction_object = _obj;
        }
        public int touch_id;
        public GameObject reaction_object;
    }

    public void Init()
    {
        reaction_obj_list = new List<ReactionObjectInfo>();
    }

    private void LateUpdate()
    {
    }

    /// <summary>
    /// リアクションが必要なゲームオブジェクトを生成する
    /// </summary>
    /// <param name="_need_notification_obj"></param>
    /// <param name="_game_object"></param>
    public bool SetNotificationGameObject(GameObject _need_notification_obj)
    {
        if (null == dic_need_reflection_object)
        {
            Debug.Util.LogWarning("Not yet made Object!");
            return false;
        }

        var _recive = _need_notification_obj.GetComponent<IClickReceive>();
        if (null == _recive)
        {
            Debug.Util.LogWarning("GamelObject：" + _need_notification_obj.name + "はIClickReceiveがついていません");
            return false;
        }
        Debug.Util.Log("登録されてオブジェクトは" + _need_notification_obj.name);

        ///すでに登録済みだったため。
        if (dic_need_reflection_object.ContainsKey(_need_notification_obj))
        {
            Debug.Util.LogFormat("すでに登録済み：{0}", _need_notification_obj.name);
            return false;
        }


        dic_need_reflection_object.Add(_need_notification_obj, _recive);

        return true;
    }

    /// <summary>様々なオブジェクトから、タッチIDとGameObjectが通知として飛んでくる。</summary>
    public void SetReactionGameObject(int _touch_id, GameObject _reaction_object)
    {

        if (null == reaction_obj_list)
        {
            Debug.Util.LogWarning("まだreactionインスタンスが作られていない。");
            return;
        }
#if UNITY_EDITOR
        //マウスの左クリックに合わせてのID設定
        _touch_id = 0;
#endif
        Debug.Util.LogFormat("反応が来たオブジェクトは:{0}", _reaction_object.name);
        ReactionObjectInfo _info = new ReactionObjectInfo(_touch_id, _reaction_object);
        reaction_obj_list.Add(_info);

        Debug.Util.LogFormat("ついかされている数は:{0}", reaction_obj_list.Count);
    }
}
