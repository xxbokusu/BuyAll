///<summary>
/// <filename>PlayUIManager.cs</filename>
///</summary>

using UnityEngine;
using UniRx;

namespace Result
{
    public class ResultUIManager : MonoBehaviour
    {
        [SerializeField] private ResultUIController ui_controller = null;
        [SerializeField] Camera ui_camra;

        private Subject<ResultTouchInfomation> notice = new Subject<ResultTouchInfomation>();

        private TouchedOption tmp_touch_opt;
        /// <summary>タッチが来た時の通知</summary>
        public Subject<ResultTouchInfomation> TouchNotice
        {
            get
            {
                return notice;
            }
        }

        // Use this for initialization
        void Start()
        {
            ui_controller.ReultUINotice
                .Subscribe((_unit) => { ui_touch_notice(_unit); })
                .AddTo(this);
        }

        void Update()
        {
        }

        private void ui_touch_notice(ResultTouchInfomation _infomation)
        {
            notice.OnNext(_infomation);
        }
    }
}
