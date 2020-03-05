///<summary>
/// <filename>Status.cs</filename>
///</summary>

public class StatusBase
{
    public string name { get; private set; }
    public StatusBase(string _name)
    {
        name = _name;
    }
}
