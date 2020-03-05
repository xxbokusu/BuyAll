///<summary>
/// <filename>ResultUIController.cs</filename>
///</summary>

using UniRx;

namespace Result
{
    public class ResultUIController : SimpleUIController<ResultTouchInfomation>
    {
        private Subject<ResultTouchInfomation> touch_notice = new Subject<ResultTouchInfomation>();
        public Subject<ResultTouchInfomation> ReultUINotice
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

        protected override void touch_reaction(ResultTouchInfomation _pointer_data)
        {
            if (null != _pointer_data)
            {
                Debug.Util.Log("Reacted!");
                touch_notice.OnNext(_pointer_data);
            }

        }

    }
}
