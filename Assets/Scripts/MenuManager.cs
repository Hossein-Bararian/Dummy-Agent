using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtBestScore;
    [SerializeField] private TextMeshProUGUI txtLastScore;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private PlayerMovement player;
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
        print("Muted");
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
}
