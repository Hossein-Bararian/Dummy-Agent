using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class SystemStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI cpuText;
    public TextMeshProUGUI ramText;
    private float deltaTime;

    void Update()
    {
        // Update FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);

        // Update CPU and RAM usage
        UpdateSystemUsage();
    }

    void UpdateSystemUsage()
    {
        // Get approximate CPU usage (Not exact, but gives an idea)
        float cpuUsage = 1.0f - SystemInfo.processorCount / SystemInfo.processorFrequency;

        // Get used and total RAM
        float usedRAM = (SystemInfo.systemMemorySize - Profiler.GetTotalReservedMemoryLong() / (1024 * 1024));
        float totalRAM = SystemInfo.systemMemorySize;

        cpuText.text = string.Format("CPU: {0:0.0}%", cpuUsage * 100);
        ramText.text = string.Format("RAM:{0:0.0} MB/ {1:0.0} MB", usedRAM, totalRAM);
    }
}