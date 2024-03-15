using System;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Shake(CinemachineImpulseSource impulseSource, float impulseForce)
    {
        impulseSource.GenerateImpulseWithForce(impulseForce);
    }
}