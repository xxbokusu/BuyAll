///<summary>
/// <filename>MissionProcess.cs</filename>
///</summary>

public class MissionProgress
{
    private int mission_limit;
    private int now_mission_no;
    private int done_mission_num;
    public int MissionLimit
    {
        get
        {
            return mission_limit;
        }
    }

    public MissionProgress(int _mission_limit)
    {
        now_mission_no = 1;
        done_mission_num = 0;
    }

    public void AddDoneMission()
    {
        now_mission_no++;
        done_mission_num++;
    }
}
