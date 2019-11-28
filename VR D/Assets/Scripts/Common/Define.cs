using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 宣言・Util関数のみ
    /// ※静的または定数
    /// </summary>
    public static class Define
    {
        //素材のタイプ
        public enum ContentsType : uint
        {
            //水
            Water = 0,
            //砂糖
            Sugar = 1,
            //塩
            Salt = 2,
            //黒糖
            BlackSugar = 4,
        }

    }
}
