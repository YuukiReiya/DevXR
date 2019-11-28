using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuuki.MethodExpansions;
public class Powder : MonoBehaviour
{
    public enum Type : uint
    {
        Sugar = 0,
        BlackSuger,
        Salt,
    }
    //serialize param
    [SerializeField] Type type_;
    [SerializeField,Tooltip("発生したエフェクトの親オブジェクト")] GameObject instantiatePositionObject_;
    [SerializeField,Tooltip("当たり判定を持たせた空のオブジェクトのプレハブ")] GameObject hitEmptyObjectPrefab;
    //private param
    IEnumerator[] routines;
    const int c_RoutineCount = 5;
    //public param
    //accessor
    public Type type { get { return type_; } }

    // Start is called before the first frame update
    void Start()
    {
        routines = new IEnumerator[c_RoutineCount];
    }

    // Update is called once per frame
    void Update()
    {
        //毎フレ判定するお！
        Shake();
    }

    /// <summary>
    /// 振った時の処理
    /// </summary>
    void Shake()
    {
        if(IsShake())
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
        Debug.Log("2つのなす角 ＝ " + angle);

        //傾いているか
        if (!(angle > 90)) { return false; }


        return true;
    }

    IEnumerator MainRoutine()
    {
        //エフェクトの生成と再生タイミング

        //エフェクト
        //var effect = PowderPool.Instance.GetObject().GetComponent<ParticleSystem>();
        //{
        //    effect.gameObject.transform.parent = instantiatePositionObject_.transform;
        //    effect.gameObject.transform.position = instantiatePositionObject_.transform.position;
        //    effect.transform.localScale = Vector3.one;
        //    effect.Play();
        //}
        ////当たり判定を確認するための空オブジェクト
        //{
        //    var hitObj = Instantiate(hitEmptyObjectPrefab);
        //    hitObj.transform.parent = instantiatePositionObject_.transform;
        //    hitObj.transform.localPosition = Vector3.zero;
        //    var mover = hitObj.GetComponent<GameObjectMover>();
        //    mover.Execute(Vector3.down);
        //}


        //yield return new WaitWhile(() => effect.IsAlive(true));
        ////エフェクトの再生終了時
        ////effect.transform.parent = PowderPool.Instance.gameObject.transform;
        //effect.gameObject.SetActive(false);
        yield break;
    }

    void ResetRoutine(int index)
    {
        routines[index] = null;
    }
}
