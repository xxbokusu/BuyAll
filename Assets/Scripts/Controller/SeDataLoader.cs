///<summary>
/// <filename>SeDataLoader.cs</filename>
///</summary>

using System.Collections.Generic;
using Scene;
using UnityEngine;

namespace ResourceLoad
{
    public class SePath
    {
        private static Dictionary<SceneDefinition, string[]> scene_resource_path_dic = new Dictionary<SceneDefinition, string[]>() {
            {
                SceneDefinition.Menu,new string[] {
                    "Audio/Se/itemgetsea_pocket",
                    "Audio/Se/tereru_pocket",
                }
            },
            {
                SceneDefinition.Play,new string[] {
                    "Audio/Se/itemgetsea_pocket",
                    "Audio/Se/tereru_pocket",
                }
            },
            {
                SceneDefinition.Result,new string[] {
                }
            },
            {
                SceneDefinition.Ranking,new string[] {
                }
            }
        };

        /// <summary>シーンに合わせたリソースのパスを取得</summary>
        public static string[] GetResoucePaths(SceneDefinition _define)
        {
            if (scene_resource_path_dic.ContainsKey(_define))
            {
                return scene_resource_path_dic[_define];
            }
            return new string[] { "" };
        }
    }

    public class SeDataLoader : DataLoadBase<AudioClip>
    {
        protected override string[] get_resouce_path(SceneDefinition _next_scene)
        {
            return SePath.GetResoucePaths(_next_scene);
        }
    }
}
