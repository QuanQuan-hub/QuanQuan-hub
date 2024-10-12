using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoSigleton<CameraManger>
{
    public Camera MainCamera;
    [Tooltip("fgui摄像机")]
    public Camera StageCamera;
    public override void OnCreate()
    {

    }
}
