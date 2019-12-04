using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Yuuki.MethodExpansions;

//TODO:カメラの位置を固定するコードを書く
public class FixedVRCameraPosition : MonoBehaviour
{
    [SerializeField] Vector3 setupPosition;
    [SerializeField] Camera target;
    [SerializeField] float resetTime;
    IEnumerator routine;
#if UNITY_EDITOR
    [SerializeField] bool isUpdateRecenterPosition = false;
#endif

    private void Awake()
    {
        //XRDevice.DisableAutoXRCameraTracking(target,true);
        OVRManager.display.RecenterPose();
    }

    private void Update()
    {
        #region メニュー長押しでカメラ位置のリセット
        if(OVRInput.Get(OVRInput.RawButton.Start))
        {
            if (routine == null)
            {
                routine = ResetCameraCenterPosition();
                this.StartCoroutine(routine, () => { routine = null; });
            }
        }
        else
        {
            if (routine != null)
            {
                StopCoroutine(routine);
                routine = null;
            }
        }
        #endregion

#if UNITY_EDITOR
        if (!isUpdateRecenterPosition) { return; }
        if(OVRInput.Get(OVRInput.Button.Any))
        {
            OVRManager.display.RecenterPose();
        }
#endif
    }

    IEnumerator ResetCameraCenterPosition()
    {
        var time = Time.time;
        while (Time.time < time + resetTime)
        {
            yield return null;
        }
        OVRManager.display.RecenterPose();
        yield break;
    }
}
