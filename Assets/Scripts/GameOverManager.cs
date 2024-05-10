using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject gameOverButtons;
    [SerializeField] private TextMeshProUGUI txtScore;
  
    private void Awake()
    {
        gameOverButtons.SetActive(false);
        _anim = GetComponent<Animator>();
    }

    public IEnumerator CrossFadeRestartLevel()
    {
        SlowMotionMechanic.Instance.StopSlowMotion();
        yield return new WaitForSeconds(0.7f);
        _anim.SetTrigger("Fade");
        yield return new WaitForSeconds(0.8f);
        gameOverButtons.SetActive(true);
        gameOverButtons.GetComponent<Animator>().Play("ButtonsPanel_FadeIn");
        txtScore.text = "Your Score: "+PlayerPrefs.GetInt("Score");
        yield return new WaitForSeconds(0.8f);
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
            txtScore.GetComponentInChildren<Image>().enabled = true;
            txtScore.GetComponent<Animator>().Play("NewIcon");
        }
    }

    public void btn_Ads()
    {
        //Show Ads Later ...
    }
    
    public void btn_Level(int levelIndex)
    {
        StartCoroutine("Buttons_FadeOut",levelIndex);
    }

    private IEnumerator Buttons_FadeOut(int levelIndex)
    {
        gameOverButtons.SetActive(true);
        gameOverButtons.GetComponent<Animator>().Play("ButtonsPanel_FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIndex);
    }

    
}
