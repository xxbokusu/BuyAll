///<summary>
/// <filename>PlayManager.cs</filename>
///</summary>

using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Scene;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Play
{
    public enum PlaySceneType
    {
        Play,
        NextScene,
    }

    public class PlayManager : MonoBehaviour
    {
        private PlaySceneType now_state = PlaySceneType.Play;
        private TouchedOption prev_push_opt;
        [SerializeField] private PlayUIManager ui_manager;
        [SerializeField] private Image play_area;

        void Start()
        {
            if (null != SceneTransition.Instance)
            {
                Debug.Util.Log("[SceneTransition] Instance Ok");
                if (null != SceneTransition.Instance.ReadyNotice)
                {
                    Debug.Util.Log("[SceneTransition] Ready Notice Received");
                    SceneTransition.Instance.ReadyNotice.Subscribe((_ready) => { finish(); });
                }
            }
            else
            {
                Debug.Util.Log("[SceneTransition] Instance is not made.");
            }

            ui_manager.TouchNotice
                .Subscribe((_touch_info) =>
                {
                    select_menu_type(_touch_info.Opt);
                })
                .AddTo(this);
            finish();
            play_area.transform.DOScale(new Vector2(0.3f, 0.3f), 30.0f).OnComplete(() =>
            {
                Debug.Util.Log("[Move to Result] Complete PlayArea Scale");
                set_change_scene_ready(SceneDefinition.Result);
            });
        }

        void Update()
        {
        }

        private void select_menu_type(TouchedOption _type)
        {
            switch (_type)
            {
                case TouchedOption.Result:
                    set_change_scene_ready(SceneDefinition.Result);
                    break;
                default:
                    Debug.Util.LogWarning("Undefined Scene is Called!");
                    break;
            }
        }

        private void finish()
        {
            if (null != SceneTransition.Instance)
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

        private void set_change_scene_ready(SceneDefinition _scene)
        {
            ResourceManager.Instance.CallLoadEndSubject.Subscribe((x) =>
            {
                Debug.Util.Log("[To Result] All load is Complete");
                UnitManagerSystem.Instance.ResetEnemyList();
                SceneManager.LoadScene(SceneTransition.Instance.get_scene_name(_scene));
            }).AddTo(this);
            SceneTransition.Instance.ChangeScene(_scene);
        }
    }
}
