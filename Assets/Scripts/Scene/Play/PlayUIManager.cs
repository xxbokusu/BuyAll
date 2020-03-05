﻿///<summary>
/// <filename>PlayUIManager.cs</filename>
///</summary>

using UnityEngine;
using UniRx;

namespace Play
{
    public class PlayUIManager : MonoBehaviour
    {
        [SerializeField] private PlayUIController menu_ui = null;
        [SerializeField] Camera ui_camra;

        private Subject<PlayTouchInfomation> notice = new Subject<PlayTouchInfomation>();
        public Subject<PlayTouchInfomation> TouchNotice
        {
            get
            {
                return notice;
            }
        }

        void Start()
        {
            menu_ui.MenuUINotice
                .Subscribe((_unit) => { menu_ui_notice(_unit); })
                .AddTo(this);
        }

        void Update()
        {
        }

        private void menu_ui_notice(PlayTouchInfomation _infomation)
        {
            notice.OnNext(_infomation);
        }
    }
}