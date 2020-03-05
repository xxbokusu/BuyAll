///<summary>
/// <filename>ResultTouchInfomation.cs</filename>
///</summary>

using UnityEngine.EventSystems;
using UnityEngine;

namespace Result
{
    public class ResultTouchInfomation
    {
        public TouchedOption Opt { get; set; }

        public Vector2 TouchObjectPosition { get; set; }

        public Touch touch_info { get; set; }

        public PointerEventData PointerData { get; set; }
    }
}
