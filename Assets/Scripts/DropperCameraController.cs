using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DropperCameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private CinemachineVirtualCamera cameraController;
    private CinemachineFramingTransposer transposer;

    private float softZoneWidth;
    private float softZoneHeight;

    public bool lockX = false;
    public bool lockY = false;

    private void Awake()
    {
        transposer = cameraController.GetComponent<CinemachineFramingTransposer>();
        softZoneWidth = transposer.m_SoftZoneWidth;
        softZoneHeight = transposer.m_SoftZoneHeight;
        
    }

    private void Update()
    {
        if (lockX)
        {
            transposer.m_SoftZoneWidth = 1.0f;
            transposer.m_DeadZoneWidth = 1.0f;
        }
        else
        {
            transposer.m_SoftZoneWidth = softZoneWidth;
            transposer.m_DeadZoneWidth = 0;
        }


        if (lockY)
        {
            transposer.m_SoftZoneHeight = 1.0f;
            transposer.m_DeadZoneHeight = 1.0f;
        }
        else
        {
            transposer.m_SoftZoneHeight = softZoneHeight;
            transposer.m_DeadZoneHeight = 0;
        }
    }
}
