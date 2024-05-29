using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static bool IsOnGame;
    [SerializeField] private TextMeshProUGUI txtBestScore;
    [SerializeField] private TextMeshProUGUI txtLastScore;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Animator muteButtonAnimator;
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
            _isMute = false;
            muteButtonAnimator.Play("Music");
            //play 
        }
        if (! _isMute)
        {
            _isMute = true;
            muteButtonAnimator.Play("Mute");
            //mute
        }
    }

    public void Shop()
    {
        Debug.Log("Shop");
    }

    public void AboutUs()
    {
        Debug.Log("About Us");
    }

    private IEnumerator StartGameCoroutine()
    {
        IsOnGame = true;
        _animator.Play("MenuUI_FadOut");
        yield return new WaitForSeconds(0.3f);
        player.gameObject.GetComponent<Animator>().Play("Walk");
        player.runSpeed = 8;
        yield return new WaitForSeconds(0.2f);
        GameManager.Score = 0;
        PlayerPrefs.SetInt("Score",GameManager.Score);
        gameUI.SetActive(true);
        menuUI.SetActive(false);
    }
    
}
