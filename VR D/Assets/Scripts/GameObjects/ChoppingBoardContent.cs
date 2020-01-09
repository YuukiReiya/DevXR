using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace Game
{
    /// <summary>
    /// フレイパンの中身をまな板に移す
    /// </summary>
    public class ChoppingBoardContent : MonoBehaviour
    {
        //検索してもよかったが、参照持たせてグローバルスコープにしたほうが早い
        [SerializeField] FrypanContent refineContent;
        public HashSet<Define.ContentsType> contentsID;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            #region フライパンの中身投入処理

            ThrowFlypanContent content = null;

            var contentParent = other.transform.parent != null ? other.transform.parent.parent : null;

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

        private void ThrowIn(ThrowBowlContents content)
        {
            contentsID = content.Content.contentsID;

            //見かけを変える処理
            //以下に記述

            //焼くオブジェクト現出
            content.Content.WaterMeshObject.SetActive(false);
            contentMesh.SetActive(true);
        }
    }
}