using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Powder等のトリガーオブジェクトの破棄を行う
/// </summary>
public class TriggerObjectDestroyArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
