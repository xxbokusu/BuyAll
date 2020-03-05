///<summary>
/// <filename>SceneTransFade.cs</filename>
///</summary>

using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

namespace FadeSystem
{
    public enum FadeType
    {
        Finish,     //完了
        FadeIn,     //FadeIn
        FadeOut,    //FadeOut
    }

    public enum FadeEndType
    {
        FadeInFinish,
        FadeOutFinish
    }

    public class SceneTransFade : MonoBehaviour, IDisposable
    {
        private Subject<FadeEndType> finish_fade_subject = new Subject<FadeEndType>();

        [SerializeField]
        private Image fade_image = null;
        private float fade_sec;
        private float elapsed = 0.0f;
        private float change_alpha_value = 0.0f;
        private FadeType fade_mode = FadeType.Finish;
        public Subject<FadeEndType> FinishSubject
        {
            get
            {
                return finish_fade_subject;
            }
        }

        void Start()
        {
            DontDestroyOnLoad(this);
        }

        void Update()
        {
            if (fade_mode == FadeType.Finish)
            {
                return;
            }

            if (fade_mode == FadeType.FadeIn)
            {
                elapsed += Time.deltaTime;
                Color _color = fade_image.color;
                _color.a = elapsed * change_alpha_value;
                fade_image.color = _color;

                if (elapsed > fade_sec)
                {
                    finish_fade_subject.OnNext(FadeEndType.FadeInFinish);
                    fade_mode = FadeType.Finish;
                }
            }
            else if (fade_mode == FadeType.FadeOut)
            {
                elapsed += Time.deltaTime;
                Color _color = fade_image.color;
                _color.a = (fade_sec - elapsed) * change_alpha_value;
                fade_image.color = _color;

                if (elapsed > fade_sec)
                {
                    finish_fade_subject.OnNext(FadeEndType.FadeOutFinish);
                    fade_mode = FadeType.Finish;
                }
            }
        }

        public void FadeIn(float _sec)
        {
            change_alpha_value = 1f / _sec;
            fade_sec = _sec;
            fade_mode = FadeType.FadeIn;
            elapsed = 0f;
        }

        public void FadeOut(float _sec)
        {
            change_alpha_value = 1f / _sec;
            fade_sec = _sec;
            fade_mode = FadeType.FadeOut;
            elapsed = 0f;
        }

        public void Dispose()
        {
            Destroy(this);
        }
    }
}
