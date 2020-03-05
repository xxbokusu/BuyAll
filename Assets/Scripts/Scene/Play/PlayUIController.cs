///<summary>
/// <filename>PlayUIController.cs</filename>
///</summary>

using UniRx;

namespace Play
{
    public class PlayUIController : SimpleUIController<PlayTouchInfomation>
    {
        private Subject<PlayTouchInfomation> touch_notice = new Subject<PlayTouchInfomation>();
        public Subject<PlayTouchInfomation> MenuUINotice
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

        protected override void touch_reaction(PlayTouchInfomation _pointer_data)
        {
            if (null != _pointer_data)
            {
                Debug.Util.Log("Reacted!");
                touch_notice.OnNext(_pointer_data);
            }

        }

    }
}
