using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMover : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    IEnumerator routine;
    public void Execute(Vector3 dir)
    {
        routine = MainRoutine(dir.normalized);
        StartCoroutine(routine);
    }

    IEnumerator MainRoutine(Vector3 nDir)
    {
        while(true)
        {
            transform.position += nDir * speed * Time.deltaTime;
            yield return null;
        }
        //yield break;
    }

    private void OnDestroy()
    {
        if (routine != null) { StopCoroutine(routine); }
    }
}
