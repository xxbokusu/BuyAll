///<summary>
/// <filename>MissionNoticeInfo.cs</filename>
///</summary>

namespace Mission
{
    public enum MissionNoticeType
    {
        None = 0,
        BreakEnemy = 1 << 1,
        Correct = 1 << 2,
    }

    public class MissionInfoContainerBase { }
    public class MissionNoticeInfo
    {
        private MissionNoticeType type;
        public MissionNoticeType NoticeType
        {
            get
            {
                return type;
            }
        }

        private MissionInfoContainerBase container;
        public MissionInfoContainerBase ContainerData
        {
            get
            {
                return container;
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        public MissionNoticeInfo(MissionNoticeType _type, MissionInfoContainerBase _container, string _name)
        {
            type = _type;
            container = _container;
            name = _name;
        }

    }

}