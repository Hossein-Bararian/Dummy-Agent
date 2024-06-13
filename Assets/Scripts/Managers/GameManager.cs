using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtScore;
    public static int Score;
    public static int HeadShots;
    private Animator _scoreAnimator;

    private void Start()
    {
        Application.targetFrameRate = 80;
        _scoreAnimator = txtScore.GetComponent<Animator>();
        txtScore.text = "Score: 0";
    }

    public void UpdateScore(int value)
    {
        Score += value;
        txtScore.text = "Score: " + Score;
        _scoreAnimator.Play("GetScore");
        PlayerPrefs.SetInt("Score",Score);
    }
    public  void UpdateHeadShots()
    {
        HeadShots++;
        PlayerPrefs.SetInt("HeadShots",HeadShots);
    }
}