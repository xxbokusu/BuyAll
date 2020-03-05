///<summary>
/// <filename>DataLoadBase.cs</filename>
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;
using UniRx;

namespace ResourceLoad
{
    public enum LoadDataType
    {
        None,
        UnLoad,
    }

    /// <summary>
    /// リソースの進捗具合を通知するための構造体
    /// </summary>
    public class ResourceLoadProgressInfo
    {
        public int LoadTargetNum { get; private set; }
        public int LoadCompleteNum { get; set; }

        public bool IsCompleate
        {
            get
            {
                if (LoadTargetNum <= 0 && LoadCompleteNum <= 0)
                {
                    Debug.Util.LogWarningFormat("Enpty Load Target");
                    return false;
                }

                if (LoadTargetNum == LoadCompleteNum)
                {
                    return true;
                }
                return false;
            }
        }

        public ResourceLoadProgressInfo(int _load_resource_count)
        {
            LoadTargetNum = _load_resource_count;
            LoadCompleteNum = 0;
        }
    }

    public abstract class DataLoadBase<T> where T : class
    {
        /// <summary> 終了通知</summary>
        private Subject<ResourceLoadProgressInfo> subject = new Subject<ResourceLoadProgressInfo>();

        private bool is_complete;

        private Dictionary<int, T> resource_data_dic = new Dictionary<int, T>();
        /// <summary>読み込み完了済み</summary>
        private SceneDefinition already_loaded_scene = SceneDefinition.Initalize;

        /// <summary>次の読み込み対象</summary>
        private SceneDefinition next_scene_load;

        /// <summary>ロードの進捗具合を通知する</summary>
        public Subject<ResourceLoadProgressInfo> ProgressNotification
        {
            get
            {
                return subject;
            }
        }

        public bool IsComplete
        {
            get
            {
                return is_complete;
            }
        }

        public void LoadResource(SceneDefinition _next_scene)
        {
            next_scene_load = _next_scene;
            UnLoadResouces(already_loaded_scene);
            Observable.FromCoroutine(load_resources).Subscribe();
        }

        protected void UnLoadResouces(SceneDefinition _now_scene)
        {
            resource_data_dic.Clear();
        }

        protected abstract string[] get_resouce_path(SceneDefinition _next_scene);
        /// <summary>指定の型のリソースを読み込む</summary>
        protected virtual IEnumerator load_resources()
        {
            string[] file_paths = get_resouce_path(next_scene_load);
            ResourceLoadProgressInfo load_progress_info = new ResourceLoadProgressInfo(file_paths.Length);

            for (int _i = 0; _i < file_paths.Length; _i++)
            {
                string _path = file_paths[_i];
                Debug.Util.Log("Now Loading : " + _path);
                var _object = Resources.Load(_path) as T;
                if (null == _object)
                {
                    Debug.Util.LogWarningFormat("{0}のロードが失敗しました。:読み込んだシーンは,:{1}", _path, next_scene_load);
                    continue;
                }
                var _resouce = _object as T;
                if (null != _resouce)
                {
                    //FIX;この部分がまだ未対応案件　Enumで管理をしてあげる。
                    Debug.Util.LogFormat("読み込むの成功 path:{0}", _path);
                    if (!resource_data_dic.ContainsKey(_i))
                    {
                        resource_data_dic.Add(_i, _resouce);
                    }
                }
                else
                {
                    Debug.Util.LogWarningFormat("{0}のパスは、ロード失敗 変換も失敗", _path);
                }
                load_progress_info.LoadCompleteNum++;
                yield return null;
            }

            Debug.Util.Log("Target Paths was : " + string.Join(", ", file_paths));
            subject.OnNext(load_progress_info);
            is_complete = true;
            yield return null;
        }

        /// <summary>ロードが完了済みリソースを取得</summary>
        public T GetResource(int _key)
        {
            if (resource_data_dic.ContainsKey(_key))
            {
                return resource_data_dic[_key];
            }
            return null;
        }
    }
}
