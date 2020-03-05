///<summary>
/// <filename>MenuUIController.cs</filename>
///</summary>

using UniRx;

namespace Menu
{
    public class MenuUIController : SimpleUIController<MenuTouchInfomation>
    {
        private Subject<MenuTouchInfomation> touch_notice = new Subject<MenuTouchInfomation>();
        public Subject<MenuTouchInfomation> MenuUINotice
        {
            get
            {
                return touch_notice;
            }
        }

        void Start()
        {
            init();
        }

        void Update()
        {
        }

        protected override void touch_reaction(MenuTouchInfomation _pointer_data)
        {
            if (null != _pointer_data)
            {
                Debug.Util.Log("Reacted!");
                touch_notice.OnNext(_pointer_data);
            }

        }

    }
}
