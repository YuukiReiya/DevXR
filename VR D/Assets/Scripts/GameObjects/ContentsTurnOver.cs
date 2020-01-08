using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuuki.MethodExpansions;
namespace Game
{
    /// <summary>
    /// フライパンの中身をひっくり返す
    /// </summary>
    public class ContentsTurnOver : MonoBehaviour
    {
        [Header("param")]
        [SerializeField] GameObject contentObject;
        [System.Serializable]
        struct AnimationParam
        {
            [Tooltip("フライパンの中身の相対的な位置(初期値)")] public Transform initTransform;
            public float height;//上昇させる高さ
            public AnimationCurve heightCurve;//高さのカーブ
            public float rot;//何度
            public AnimationCurve rotCurve;//回転のカーブ
            public float time;//アニメーションにかける時間
        }
        [SerializeField] AnimationParam animParam;
        [SerializeField] Rigidbody rootRigidBody;
        public System.Action func;//ひっくり返した時の処理
        IEnumerator routine;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnCollisionEnter(Collision collision)
        {
            Debug.LogError("フライ返し");
            Execute();
        }

        void Execute()
        {
            if (routine != null) { return; }
            routine = Mainroutine();
            this.StartCoroutine(routine,()=> { routine = null; });

            //加えられた力をゼロにする
            rootRigidBody.AddForce(Vector3.zero, ForceMode.VelocityChange);
        }

        IEnumerator Mainroutine()
        {
            var p = animParam;
            var time = Time.time;
            while (Time.time < time + p.time)
            {
                var elapsed = Time.time - time;
                //高さ
                var hRatio = p.heightCurve.Evaluate(elapsed / (p.time <= 0 ? 1 : p.time));
                var pos = contentObject.transform.localPosition;
                pos.y = hRatio * p.height;
                contentObject.transform.localPosition = pos;
                //回転
                var rot = contentObject.transform.localRotation.eulerAngles;
                var rRatio = p.rotCurve.Evaluate(elapsed / (p.time <= 0 ? 1 : p.time));
                rot.x = rRatio * p.rot;

                //Lerp
                rot.x = Mathf.Lerp(0, p.rot, Utility.Normalize(0, 1, elapsed));
                rot.y = 0;
                rot.z = 0;
                //sin
                //rot.x=Mathf.Sin(Mathf.Deg2Rad*)
                //rot.x = Mathf.Sin(elapsed)*p.rot;

                contentObject.transform.localRotation = Quaternion.Euler(rot);
                yield return null;
            }

            //ひっくり返した時の処理
            func?.Invoke();


            //補正
            contentObject.transform.localPosition = p.initTransform.localPosition;
            //var rq = contentObject.transform.localRotation;
            //rq.x = p.rot;
            contentObject.transform.localRotation = p.initTransform.localRotation;
            contentObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            yield break;
        }

    }
}