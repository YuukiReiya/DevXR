using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuuki.MethodExpansions;
using Common;
namespace Game
{
    #region !致命的な問題!
    //GameobjectMoverのExecute関数で生成したオブジェクトを動かしているが、
    //親オブジェクトの影響を受けてしまうので、移動が真下にいってない
    //
    //修正案件:ランクA
    #endregion


    /// <summary>
    /// ボウルの中身をフライパンに投入する処理
    /// </summary>
    public class ThrowBowlContents : MonoBehaviour
    {
        /// <summary>
        /// 傾けた際に当たり判定をするオブジェクトを生成するが、そのオブジェクト
        /// </summary>
        [SerializeField] GameObject dummyHitPrefab;
        [SerializeField] Transform hitObjParent;
        [SerializeField] BowlContent content;
        //private param
        IEnumerator[] routines;
        const int c_RoutineCount = 5;

        public BowlContent Content { get { return content; } }

        // Start is called before the first frame update
        void Start()
        {
            routines = new IEnumerator[c_RoutineCount];
        }

        // Update is called once per frame
        void Update()
        {
            //水が入ってなければ処理しない
            if (!content.contentsID.Contains(Define.ContentsType.Water)) { return; }
            
            //毎フレ判定するお！
            Shake();
        }

        void Shake()
        {
            if (IsShake())
            {
                for (int i = 0; i < routines.Length; ++i)
                {
                    if (routines[i] != null) { continue; }
                    routines[i] = MainRoutine();

                    int index = i;
                    this.StartCoroutine(routines[i], () => { ResetRoutine(index); });
                }
            }
        }

        /// <summary>
        /// 振られたか判定
        /// </summary>
        bool IsShake()
        {
            //オブジェクトの上向きベクトルを取得
            var upvec = this.gameObject.transform.up.normalized;
            var angle = Vector3.Angle(upvec, Vector3.up);

            //TODO:0～360と-180～180で処理が変わってしまう？(前者な気がする)
            //傾いているか
            if (!(angle > 90)) { return false; }


            return true;
        }

        IEnumerator MainRoutine()
        {
            //当たり判定を確認するための空オブジェクト
            {
                var hitObj = Instantiate(dummyHitPrefab);
                hitObj.transform.parent = hitObjParent;
                hitObj.transform.localPosition = Vector3.zero;
                var mover = hitObj.GetComponent<GameObjectMover>();
                mover.Execute(Vector3.down);
            }
            //エフェクトの再生終了時
            yield break;
        }

        void ResetRoutine(int index)
        {
            routines[index] = null;
        }

    }
}