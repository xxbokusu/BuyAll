///<summary>
/// <filename>SimpleTouch.cs</filename>
///</summary>

using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using ClickReceive;

public class SimpleTouch<T> : MonoBehaviour, IClickReceive,
    IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    where T : class
{
    private Subject<T> observer_receive = new Subject<T>();
    public Subject<T> ObserverReceive
    {
        get
        {
            return observer_receive;
        }
    }

    public void Start()
    {
        UIControlSystem.CreateInstance();
        UIControlSystem.Instance.SetNotificationGameObject(this.gameObject);

        observer_receive.AddTo(this.gameObject);
    }


    protected virtual T createdData(PointerEventData _event_data)
    {
        return null;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UIControlSystem.Instance.SetReactionGameObject(eventData.pointerId, this.gameObject);
    }

}
