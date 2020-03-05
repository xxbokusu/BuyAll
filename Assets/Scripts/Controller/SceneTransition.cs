///<summary><filename>SceneTransition.cs</filename></summary>

using System.Collections;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneTransition : MonoBehaviour, IDisposable
    {
        private static SceneTransition scene_trans = null;
        /// <summary>外部アクセスインスタンス</summary>
        public static SceneTransition Instance { get { return scene_trans; } }

        [SerializeField] private bool is_debug = false;

        private ResourceManager resouce_manager = null;
        private SoundController sound_controller = null;

        private SceneDefinition now_scene = SceneDefinition.Menu;
        private SceneDefinition next_scene = SceneDefinition.Menu;

        public SoundController Sound { get { return sound_controller; } }

        /// <summary>準備完了通知</summary>
        private Subject<Unit> ready_notice = new Subject<Unit>();
        /// <summary>準備完了通知渡し</summary>
        public Subject<Unit> ReadyNotice { get { return ready_notice; } }

        public void Awake()
        {
            scene_trans = this;
            Debug.Util.Log("[SceneTransition] Instance is get Ready");
            DontDestroyOnLoad(this);
            resouce_manager = ResourceManager.CreateInstance();
            Debug.Util.Log("--- Resource Loaded");

            sound_controller = SoundController.CreateInstance();
            sound_controller.setDataLoader(resouce_manager.Bgm, resouce_manager.Se);
            Debug.Util.Log("--- Sound Loaded : " + sound_controller.ToString());

            next_scene = SceneDefinition.Menu;
            ChangeScene(next_scene);
            Debug.Util.Log("--- Scene Changed");
        }

        private void OnGUI()
        {
            GUILayout.Label("", GUILayout.Height(50f));
            if (null == SceneTransition.Instance)
            {
                GUILayout.Label("Instanceがnullになっている");
            }
        }

        void Start() { }

        private void ReadyOk()
        {
            ready_notice.OnNext(new Unit());
        }

        /// <summary>アプリ終了時に破棄する。</summary>
        public void Dispose()
        {
            Destroy(this);
        }

        /// <summary>遷移実行</summary>
        public void ChangeScene(SceneDefinition _next_scene)
        {
            next_scene = _next_scene;
            Debug.Util.LogFormat("Scene {0} is called", _next_scene.ToString());
            ResourceManager.Instance.ChangeScene(next_scene);
        }

        /// <summary>シーン名取得</summary>
        public string get_scene_name(SceneDefinition _scene_type)
        {
            switch (next_scene)
            {
                case SceneDefinition.Initalize:
                    return "Initialize";
                case SceneDefinition.Menu:
                    return "Menu";
                case SceneDefinition.Play:
                    return "Play";
                case SceneDefinition.Ranking:
                    return "Ranking";
                case SceneDefinition.Result:
                    return "Result";
            }
            return "undefined_scene";
        }

        /// <summary>シーン読み込みの進捗確認</summary>
        private IEnumerator load_scene_progress()
        {
            var _proggress = SceneManager.LoadSceneAsync(get_scene_name(next_scene));
            while (false == _proggress.isDone)
            {
                yield return null;
            }

            int _safe_debug_count = 0;// 無限ループを防止する
            while (false == ResourceManager.Instance.IsComplete)
            {
                _safe_debug_count++;
                if (_safe_debug_count > 1000) { break; }
                yield return null;
            }

            SceneManager.UnloadSceneAsync(get_scene_name(now_scene));
            now_scene = next_scene;
            yield break;
        }
    }
}
