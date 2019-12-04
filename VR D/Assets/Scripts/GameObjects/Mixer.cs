using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// かき混ぜるオブジェクト
    /// </summary>
    public class Mixer : MonoBehaviour
    {
        [SerializeField] float distance = 1.0f;
        Vector2 pos_xz;
        Vector2 prevPos_xz;
        float saveDistance;
        public uint MixCount { get; private set; }

        public UnityEngine.UI.Text debugMixText;

        private void OnTriggerStay(Collider other)
        {
            #region かき混ぜる処理
            //ミキサーが触れたら混ぜるための始点を設定
            if (other.tag == "MixContent")
            {
                Vector2 pos = new Vector2(
                    transform.position.x,
                    transform.position.z);
                //現座標と1フレ前の座標を比較
                var distance = Vector2.Distance(pos, prevPos_xz);
                saveDistance += distance;
                if (saveDistance > this.distance)
                {
                    //一回分のかき混ぜ完了したときに通る
                    saveDistance = 0;
                    MixCount++;

                    //実装
                    MixProcess();

                    //デバッグ
                    debugMixText.text = MixCount.ToString();
                }
                prevPos_xz = pos;
            }
            #endregion
        }

        /// <summary>
        /// 1回分のかき混ぜ処理
        /// </summary>
        void MixProcess()
        {
            //エフェクトだしたり。。。
        }
    }
}