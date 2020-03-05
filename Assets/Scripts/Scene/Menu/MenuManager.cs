///<summary>
/// <filename>MenuManager.cs</filename>
///</summary>

using UnityEngine;
using UniRx;
using Scene;
using UnityEngine.SceneManagement;

namespace Menu
{
    public enum TouchedOption
    {
        None,
        GameStart,
        Ranking,
    }

    public class MenuManager : MonoBehaviour
    {
        private TouchedOption prev_push_menu_type;
        [SerializeField] private MenuUIManager menu_ui_manager;
        [SerializeField] private TitleUnitBase title_unit;

        void Start()
        {
            if (null != SceneTransition.Instance)
            {
                if (null != SceneTransition.Instance.ReadyNotice)
                {
                    Debug.Util.Log("[SceneTransition] Ready Notice Received");
                    SceneTransition.Instance.ReadyNotice.Subscribe(
                        (_ready) => { finish(); });
                }
            }
            else
            {
                Debug.Util.Log("[SceneTransition] Instance is not made.");
            }

            menu_ui_manager.TouchNotice
                .Subscribe((_touch_info) =>
                {
                    select_menu_type(_touch_info.Opt);
                })
                .AddTo(this);
            finish();

            title_unit.CollisionNotice.Subscribe((scene) =>
            {
                change_scene(scene);
            }).AddTo(this);
        }

        void Update()
        {
        }

        private void select_menu_type(TouchedOption _type)
        {
            switch (_type)
            {
                case TouchedOption.GameStart:
                    change_scene(SceneDefinition.Play);
                    break;
                case TouchedOption.Ranking:
                    SceneTransition.Instance.ChangeScene(SceneDefinition.Ranking);
                    SceneManager.LoadScene(SceneTransition.Instance.get_scene_name(SceneDefinition.Ranking));
                    break;
                default:
                    Debug.Util.LogWarning("Undefined Scene is Called!");
                    break;
            }
        }
        private void change_scene(SceneDefinition _scene)
        {
            ResourceManager.Instance.CallLoadEndSubject.Subscribe((x) =>
            {
                Debug.Util.Log("[To Play] All load is Complete");
                SceneManager.LoadScene(SceneTransition.Instance.get_scene_name(_scene));
            }).AddTo(this);
            SceneTransition.Instance.ChangeScene(_scene);
        }

        private void finish()
        {
            if (null == SceneTransition.Instance.Sound)
            {
                Debug.Util.Log("SoundController is null!");
            }
            else
            {
                SceneTransition.Instance.Sound.PlayBgm(0, true);
            }
        }

    }
}
