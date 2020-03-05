///<summary>
/// <filename>IClickReceive.cs</filename>
///</summary>

namespace ClickReceive
{
    public enum ReciveType
    {
        None,       //何も反応しない
        Begin,
        Drag,
        Ended,
    }

    /// <summary>
    /// タッチ情報を受け取るために、
    /// ・オブジェクトー＞UIManagerに流す仕組みと
    /// ・UIManagerチェックー＞処理したい部分を受け取る箇所を取得する必要がある。
    /// </summary>
    interface IClickReceive
    {
    }
}
