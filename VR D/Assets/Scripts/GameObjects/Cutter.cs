using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour {

    // 動く方向で切断する場合
    //  private Vector3 prePos = Vector3.zero;
    //  private Vector3 prePos2 = Vector3.zero;

    //  void FixedUpdate ()
    //  {
    //      prePos = prePos2;
    //      prePos2 = transform.position;
    //  }

    [SerializeField] Transform cutObjParent;

    #region 拡張
    private void Start()
    {
        //切断後に実装する関数
        MeshCut.InstantiateFunc = it =>
        {
            //親オブジェクトの変更
            it.transform.parent = cutObjParent;
            //MEMO:親に合わせてトランスフォームも調整

            //必要ならOVRGrabbableの初期化
            OVRGrabbable grab = it.AddComponent<OVRGrabbable>();
            if (!it.TryGetComponent(out grab))
            {
                Debug.LogError("Cutter.cs line39 TryGetComponent for Instansiate callback!");
                return;
            }
            grab.SetupGrabPoints(it.GetComponents<Collider>());
        };
    }
    #endregion

    // このコンポーネントを付けたオブジェクトのCollider.IsTriggerをONにする
    void OnTriggerEnter(Collider other)
    {
        var meshCut = other.gameObject.GetComponent<MeshCut>();
        if (meshCut == null) { return; }
         //一方向のみで切断する方法、方向については適宜変更
        var cutPlane = new Plane (transform.right, transform.position);
        //動きで切断する場合
        //var cutPlane = new Plane (Vector3.Cross(transform.forward.normalized, prePos - transform.position).normalized, transform.position);
        meshCut.Cut(cutPlane);
    }

}