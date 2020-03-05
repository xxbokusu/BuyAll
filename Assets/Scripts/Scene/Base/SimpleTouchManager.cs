///<summary>
/// <filename>SimpleTouchManager.cs</filename>
///</summary>

using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SimpleUIController<DATA> : MonoBehaviour, System.IDisposable where DATA : class
{
    private List<IDisposable> subscribe_list;
    void Start()
    {
        init();
    }
    protected void init()
    {
        subscribe_list = new List<IDisposable>();
        var _touch_array = this.transform.GetComponentsInChildren<SimpleTouch<DATA>>();
        foreach (var _obj in _touch_array)
        {
            var _reaction = _obj.ObserverReceive
                .Subscribe(
                (_event_data) =>
                {
                    Debug.Util.Log(this.ToString() + ":it react");
                    this.touch_reaction(_event_data);
                }).AddTo(gameObject);
            subscribe_list.Add(_reaction);
        }
    }

    void Update()
    {
    }

    protected virtual void touch_reaction(DATA _pointer_data)
    {
    }

    public virtual void Dispose()
    {
        foreach (var _subscribe in subscribe_list)
        {
            _subscribe.Dispose();
        }
    }
}
