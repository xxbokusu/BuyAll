///<summary>
/// <filename>PlayScore.cs</filename>
///</summary>

using System.Collections.Generic;

public class PlayScore
{
    private int score = 0;
    private List<string> get_item_list = new List<string>();

    public PlayScore(int now_score = 0)
    {
        score = now_score;
    }

    public void Start()
    {
        set(score);
    }

    public int get() { return this.score; }
    public void set(int value) { this.score = value; }

    public void add(int value, string str)
    {
        this.score += value;
        get_item_list.Add(str);
    }

    public void reset()
    {
        this.score = 0;
    }
}