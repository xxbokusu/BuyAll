///<summary>
/// <filename>MenuUIManager.cs</filename>
///</summary>

using UnityEngine;
using UniRx;

namespace Menu
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private MenuUIController menu_ui = null;
        [SerializeField] Camera ui_camra;

        private Subject<TouchInfomation> notice = new Subject<TouchInfomation>();

        private TouchedOption tmp_touch_opt;
        /// <summary>タッチが来た時の通知</summary>
        public Subject<TouchInfomation> TouchNotice
        {
            get
            {
                Debug.Util.Log(notice + " : it is touched!");
                return notice;
            }
        }

        // Use this for initialization
        void Start()
        {
            menu_ui.MenuUINotice
                .Subscribe((_unit) => { menu_ui_notice(_unit); })
                .AddTo(this);
        }

        void Update()
        {
        }

        private void menu_ui_notice(TouchInfomation _infomation)
        {
            notice.OnNext(_infomation);
        }
    }
}
