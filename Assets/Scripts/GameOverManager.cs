using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    
    private void Awake()
    {
        ActiveGameOverPanel(false);
    }
    public void ActiveGameOverPanel(bool value)
    {
        panel.SetActive(value);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
