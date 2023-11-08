using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public float duration { get; private set; }
    public float remaining { get; private set; }
    public float step { get; private set; }
    public bool isPause { get; private set; }
    private WaitForSeconds waitStep;

    private MonoBehaviour _mono;
    private Action _onDone;
    private Action<float> _onUpdate;
    public Timer(MonoBehaviour mono, float duration, float step, Action onDone, Action<float> onUpdate)
    {
        _mono = mono;
        this.duration = duration;
        this.step = step;
        _onDone = onDone;
        _onUpdate = onUpdate;
    }
    public void Start()
    {
        remaining = duration;
        _mono.StartCoroutine(IECounting());
    }
    private IEnumerator IECounting()
    {
        waitStep = new WaitForSeconds(step);
        while (true)
        {
            yield return waitStep;
            if (isPause) continue;
            remaining -= step;
            _onUpdate?.Invoke(remaining);
            if(remaining <= 0)
            {
                remaining = 0;
                _onDone?.Invoke();
                break;
            }
        }
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = true;
    }
    public float RatioRemaing()
    {
        return remaining / duration;
    }
}
