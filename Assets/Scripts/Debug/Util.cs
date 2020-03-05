///<summary>
/// <filename>Util.cs</filename>
///</summary>

using UnityEngine;
using System.Diagnostics;

namespace Debug {
    /// <summary>EDITOR_DEBUG定義時用関数</summary>
    public class Util {
        [Conditional("EDITOR_DEBUG")]
        public static void Log(string _message) {
            UnityEngine.Debug.Log(_message);
        }

        [Conditional("EDITOR_DEBUG")]
        public static void LogFormat(string _message, params object[] _params) {
            UnityEngine.Debug.LogFormat(_message, _params);
        }

        [Conditional("EDITOR_DEBUG")]
        public static void LogWarning(string _message) {
            UnityEngine.Debug.LogWarning(_message);
        }

        [Conditional("EDITOE_DEBUG")]
        public static void LogWarningFormat(string _message, params object[] _params) {
            UnityEngine.Debug.LogWarningFormat(_message, _params);
        }

        [Conditional("EDITOR_DEBUG")]
        public static void LogError(string _message) {
            UnityEngine.Debug.LogError(_message);
        }

        [Conditional("EDITOR_DEBUG")]
        public static void LogErrorFormat(string _message, params object[] _params) {
            UnityEngine.Debug.LogErrorFormat(_message, _params);
        }
    }

    /// <summary>
    /// 描画系のデバック機能
    /// </summary>
    public class Drawer {
        /// <summary>
        /// ラインを描画する。点と点をつなぐイメージ
        /// </summary>
        /// <param name="_pos">始点</param>
        /// <param name="_pos2">終点</param>
        /// <param name="_color">色</param>
        [Conditional("EDITOR_DEBUG")]
        public static void DrawLine(Vector3 _pos, Vector3 _pos2, Color _color)
        {
            UnityEngine.Debug.DrawLine(_pos, _pos2, _color);
        }

        public static void DrawLineRay(Vector3 _start_pos, Vector3 _dir, Color _color)
        {
            Vector3 _pos2 = _start_pos + _dir;
            UnityEngine.Debug.DrawLine(_start_pos, _pos2, _color);
        }

        /// <summary>
        /// レイの描画
        /// </summary>
        /// <param name="_start">始点</param>
        /// <param name="_dir">方向</param>
        /// <param name="_color">色</param>
        /// <param name="_display_duration">描画期間(たぶん秒)この期間を過ぎると、消える</param>
        /// <param name="_depth_test">オブジェクトが手前にあった時に隠れるか？</param>
        [Conditional("EDITOR_DEBUG")]
        public static void DrawRay(Vector3 _start, Vector3 _dir, Color _color, float _display_duration, bool _depth_test)
        {
            UnityEngine.Debug.DrawRay(_start, _dir, _color, _display_duration, _depth_test);
        }

        /// <summary>
        /// ベクトル等を表現する際に、作成するもの
        /// 矢印型に出現する
        /// </summary>
        /// <param name="_start_pos">開始位置</param>
        /// <param name="_dir">ベクトル</param>
        /// <param name="_color">色</param>
        [Conditional("EDITOR_DEBUG")]
        public static void DrawArrow(Vector3 _start_pos, Vector3 _dir, Color _color)
        {
            UnityEngine.Debug.DrawLine(_start_pos, _start_pos + _dir, _color);
            Vector3 _invers_dir = -_dir.normalized;
            Vector3 _finish_pos = _start_pos + _dir;
            Vector3 _left_dir = Quaternion.Euler(0, 30, 0) * _invers_dir;
            Vector3 _right_dir = Quaternion.Euler(0, -30, 0) * _invers_dir;
            UnityEngine.Debug.DrawLine(_finish_pos, _finish_pos + _left_dir * 100, _color);
            UnityEngine.Debug.DrawLine(_finish_pos, _finish_pos + _right_dir * 100, _color);
        }

    }

}//end Logger
