///<summary>
/// <filename>ResultSimpleTouch.cs</filename>
///</summary>

using UnityEngine;
using Result;
using UnityEngine.EventSystems;

public class ResultSimpleTouch : SimpleTouch<ResultTouchInfomation>
{
    [SerializeField]
    private TouchedOption opt = TouchedOption.None;

    public TouchedOption MyType
    {
        get
        {
            return opt;
        }
    }

    void Update()
    {
    }

    protected override ResultTouchInfomation createdData(PointerEventData _event_data)
    {
        ResultTouchInfomation _info = new ResultTouchInfomation()
        {
            Opt = opt,
            PointerData = _event_data
        };
        return _info;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Util.Log(this.ToString() + ": it is touched");
        this.ObserverReceive.OnNext(createdData(eventData));
    }


}
