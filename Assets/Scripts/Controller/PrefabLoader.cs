///<summary>
/// <filename>PrefabLoader.cs</filename>
///</summary>

using System.Collections.Generic;
using Scene;
using UnityEngine;

namespace ResourceLoad
{
    public class PrefabPath
    {
        private static Dictionary<SceneDefinition, string[]> scene_resource_path = new Dictionary<SceneDefinition, string[]>() {
        {
            SceneDefinition.Menu,new string[] {
            }
        },
        {
            SceneDefinition.Play,new string[] {
                "prefab/UI/Play/Text",
                "prefab/UI/Play/ShoppingBag",
                "prefab/UI/Play/ShoppingBag2",
                "prefab/UI/Play/ShoppingBag3",
            }
        },
        {
            SceneDefinition.Result,new string[] {
                "prefab/UI/Result/Recipe",
            }
        }
        };

        /// <summary>シーンに合わせたリソースのパスを取得</summary>
        public static string[] GetResoucePaths(SceneDefinition scene_name)
        {
            if (scene_resource_path.ContainsKey(scene_name))
            {
                return scene_resource_path[scene_name];
            }
            return new string[] { "" };
        }
    }

    public class PrefabLoader : DataLoadBase<GameObject>
    {
        protected override string[] get_resouce_path(SceneDefinition _next_scene)
        {
            return PrefabPath.GetResoucePaths(_next_scene);
        }
    }
}
