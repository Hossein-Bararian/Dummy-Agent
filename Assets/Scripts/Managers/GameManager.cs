using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownMenu;
    [SerializeField] private Button muteButton;
    [SerializeField] private TextMeshProUGUI[] menuTexts;
    [SerializeField] private Image[] menuImage;
    [Space(35)]
    [SerializeField] private TextMeshProUGUI txtScore;
    public static int Score;
    public static int HeadShots;
    private Animator _scoreAnimator;

    private void Start()
    {
        Application.targetFrameRate = 80;
        _scoreAnimator = txtScore.GetComponent<Animator>();
        txtScore.text = "Score: 0";
        print(PlayerPrefs.GetInt("Tutorial"));
        TutorialCheck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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

    private void TutorialCheck()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            foreach (var text in menuTexts)
            {
              text.color =new Color(1,1,1,0.3f);
            }
            foreach (var image in menuImage)
            {
               image.color=new Color(1,1,1,0.3f);
            }
            muteButton.interactable = false;
            muteButton.GetComponent<Animator>().Play("MusicLowOpacity");
            dropdownMenu.gameObject.GetComponent<Image>().color =new Color(1,1,1,0.3f);
            dropdownMenu.interactable = false;
        }
    }
}