///<summary>
/// <filename>MenuUIController.cs</filename>
///</summary>

using UniRx;

namespace Menu
{
    public class MenuUIController : SimpleUIController<TouchInfomation>
    {
        private Subject<TouchInfomation> touch_notice = new Subject<TouchInfomation>();
        public Subject<TouchInfomation> MenuUINotice
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

        protected override void touch_reaction(TouchInfomation _pointer_data)
        {
            if (null != _pointer_data)
            {
                Debug.Util.Log("Reacted!");
                touch_notice.OnNext(_pointer_data);
            }

        }

    }
}
