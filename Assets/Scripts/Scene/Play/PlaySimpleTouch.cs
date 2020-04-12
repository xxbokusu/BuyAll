///<summary>
/// <filename>PlaySimpleTouch.cs</filename>
///</summary>

using UnityEngine;
using Play;
using UnityEngine.EventSystems;

public class PlaySimpleTouch : SimpleTouch<TouchInfomation>
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

    protected override TouchInfomation createdData(PointerEventData _event_data)
    {
        TouchInfomation _info = new TouchInfomation()
        {
            Opt = opt,
            PointerData = _event_data
        };
        return _info;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        this.ObserverReceive.OnNext(createdData(eventData));
    }


}
