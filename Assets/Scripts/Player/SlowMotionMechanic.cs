using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SlowMotionMechanic : MonoBehaviour
{
    [SerializeField] private float slowTime;
    [SerializeField] private float slowTimeEffect;
    [SerializeField] private Volume volume;
    private float _startScaleTime;
    private float _startFixedDeltaTime;


    private void Start()
    {
        _startScaleTime = Time.timeScale;
        _startFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetButton("SlowMotion"))
        {
            SlowMotion(slowTime);
        }

        if (Input.GetButtonUp("SlowMotion"))
        {
            StopSlowMotion();
        }
    }

    private void SlowMotion(float time)
    {
        Time.timeScale = time;
        Time.fixedDeltaTime = time * _startFixedDeltaTime;
        SlowMotionEffects(true);
        // slow sound and music
    }

    private void StopSlowMotion()
    {
        SlowMotionEffects(false);
        Time.timeScale = _startScaleTime;
        Time.fixedDeltaTime = _startFixedDeltaTime;
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