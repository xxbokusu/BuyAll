///<summary>
/// <filename>TouchInfomation.cs</filename>
///</summary>

using UnityEngine.EventSystems;
using UnityEngine;

public enum TouchedOption
{
    Menu,
    Result,
    GameStart,
    Ranking,
    None,
}

public class TouchInfomation
{
    public TouchedOption Opt { get; set; }

    public Vector2 TouchObjectPosition { get; set; }

    public Touch touch_info { get; set; }
    /// <summary>
    /// 受け取ったイベントデータです。
    /// </summary>
    public PointerEventData PointerData { get; set; }
}
