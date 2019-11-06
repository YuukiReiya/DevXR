using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    /// <summary>
    /// 投入物のIDを格納
    /// 例)砂糖、黒糖.etc)
    /// </summary>
    Queue<uint> contentsID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Charge()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Powder powder;
        if (other.TryGetComponent(out powder))
        {
            ThrowIn(powder);
        }
    }

    /// <summary>
    /// 粉ものの投入処理
    /// </summary>
    /// <param name="powder"></param>
    private void ThrowIn(Powder powder)
    {
        contentsID.Enqueue((uint)powder.type);
    }
}
