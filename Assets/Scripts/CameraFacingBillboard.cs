using UnityEngine;
using System.Collections;

/// <summary>
/// I stole this script from the internet. Yaaaar!
/// </summary>
public class CameraFacingBillboard : MonoBehaviour
{
    public Camera m_Camera;

    void Update()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}