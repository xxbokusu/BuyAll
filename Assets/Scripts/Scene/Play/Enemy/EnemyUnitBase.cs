///<summary>
/// <filename>EnemyUnitBase.cs</filename>
///</summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Scene;
using Mission;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyUnitBase : MonoBehaviour
{
    private Rigidbody2D physics = null;
    protected StatusBase status;
    public StatusBase UnitStatus
    {
        get { return status; }
    }

    public Vector2 Pos2D
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }

    public void Awake()
    {
        this.physics = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
    }

    public void TappedMove(BaseEventData eventData)
    {
        SceneTransition.Instance.Sound.PlaySe(1);
        PointerEventData pointer_event = eventData as PointerEventData;
        this.physics.AddForce(new Vector2(
            this.transform.position.x - pointer_event.position.x,
            this.transform.position.y - pointer_event.position.y
        ), ForceMode2D.Impulse);
    }

    public void Init(string _name)
    {
        status = new StatusBase(_name);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Text other_text = other.gameObject.GetComponent<Text>();
        if (null == other_text)
        {
            return;
        }

        Text this_text = this.gameObject.GetComponent<Text>();
        TextJudgeResult result = ScoreManager.Instance.ContainsTextPair(this_text.text, other_text.text);
        if (result.isMatch())
        {
            UnitManagerSystem.Instance.DeadEnemy(this.gameObject);
        }
        if (result.isFirst())
        {
            SceneTransition.Instance.Sound.PlaySe(0);
            MissionNoticeInfo _notice_info = new MissionNoticeInfo(
                MissionNoticeType.BreakEnemy,
                new MissionNoticeEnemyBreak(status.name),
                this_text.text + " " + other_text.text
                );
            UnitManagerSystem.Instance.EnemyBreakNotice.OnNext(_notice_info);
        }
    }
}
