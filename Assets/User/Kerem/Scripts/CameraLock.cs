using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using AC;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CameraLock : CinemachineExtension
{
    public bool XLockEnabled = false;
    [Tooltip("Lock the camera's X position to this value")]
    public float m_XPosition = 0;
    public bool YLockEnabled = false;
    [Tooltip("Lock the camera's Y position to this value")]
    public float m_YPosition = 0;
    public bool ZLockEnabled = false;
    [Tooltip("Lock the camera's Z position to this value")]
    public float m_ZPosition = 0;

    private void Start()
    {
        KickStarter.mainCamera.Enable();
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            if(XLockEnabled)
                pos.x = m_XPosition;
            if(YLockEnabled)
                pos.y = m_YPosition;
            if(ZLockEnabled)
                pos.z = m_ZPosition;
            state.RawPosition = pos;
        }
    }
}
