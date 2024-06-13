using TMPro;
using UnityEngine;

public class SystemStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float deltaTime;

    void Update()
    { 
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);

    }

}