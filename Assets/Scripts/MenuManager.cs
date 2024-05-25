using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI txtBestScore;
    [SerializeField] private TextMeshProUGUI txtLastScore;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Animator muteButtonAnimator;
    private bool _isMute = false;
    private void Awake()
    {
        player.runSpeed = 0;
        menuUI.SetActive(true);
        gameUI.SetActive(false);
    }

    private void Start()
    {
        txtLastScore.text = PlayerPrefs.GetInt("Score").ToString();
        txtBestScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void StartGame()
    {
        StartCoroutine(Wait(0.5f));
        player.gameObject.GetComponent<Animator>().Play("Walk");
        StartCoroutine(Wait(0.15f));
        GameManager.Score = 0;
        PlayerPrefs.SetInt("Score",GameManager.Score);
        player.runSpeed = 8;
        menuUI.SetActive(false);
        gameUI.SetActive(true);
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

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
}
