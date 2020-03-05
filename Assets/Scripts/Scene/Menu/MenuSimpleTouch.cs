///<summary>
/// <filename>MenuSimpleTouch.cs</filename>
///</summary>

using UnityEngine;
using UnityEngine.EventSystems;
using Menu;

public class MenuSimpleTouch : SimpleTouch<MenuTouchInfomation>
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

    protected override MenuTouchInfomation createdData(PointerEventData _event_data)
    {
        MenuTouchInfomation _info = new MenuTouchInfomation()
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
