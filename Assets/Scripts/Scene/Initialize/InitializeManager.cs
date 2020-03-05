///<summary>
/// <filename>InitializeManager.cs</filename>
///</summary>

using UnityEngine;
using UniRx;
using Scene;
using UnityEngine.SceneManagement;

namespace Initialize
{
    public enum TouchedOption
    {
        GameStart,
        Ranking,
    }

    public class InitializeManager : MonoBehaviour
    {
        void Start()
        {
            if (null != SceneTransition.Instance && null != ResourceManager.Instance)
            {
                Debug.Util.Log("[SceneTransition, ResourceMAnager] Notice Set");
                SceneTransition.Instance.ChangeScene(SceneDefinition.Menu);
                var merge_notice = Observable.Merge(
                    SceneTransition.Instance.ReadyNotice,
                    ResourceManager.Instance.CallLoadEndSubject
                );
                merge_notice.Buffer(2).Subscribe(
                    (_ready) =>
                    {
                        Debug.Util.Log("[To Menu] All load is Complete");
                        SceneManager.LoadScene(SceneTransition.Instance.get_scene_name(SceneDefinition.Menu));
                    }).AddTo(this);
            }
            else
            {
                Debug.Util.Log("[SceneTransition or ResourceManager] Instance is not made.");
            }
        }

        void Update() { }
    }
}