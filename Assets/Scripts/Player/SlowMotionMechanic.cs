using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SlowMotionMechanic : MonoBehaviour
{
    private bool _isOnSlowMotion;

    [SerializeField] private float slowTime;
    [SerializeField] private float slowTimeEffect;
    [SerializeField] private Volume volume;
    private float _startScaleTime;
    private float _startFixedDeltaTime;
    [Header("SlowMotion Bar")] [SerializeField]
    private Slider slowMotionBar;
    [SerializeField] private float increaseSlowValue;
    [SerializeField] private float decreaseSlowValue;


    private void Start()
    {
        _startScaleTime = Time.timeScale; 
        _startFixedDeltaTime = Time.fixedDeltaTime;
        StopSlowMotion();
    }

    private void Update()
    {
        CheckInputs();
        if (slowMotionBar.value < 100 && !_isOnSlowMotion)
        {
            RecoverSlowMotionBar();
        }
    }

    private void CheckInputs()
    {
        if (Input.GetButton("SlowMotion"))
        {
            StartSlowMotion(slowTime);
        }

        if (Input.GetButtonUp("SlowMotion"))
        {
            StopSlowMotion();
        }
    }

    private void StartSlowMotion(float time)
    {
        _isOnSlowMotion = true;
        Time.timeScale = time;
        Time.fixedDeltaTime = time * _startFixedDeltaTime;
        UseSlowMotionBar();
        SlowMotionEffects(true);
        if (slowMotionBar.value <= 0)
        {
            StopSlowMotion();
        }
        
    }

    private void StopSlowMotion()
    {
        _isOnSlowMotion = false;
        Time.timeScale = _startScaleTime;
        Time.fixedDeltaTime = _startFixedDeltaTime;
        SlowMotionEffects(false);
    }

    private void RecoverSlowMotionBar()
    {
        slowMotionBar.value += increaseSlowValue * Time.unscaledDeltaTime;
    }

    private void UseSlowMotionBar()
    {
        slowMotionBar.value -= decreaseSlowValue * Time.unscaledDeltaTime;
    }

    private void SlowMotionEffects(bool active)
    {
        if (active)
        {
            if (volume.profile.TryGet(out Vignette vignetteStart))
            {
                vignetteStart.active = true;
                vignetteStart.intensity.value = Mathf.MoveTowards(vignetteStart.intensity.value, 0.3f,
                    slowTimeEffect * Time.unscaledDeltaTime);
                vignetteStart.smoothness.value = Mathf.MoveTowards(vignetteStart.smoothness.value, 1,
                    slowTimeEffect * Time.unscaledDeltaTime);
            }

            if (volume.profile.TryGet(out ChromaticAberration chromaticAberrationStart))
            {
                chromaticAberrationStart.active = true;
                chromaticAberrationStart.intensity.value = Mathf.MoveTowards(chromaticAberrationStart.intensity.value,
                    0.35f, slowTimeEffect * Time.unscaledDeltaTime);
            }
        }
        else
        {
            if (volume.profile.TryGet(out Vignette vignetteEnd))
            {
                vignetteEnd.intensity.value = 0;
                vignetteEnd.smoothness.value = 0.01f;
                vignetteEnd.active = false;
            }

            if (volume.profile.TryGet(out ChromaticAberration chromaticAberrationEnd))
            {
                chromaticAberrationEnd.intensity.value = 0;
                chromaticAberrationEnd.active = false;
            }
        }
    }
}