using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtScore;
    public static int Score;
    private void Start()
    {
        txtScore.text = "Score: 0";
    }
    public void UpdateScore(int value)
    {
        Score += value;
        txtScore.text = "Score: "+Score;
        //some effects
        //some sounds maybe !
        //some particles maybe @
    }

}
