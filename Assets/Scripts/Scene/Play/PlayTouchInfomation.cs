///<summary>
/// <filename>PlayTouchInfomation.cs</filename>
///</summary>

using UnityEngine.EventSystems;
using UnityEngine;

namespace Play
{
    public class PlayTouchInfomation
    {
        public TouchedOption Opt { get; set; }

        public Vector2 TouchObjectPosition { get; set; }

        public Touch touch_info { get; set; }
        /// <summary>
        /// 受け取ったイベントデータです。
        /// </summary>
        public PointerEventData PointerData { get; set; }
    }
}
