using System;
using System.Collections;
using UnityEngine;

public class StopMenuManager : MonoBehaviour
{
    public static bool IsGameStop;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject stopMenuPanel;
    [SerializeField] private GameObject gameUiPanel;

    public void StopStatus()
    {
        if(PlayerManager.IsDead) return;
        if (!IsGameStop)
        {
           
            Time.timeScale = 0;
            gameUiPanel.GetComponent<Animator>().SetTrigger("FadOut");
            StartCoroutine(ResumeAfterGameUiAnimation());
            stopMenuPanel.SetActive(true);
            IsGameStop = true;
        }
        else
        {
            animator.SetTrigger("FadeOut");
            StartCoroutine(ResumeAfterAnimation());
        }
    }

    private IEnumerator ResumeAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);
        stopMenuPanel.SetActive(false);
        gameUiPanel.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Time.timeScale = Mathf.SmoothStep(0, 1, elapsedTime / 1f);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
        IsGameStop = false;
    }
    
    private IEnumerator ResumeAfterGameUiAnimation()
    {
        yield return new WaitForSecondsRealtime(gameUiPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        gameUiPanel.SetActive(false);
    }
    
}
