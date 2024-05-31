using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool IsOnGame;
    [SerializeField] private TextMeshProUGUI txtBestScore;
    [SerializeField] private TextMeshProUGUI txtLastScore;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Animator muteButtonAnimator;
    [SerializeField] private Animator levelFaderAnimator;
    private Animator _animator;
    
    private bool _isMute = false;

    private void Awake()
    {
        IsOnGame = false;
        player.runSpeed = 0;
        menuUI.SetActive(true);
        gameUI.SetActive(false);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        txtLastScore.text = PlayerPrefs.GetInt("Score").ToString();
        txtBestScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void MuteMusic()
    {
        if (_isMute)
        {
            PlayerPrefs.SetInt("HighScore", 0);
            _isMute = false;
            muteButtonAnimator.Play("Music");
            //play 
        }

        if (!_isMute)
        {
            _isMute = true;
            muteButtonAnimator.Play("Mute");
            //mute
        }
    }

    private IEnumerator Shop()
    {
        levelFaderAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(1);
    }
    private IEnumerator AboutUs()
    {
        levelFaderAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(2);
    }

    public void MenuButton(string sceneName)
    {
        switch (sceneName)
        {
            case "shop":
                StartCoroutine(Shop());
                break;
            case "aboutUs":
                StartCoroutine(AboutUs());
                break;
        }
    }

    private IEnumerator StartGameCoroutine()
    {
        IsOnGame = true;
        _animator.Play("MenuUI_FadOut");
        yield return new WaitForSeconds(0.3f);
        player.gameObject.GetComponent<Animator>().Play("Walk");
        player.runSpeed = 8;
        yield return new WaitForSeconds(0.2f);
        GameManager.HeadShots = 0;
        GameManager.Score = 0;
        PlayerPrefs.SetInt("HeadShots", GameManager.HeadShots);
        PlayerPrefs.SetInt("Score", GameManager.Score);
        gameUI.SetActive(true);
        menuUI.SetActive(false);
    }
}