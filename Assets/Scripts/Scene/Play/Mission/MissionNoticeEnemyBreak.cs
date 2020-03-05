///<summary>
/// 概要：このクラスは、ミッションの進行度のために通知を行うためのクラスです。
/// このクラスの通知内容は、敵を倒した時に何が倒されたかを通知するためのものです。
/// ボスの討伐もこれを使用する。
///
/// <filename>
/// MissionNoticeEnemyBreak.cs
/// </filename>
///
/// <creatername>
/// 作成者：堀　明博
/// </creatername>
/// 
/// <address>
/// mailladdress:herie270714@gmail.com
/// </address>
///</summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mission
{

    public class MissionNoticeEnemyBreak : MissionInfoContainerBase
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        private int count;
        public int Count
        {
            get
            {
                return count;
            }
        }

        public MissionNoticeEnemyBreak(string _name)
        {
            name = _name;
            count = 1;
        }

        public MissionNoticeEnemyBreak(string _name, int _count)
        {
            name = _name;
            count = _count;
        }


    }
}
