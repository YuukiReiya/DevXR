﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace Game
{
    public class FrypanContent : MonoBehaviour
    {
        public HashSet<Define.ContentsType> contentsID;
        [SerializeField] GameObject contentMesh;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            #region ボウルの中身投入処理

            //Debug.Log(":" + other.name);

            ThrowBowlContents content = null;
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