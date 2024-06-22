using System.Collections;
using Cinemachine;
using RTLTMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool IsOnGame;
    [SerializeField] private RTLTextMeshPro txtBestScore;
    [SerializeField] private RTLTextMeshPro txtLastScore;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Animator muteButtonAnimator;
    [SerializeField] private Animator levelFaderAnimator;
    [SerializeField] private GameObject hand;
    private Animator _animator;
    [Space(40)]
    [Header("Shop")]
    [SerializeField] private new CinemachineVirtualCamera camera;
    private Animator _cameraAnimator;
  
    
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
        _cameraAnimator = camera.gameObject.GetComponent<Animator>();
        txtLastScore.text = PlayerPrefs.GetInt("Score").ToString();
        txtBestScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void StartGame()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            PlayerPrefs.SetInt("Tutorial",1);
            SceneManager.LoadScene(3);
        }
        StartCoroutine(StartGameCoroutine());
    }

    public void MuteMusic()
    {
        if (_isMute)
        {
            PlayerPrefs.SetInt("Tutorial", 0);
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
        _animator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(_animator.GetCurrentAnimatorStateInfo(0).length/1.5f);
        _cameraAnimator.SetTrigger("ZoomIn");
    }
    private IEnumerator AboutUs()
    {
        yield break;
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
        _animator.SetTrigger("FadeOut");
        hand.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -90), .6f);
        yield return new WaitForSeconds(0.3f);
        IsOnGame = true;
        player.gameObject.GetComponent<Animator>().Play("Walk");
        player.runSpeed = 8;
        yield return new WaitForSeconds(0.2f);
        GameManager.HeadShots = 0;
        GameManager.Score = 0;
        PlayerPrefs.SetInt("HeadShots", GameManager.HeadShots);
        PlayerPrefs.SetInt("Score", GameManager.Score);
        menuUI.SetActive(false);
        gameUI.gameObject.GetComponent<Animator>().SetTrigger("FadeIn");
        gameUI.SetActive(true);
     
    }
}