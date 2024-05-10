using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtScore;
    public static int Score;
    private Animator _scoreAnimator;

    private void Start()
    {
         Score = 0;
         PlayerPrefs.SetInt("Score",Score);
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
}