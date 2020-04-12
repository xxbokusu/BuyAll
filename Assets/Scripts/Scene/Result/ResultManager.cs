///<summary>
/// <filename>ResultManager.cs</filename>
///</summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Scene;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Result
{
    public class ResultManager : MonoBehaviour
    {
        private TouchedOption prev_push_opt;
        [SerializeField] private ResultUIManager ui_manager;
        [SerializeField] Canvas canvas = null;

        void Start()
        {
            ui_manager.TouchNotice
                .Subscribe((_touch_info) =>
                {
                    select_menu_type(_touch_info.Opt);
                })
                .AddTo(this);
            disp_effect();
        }
        public void disp_effect()
        {
            Image image = ui_manager.GetComponentInChildren<Image>();
            image.transform.DOScale(new Vector2(3, 3), 1.5f).OnComplete(() =>
            {
                listing_shopping_result();
                canvas.transform.DOLocalMove(new Vector2(0, 0), 3.0f);
            });
        }
        public void listing_shopping_result()
        {
            Dictionary<string, int> shopping_result = ScoreManager.Instance.getShoppingResult;
            Dictionary<string, int> shopping_menu = ScoreManager.Instance.ShoppingMenu;
            Text text = ui_manager.GetComponentInChildren<Text>();

            string[] menu_names = new string[ScoreManager.Instance.ShoppingMenu.Keys.Count];
            ScoreManager.Instance.ShoppingMenu.Keys.CopyTo(menu_names, 0);
            for (int index = 0; index < menu_names.Length; index++)
            {
                string name = menu_names[index];
                Debug.Util.Log("[Result] Now Output : " + name);
                if (shopping_result.ContainsKey(name))
                {
                    text.text = text.text + name.Replace(" ", "") + "\n";
                    text.text = text.text + "@" + shopping_menu[name] + "  " + shopping_result[name] + "点  ";
                    int summary = shopping_menu[name] * shopping_result[name];
                    text.text = text.text + summary + "\n";
                }
            }
        }

        void Update() { }

        private void select_menu_type(TouchedOption _type)
        {
            SceneDefinition target_scene;
            switch (_type)
            {
                case TouchedOption.Menu:
                    target_scene = SceneDefinition.Menu;
                    ResourceManager.Instance.CallLoadEndSubject.Subscribe((x) =>
                    {
                        Debug.Util.Log("[To Menu] All load is Complete");
                        UnitManagerSystem.Instance.ResetEnemyList();
                        SceneManager.LoadScene(SceneTransition.Instance.get_scene_name(target_scene));
                    }).AddTo(this);
                    SceneTransition.Instance.ChangeScene(target_scene);
                    break;
                default:
                    Debug.Util.LogWarning("Undefined Scene is Called!");
                    break;
            }
        }
    }
}
