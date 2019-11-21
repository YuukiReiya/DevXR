using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    /// <summary>
    /// 投入物のIDを格納
    /// 例)砂糖、黒糖.etc)
    /// </summary>
    HashSet<Powder.Type> contentsID;

    private void Awake()
    {
        contentsID = new HashSet<Powder.Type>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public UnityEngine.UI.Text tex;
    // Update is called once per frame
    void Update()
    {
        //更新
        tex.text = string.Empty;
        foreach(var it in contentsID)
        {
            tex.text += it.ToString()+"\n";
        }
    }

    void Charge()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Powder powder;
        var powderGetObject = other.transform.parent.parent;
        //判定は厳しめにとっておく
        if (powderGetObject == null) { return; }
        //粉ものの当たり判定でなければ抜ける
        if (!powderGetObject.TryGetComponent(out powder)) { return; }

        //粉ものの投入
        ThrowIn(powder);

        Destroy(other.gameObject);
    }

    /// <summary>
    /// 粉ものの投入処理
    /// </summary>
    /// <param name="powder"></param>
    private void ThrowIn(Powder powder)
    {
        //contentsID.Enqueue(powder.type);
        contentsID.Add(powder.type);

        //見かけを変える処理
        //以下に記述

    }

}
