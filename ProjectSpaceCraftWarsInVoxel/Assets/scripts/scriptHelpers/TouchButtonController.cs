using UnityEngine;
using System.Diagnostics;

public class TouchButtonController : MonoBehaviour
{
    #region Auxiliary Attributes or Properties  [Aren't visible in Editor]
    private Stopwatch eventTimer;
    private float phaseTimeElapsed;
    private Collider touchButtonSizes;
    #endregion

    #region Messages Methods of MonoBehaviour
    void Start()
    {
        touchButtonSizes = GetComponent<Collider>();

        eventTimer = Stopwatch.StartNew();
        eventTimer.Stop();
    }

    void Update()
    {
        UpdateEventTimer();
    }
    #endregion

    #region Other Methods

    #region Aux. Methods: TouchPhaseConverter(TouchPhase touchPhase);  UpdateEventTimer()
    private TouchButtonPhase TouchPhaseConverter(TouchPhase touchPhase)
    {
        switch (touchPhase)
        {
            case TouchPhase.Began: return TouchButtonPhase.Began;
            case TouchPhase.Moved: return TouchButtonPhase.Moved;
            case TouchPhase.Stationary: return TouchButtonPhase.Stationary;
            case TouchPhase.Ended: return TouchButtonPhase.Ended;
            case TouchPhase.Canceled: return TouchButtonPhase.Canceled;
            default: return TouchButtonPhase.None;
        }
    }

    private bool alreadyStarted;

    private void UpdateEventTimer()
    {
        switch (GetTouchPhase())
        {
            case TouchButtonPhase.Began:
            case TouchButtonPhase.Moved:
            case TouchButtonPhase.Stationary:
                if (!alreadyStarted)
                {
                    eventTimer = Stopwatch.StartNew();
                    alreadyStarted = true;
                }
                
                phaseTimeElapsed = eventTimer.ElapsedMilliseconds / 1000.0f;
                break;

            case TouchButtonPhase.None:
            case TouchButtonPhase.Ended:
            case TouchButtonPhase.Canceled:
                eventTimer.Stop();
                phaseTimeElapsed = 0;
                alreadyStarted = false;
                break;
        }
    }
    #endregion

    private TouchButtonPhase lastPhase;
    public bool HasActionPhase()
    {
        for (int index = 0; index < Input.touchCount; index++)
        {
            if (ColliderIntersects.WithVectorFromTouch(touchButtonSizes, index))
            {
                lastPhase = TouchPhaseConverter(Input.GetTouch(index).phase);
                return true;
            }
        }

        lastPhase = TouchButtonPhase.None;
        return false;
    }

    public TouchButtonPhase GetTouchPhase()
    {
        if (HasActionPhase())
        {
            return lastPhase;
        }
        else
        {
            return TouchButtonPhase.None;
        }
    }

    public float GetPhaseTimeElapsed()
    {
        return phaseTimeElapsed;
    }
    #endregion

}

public enum TouchButtonPhase
{
    None = -1,
    Began = 0,
    Moved = 1,
    Stationary = 2,
    Ended = 3,
    Canceled = 4
}