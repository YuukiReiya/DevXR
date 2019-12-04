using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Game
{
    public class Bowl : MonoBehaviour
    {
        /// <summary>
        /// 投入物のIDを格納
        /// 例)砂糖、黒糖.etc)
        /// </summary>
        HashSet<Define.ContentsType> contentsID;
        [SerializeField] GameObject mathCupWater;
        [System.Serializable]
        struct WaterMesh
        {
            public MeshRenderer mesh;
            public Material cloneMat;//複製するマテリアル
        }
        [SerializeField] WaterMesh waterMesh;
        [System.Serializable]
        struct ColorVariation
        {
            public Color sugerOnlyCr;
            public Color saltOnlyCr;
            public Color blackOnlyCr;
            public Color sugerAndSaltCr;
            public Color sugerAndBlackCr;
            public Color saltAndBlackCr;
            public Color all;
        }
        [SerializeField]private ColorVariation colorVariation;

        #region TODO:コードのリファクタリング
        //bit,color
        int[] c_Bits =
        {
            0,1,2,3,
        };

        Dictionary<int, Color> c_Colors =new Dictionary<int, Color>
        {
            //水
            {0,new Color(0,0,0,0) },
            //砂糖
        };

        //////////////////////////////////////////
        /// やること
        //////////////////////////////////////////
        //contentsの判定が汚い
        #endregion

        [System.Serializable]
        struct MixParam
        {
            //座標はXZ平面(x:x,y:z)

            /// <summary>
            /// かき混ぜる対象のオブジェクト
            /// ※水面のオブジェクトをあてる
            /// </summary>
            public GameObject planeObj;

            /// <summary>
            /// ミキサーの開始位置
            /// </summary>
            public Vector2 startPos;

            /// <summary>
            /// ミキサーの1回転終了を判定する位置
            /// </summary>
            public Vector2 endPos;

            /// <summary>
            /// 中心位置から判定する距離
            /// </summary>
            public float length;

            /// <summary>
            /// 混ぜた回数
            /// </summary>
            public uint mixCount;

            /// <summary>
            /// 移動距離の保存
            /// </summary>
            public float saveMoveDistance;
        }
        [SerializeField] MixParam mixParam;
        private void Awake()
        {
            contentsID = new HashSet<Define.ContentsType>();
        }

        // Start is called before the first frame update
        void Start()
        {
            var mat = Instantiate(waterMesh.cloneMat);
            waterMesh.mesh.material = mat;
        }
        /// <summary>
        /// デバッグだお
        /// </summary>
        public UnityEngine.UI.Text tex;

        // Update is called once per frame
        void Update()
        {
            //更新
            tex.text = string.Empty;
            foreach (var it in contentsID)
            {
                tex.text += it.ToString() + "\n";
            }

            //初期化
            if (OVRInput.GetDown(OVRInput.RawButton.A) && OVRInput.GetDown(OVRInput.RawButton.B))
            {
                contentsID.Clear();

            }

            //色をリアルタイムで変更
            ChangeWaterColor();
        }

        private void OnTriggerEnter(Collider other)
        {
            #region 素材投入処理
            IContent content;
            var contentParent = other.transform.parent.parent;

            //判定は厳しめにとっておく
            //※当たり判定をするオブジェクトはコライダーのオブジェクトの親の親(ThrowContentのMainRoutine参照)
            //素材以外なら無視
            if (
                contentParent != null
                &&
                contentParent.TryGetComponent(out content)
                )
            {
                //素材投入処理
                ThrowIn(content);

                //当たり判定のオブジェクトを破棄
                Destroy(other.gameObject);
            }
            #endregion
        }

        void ChangeWaterColor()
        {
            if(contentsID.Contains(Define.ContentsType.Water))
            {
                //水面オブジェクトのアクティブ化
                if (waterMesh.mesh.gameObject.activeSelf) { waterMesh.mesh.gameObject.SetActive(true); }
            }

            int contentsNum = contentsID.Where(it => it != Define.ContentsType.Water).Count();
            //全部
            if (contentsNum == 3)
            {
                waterMesh.mesh.material.color = colorVariation.all;
                return;
            }
            //1種
            else if (contentsNum == 1)
            {
                //砂糖
                if (contentsID.Contains(Define.ContentsType.Sugar))
                {
                    waterMesh.mesh.material.color = colorVariation.sugerOnlyCr;
                    return;
                }
                //塩
                else if (contentsID.Contains(Define.ContentsType.Salt))
                {
                    waterMesh.mesh.material.color = colorVariation.saltOnlyCr;
                    return;
                }
                //黒糖
                else if (contentsID.Contains(Define.ContentsType.BlackSugar))
                {
                    waterMesh.mesh.material.color = colorVariation.blackOnlyCr;
                    return;
                }

            }
            //2種
            else
            {
                //砂糖＆塩
                if (contentsID.Contains(Define.ContentsType.Sugar) && contentsID.Contains(Define.ContentsType.Salt))
                {
                    waterMesh.mesh.material.color = colorVariation.sugerAndSaltCr;
                    return;
                }
                //砂糖＆黒糖
                else if (contentsID.Contains(Define.ContentsType.Sugar) && contentsID.Contains(Define.ContentsType.BlackSugar))
                {
                    waterMesh.mesh.material.color = colorVariation.sugerAndBlackCr;
                    return;
                }
                //塩＆黒糖
                else if (contentsID.Contains(Define.ContentsType.Salt) && contentsID.Contains(Define.ContentsType.BlackSugar))
                {
                    waterMesh.mesh.material.color = colorVariation.saltAndBlackCr;
                    return;
                }
            }

        }

        /// <summary>
        /// 粉ものの投入処理
        /// </summary>
        /// <param name="powder"></param>
        private void ThrowIn(IContent content)
        {
            contentsID.Add(content.Type);

            //見かけを変える処理
            //以下に記述

            //水面のオブジェクトを現出
            if(content.Type==Define.ContentsType.Water)
            {
                waterMesh.mesh.gameObject.SetActive(true);
                mathCupWater.SetActive(false);
            }
        }
    }
}
