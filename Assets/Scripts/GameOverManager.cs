using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject gameOverButtons;
    [SerializeField] private TextMeshProUGUI txtHeadShots;
  
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
        txtHeadShots.text = PlayerPrefs.GetInt("HeadShots").ToString();
        yield return new WaitForSeconds(0.2f);
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
            txtHeadShots.GetComponentInChildren<Image>().enabled = true;
            txtHeadShots.GetComponent<Animator>().Play("NewRecord");
        }
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
        Addressables.LoadSceneAsync(levelIndex);
    }
}
