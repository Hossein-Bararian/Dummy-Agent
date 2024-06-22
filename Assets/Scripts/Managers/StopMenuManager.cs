using System;
using System.Collections;
using UnityEngine;

public class StopMenuManager : MonoBehaviour
{
    public static bool IsGameStop; 
    private Animator _animator;
    [SerializeField] private GameObject stopMenuPanel;
    [SerializeField] private GameObject gameUiPanel;

    private void Start()
    {
        _animator = stopMenuPanel.GetComponent<Animator>();
    }

    public void StopStatus()
    {
        if(PlayerManager.IsDead) return;
        if (!IsGameStop)
        {
           SlowMotionMechanic.Instance.StopSlowMotion();
            Time.timeScale = 0;
            gameUiPanel.GetComponent<Animator>().SetTrigger("FadeOut");
            StartCoroutine(ResumeAfterGameUiAnimation());
            stopMenuPanel.SetActive(true);
            IsGameStop = true;
        }
        else
        {
            _animator.SetTrigger("FadeOut");
            StartCoroutine(ResumeAfterAnimation());
        }
    }

    private IEnumerator ResumeAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(_animator.GetCurrentAnimatorStateInfo(0).length);
        stopMenuPanel.SetActive(false);
        gameUiPanel.SetActive(true);
        Time.timeScale = 1;
        IsGameStop = false;
    }
    
    private IEnumerator ResumeAfterGameUiAnimation()
    {
        yield return new WaitForSecondsRealtime(gameUiPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        gameUiPanel.SetActive(false);
    }
    
}
