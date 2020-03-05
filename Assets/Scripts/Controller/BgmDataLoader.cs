///<summary>
/// <filename>BGMDataLoader.cs</filename>
///</summary>

using System.Collections.Generic;
using Scene;
using UnityEngine;

namespace ResourceLoad
{
    public class BgmPath
    {
        private static Dictionary<SceneDefinition, string[]> scene_resource_path_dic = new Dictionary<SceneDefinition, string[]>() {
            {
                SceneDefinition.Menu,new string[] {
                    "Audio/Bgm/lo-fai_dova",
                    "Audio/Bgm/neiki_bitachi",
                }
            },
            {
                SceneDefinition.Play,new string[] {
                    "Audio/Bgm/shopping1_dova",
                    "Audio/Bgm/shopping2_dova",
                }
            },
            {
                SceneDefinition.Result,new string[] {
                    "Audio/Bgm/result",
                }
            },
            {
                SceneDefinition.Ranking,new string[] {
                    "Audio/Bgm/ranking",
                }
            }
        };

        /// <summary>シーンに合わせたリソースのパスを取得</summary>
        public static string[] GetResoucePaths(SceneDefinition scene_name)
        {
            if (scene_resource_path_dic.ContainsKey(scene_name))
            {
                return scene_resource_path_dic[scene_name];
            }
            return new string[] { "" };
        }
    }

    public class BgmDataLoader : DataLoadBase<AudioClip>
    {
        protected override string[] get_resouce_path(SceneDefinition _next_scene)
        {
            return BgmPath.GetResoucePaths(_next_scene);
        }
    }


}
