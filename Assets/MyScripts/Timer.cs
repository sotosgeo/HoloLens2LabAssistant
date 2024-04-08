using System.Diagnostics;
using UnityEngine;

[System.Serializable]
public class Timer
{
    private Stopwatch watch;
    [SerializeField]
    private int current_frame = 0;

    public double AverageFPS
    {
        get
        {
            if (!watch.IsRunning)
                return 0;

            return current_frame / (0.001 * watch.ElapsedMilliseconds);
        }
    }
    public void CountFrame() => current_frame++;
    public void Start()
    {
        current_frame = 0;
        watch = Stopwatch.StartNew();
    }
    public void Stop()
    {
        watch.Stop();
    }
}