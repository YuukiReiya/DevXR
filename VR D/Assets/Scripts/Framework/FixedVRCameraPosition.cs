using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//TODO:カメラの位置を固定するコードを書く
public class FixedVRCameraPosition : MonoBehaviour
{
    [SerializeField] Vector3 setupPosition;
    [SerializeField] Camera target;

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
#if UNITY_EDITOR
        if (!isUpdateRecenterPosition) { return; }
        if(OVRInput.Get(OVRInput.Button.Any))
        {
            OVRManager.display.RecenterPose();
        }
#endif
    }
}
