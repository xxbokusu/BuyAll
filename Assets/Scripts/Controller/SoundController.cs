///<summary>
/// <filename>SoundController.cs</filename>
///</summary>

using System.Collections.Generic;
using UnityEngine;
using ResourceLoad;

namespace Scene
{
    public class SoundController : Singleton<SoundController>
    {
        /// <summary>SEの同時再生数</summary>
        private readonly int PlaySEMaximum = 5;

        /// <summary>再生用オブジェクト</summary>
        private GameObject source_attach_object;
        private AudioSource bgm_source;
        private AudioSource se_source;

        /// <summary>AudioClipのデータ</summary>
        private BgmDataLoader bgm_data_base;
        private SeDataLoader se_data_base;

        /// <summary>seの待機列としての情報を保持するためのクラス</summary>
        private class SE_PlayShotWaitData
        {
            /// <summary>この音楽の名前</summary>
            public string EnumName;
            /// <summary>音が終了するまでの時間</summary>
            public float ShotFinishTime;

            /// <summary>排除を行うかどうか？</summary>
            public bool remove_list_flg;

            /// <summary>データを入れるためのコンストラクタ</summary>
            /// <param name="_name"></param>
            /// <param name="_shot_time"></param>
            public SE_PlayShotWaitData(string _name, float _shot_time)
            {
                EnumName = _name;
                ShotFinishTime = _shot_time;
                remove_list_flg = false;
            }

            /// <summary>
            /// 更新による、減時間
            /// </summary>
            public void Update()
            {
                ShotFinishTime -= Time.deltaTime;
                if (ShotFinishTime <= 0)
                {
                    remove_list_flg = true;
                }
            }
        }

        /// <summary>現在流しているSEのリスト</summary>
        private List<SE_PlayShotWaitData> se_shot_list;
        public void setDataLoader(BgmDataLoader _bgm_data, SeDataLoader _se_data)
        {
            bgm_data_base = _bgm_data;
            se_data_base = _se_data;
            init_audiosource_object();
            se_shot_list = new List<SE_PlayShotWaitData>();
        }

        public bool PlaySe(int _enum)
        {
            if (false == se_data_base.IsComplete)
            {
                Debug.Util.LogWarning("Seが鳴らせる状況ではない");
                return false;
            }
            if (se_shot_list.Count >= PlaySEMaximum)
            {
                Debug.Util.Log("Play se is max now");
                return false;
            }

            AudioClip _clip_data = se_data_base.GetResource((int)_enum);
            if (null == _clip_data)
            {
                Debug.Util.Log("SE clip is null");
                return false;
            }
            se_source.PlayOneShot(_clip_data);
            return true;
        }

        public bool PlayBgm(int _enum, bool _force_opt)
        {
            if (null == bgm_source)
            {
                Debug.Util.LogError("bgm source がnull");
                return false;
            }
            if (null == bgm_data_base)
            {
                Debug.Util.LogError("BgmDataBase is null");
                return false;
            }
            if (false == bgm_data_base.IsComplete)
            {
                Debug.Util.LogError("まだBGMのロードが完了していない");
                return false;
            }
            Debug.Util.Log("Now Play : " + bgm_data_base.GetResource((int)_enum));
            AudioClip _clip = bgm_data_base.GetResource((int)_enum);
            if (null == _clip)
            {
                Debug.Util.LogError("BGM clip is null");
                return false;
            }
            if (bgm_source.isPlaying && _force_opt)
            {
                bgm_source.Stop();
            }
            Debug.Util.Log("Now Play Start");
            bgm_source.clip = _clip;
            bgm_source.Play();
            return true;
        }

        public void Destroy() { }

        public void Dispose() { }

        public void Init() { }

        public void LateUpdateMine()
        {
            se_shot_list.RemoveAll(x => (x.remove_list_flg == true));
        }

        public float UnityGUIDraw(Rect _rect)
        {
            if (null == se_shot_list) { return 0; }
            foreach (var _shot_data in se_shot_list)
            {
                GUILayout.Label("name:" + _shot_data.EnumName + " remine_time: " + _shot_data.ShotFinishTime);
            }
            return 0;
        }

        public void UpdateMine()
        {
            foreach (var _shot_data in se_shot_list)
            {
                _shot_data.Update();
            }
            se_shot_list.RemoveAll(x => x.remove_list_flg);
        }

        private void init_audiosource_object()
        {
            if (null != source_attach_object)
            {
                return;
            }
            source_attach_object = new GameObject("SoundSource");
            UnityEngine.Object.DontDestroyOnLoad(source_attach_object);
            bgm_source = source_attach_object.AddComponent<AudioSource>();
            se_source = source_attach_object.AddComponent<AudioSource>();
        }
    }
}
