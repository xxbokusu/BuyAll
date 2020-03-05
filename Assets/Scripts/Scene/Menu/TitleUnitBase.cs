///<summary>
/// <filename>TitleUnitBase.cs</filename>
///</summary>

using UnityEngine;
using UnityEngine.EventSystems;
using Scene;
using UnityEngine.UI;
using UniRx;

[RequireComponent(typeof(Rigidbody2D))]
public class TitleUnitBase : MonoBehaviour
{
    private Rigidbody2D physics = null;
    private Subject<SceneDefinition> collision_notice = new Subject<SceneDefinition>();
    public Subject<SceneDefinition> CollisionNotice
    {
        get { return collision_notice; }
    }

    public Vector2 Pos2D
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
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

    public void Awake()
    {
        this.physics = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Text other_text = other.gameObject.GetComponent<Text>();
        if (null != other_text)
        {
            SceneTransition.Instance.Sound.PlaySe(0);
            collision_notice.OnNext(SceneDefinition.Play);
        }
    }
}
