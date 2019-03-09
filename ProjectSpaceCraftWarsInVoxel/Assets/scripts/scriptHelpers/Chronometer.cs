using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <license>
/// Copyright (C) 2016-04-29 Tiago Penha Pedroso
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </license>

public class Chronometer
{

    //properties
    public ITimed ITimedObject { get; set; }
    public bool IsStopped { get; set; }

    //main attributes
    private Stopwatch chronometer;
    private bool chronometerWasInitiated;
    private float timeGoal;
    private bool activeRandomStartTime;
    private bool activeRandomTimeGoal;

    //Auxs
    private float elapsed;
    private float sortedWaitTime = -1;

    private bool isReseted;

    private Stopwatch chronWaitTime;
    private bool isPausedWaitTime;
    private float waitTime;

    private Stopwatch chronometerAux;
    private int minNumber;
    private int maxNumber;



    #region Constructor
    /// <summary>
    /// Basic Constructor of class.
    /// </summary>
    public Chronometer()
    {
        chronometer = Stopwatch.StartNew();
        Reset();
    }

    /// <summary>
    /// Constructor that allows set the time goal.
    /// </summary>
    /// <param name="timeGoalInSeconds">The goal value in seconds</param>
    public Chronometer(float timeGoalInSeconds) : base()
    {
        SetTimeGoal(timeGoalInSeconds);
    }

    public Chronometer(float timeGoalInSeconds, ITimed objectWithThisInterface) : base()
    {
        SetTimeGoal(timeGoalInSeconds);
        ITimedObject = objectWithThisInterface;
    }
    #endregion

    #region Basic Methods: Start, GetSortedWaitTime, Restart, SetRandomLimitWaitTime, Reset, Stop, Pause, Resume and InitChronWaitTime
    public void UpdateCounter()
    {
        GetElapsed();
    }


    /// <summary>
    /// Starts the time marker in seconds.
    /// </summary>
    public void Start()
    {
        if (validRandomLimitWaitTime && !isInitedChronWaitTime)
        {
            Reset();
            sortedWaitTime = RandomNumber.GetFloat(minNumber, maxNumber);
            InitChronWaitTime(sortedWaitTime);
        }
        else
        {
            chronometer = Stopwatch.StartNew();
            chronometerWasInitiated = true;
            isReseted = false;
            IsStopped = false;
            sortedWaitTime = -1;
        }
    }

    /// <summary>
    /// Starts the time marker in seconds.
    /// </summary>
    public void Start(float waitTime)
    {
        if (waitTime > 0)
        {
            Reset();
            InitChronWaitTime(waitTime);
        }
        else
        {
            chronometer = Stopwatch.StartNew();
            chronometerWasInitiated = true;
            isReseted = false;
            IsStopped = false;
        }
    }

    public float GetSortedWaitTime()
    {
        return sortedWaitTime;
    }

    /// <summary>
    /// Restarts the time marker in seconds. This is the same method of Start().
    /// </summary>
    public void Restart()
    {
        Start();
    }

    private bool validRandomLimitWaitTime;

    public void SetRandomLimitWaitTime(int minNumber, int maxNumber)
    {
        if (minNumber >= 0 && minNumber < maxNumber)
        {
            validRandomLimitWaitTime = true;
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
        }
        else
        {
            validRandomLimitWaitTime = false;
        }
    }

    /// <summary>
    /// Stops and Resets the time marker.
    /// Note: If you desire resume the time, you should use the method Pause().
    /// </summary>
    public void Reset()
    {
        if (chronometerWasInitiated)
        {
            chronometer.Stop();
            isReseted = true;
            timeGoal = 0;
        }

    }

    /// <summary>
    /// Stops and Resets the time marker.
    /// Note: If you desire resume the time, you should use the method Pause().
    /// </summary>
    public void Stop()
    {
        Reset();
        IsStopped = true;
    }

    /// <summary>
    /// Stops the time marker until the method Resume() have been invoked.
    /// </summary>
    public void Pause()
    {
        if (chronometerWasInitiated && !isReseted)
        {
            chronometer.Stop();
        }
    }

    /// <summary>
    /// Stops the time marker during a certain time or until the method Resume() have been invoked.
    /// Note: The method UpdateWaitTime() should be invoked in MonoBehaviour.Update() for that the auto resume time works.
    /// Or the method GetElapsed() invokes this method too.
    /// </summary>
    /// <param name="waitTime">The wait time for auto resume time</param>
    public void Pause(float waitTime)
    {
        Pause();
        isPausedWaitTime = true;
        InitChronWaitTime(waitTime);
    }

    /// <summary>
    /// Resumes the pause time.
    /// </summary>
    public void Resume()
    {
        if (!isReseted)
        {
            chronometer.Start();
        }
    }

    private bool isInitedChronWaitTime;

    private void InitChronWaitTime(float waitTime)
    {
        chronWaitTime = Stopwatch.StartNew();
        chronometerWasInitiated = true;
        this.waitTime = waitTime;
        isInitedChronWaitTime = true;
    }
    #endregion

    #region Controllers Methods: UpdateWaitTime, GetElapsed, SetTimeGoal, SetRandomTimeGoal, GetTimeGoal and IsReachTimeGoal.
    /// <summary>
    /// <summary>
    ///  When the method Pause(float waitTime) is invoked, this method 
    /// should  be invoked in MonoBehaviour.Update() for that the auto resume time works.
    /// P.S.: The method GetElapsed() invokes this method too.
    /// </summary>
    public void UpdateWaitTime()
    {
        if (isPausedWaitTime)
        {
            if ((chronWaitTime.ElapsedMilliseconds / 1000.0f) >= waitTime)
            {
                chronWaitTime.Stop();
                isPausedWaitTime = false;
                isInitedChronWaitTime = false;
                Resume();
            }
        }

        if (!isPausedWaitTime && isInitedChronWaitTime)
        {
            if ((chronWaitTime.ElapsedMilliseconds / 1000.0f) >= waitTime)
            {
                chronWaitTime.Stop();
                isInitedChronWaitTime = false;
                Start(0);
            }
        }
    }



    /// <summary>
    /// Returns the elapsed time, from time marker, in seconds.
    /// </summary>
    /// <returns>float</returns>
    public float GetElapsed()
    {
        if (isPausedWaitTime || isInitedChronWaitTime)
        {
            UpdateWaitTime();
        }

        if (isReseted)
        {
            return 0.0f;
        }
        else
        {
            float now = chronometer != null ? chronometer.ElapsedMilliseconds / 1000.0f : 0;
            //float now = chronometer.ElapsedMilliseconds / 1000.0f;
            IsReachTimeGoal();
            return GetTimeGoal() > 0 && now >= GetTimeGoal() ? GetTimeGoal() : now;
        }
    }

    /// <summary>
    /// Defines a goal for the time marker.
    /// </summary>
    /// <param name="timeInSeconds">The goal value in seconds</param>
    public void SetTimeGoal(float timeInSeconds)
    {
        if (timeInSeconds > 0)
        {
            timeGoal = timeInSeconds;
        }
    }

    public void SetRandomTimeGoal(int minNumber, int maxNumber)
    {
        SetTimeGoal(RandomNumber.GetFloat(minNumber, maxNumber));
    }

    public float GetTimeGoal()
    {
        return timeGoal;
    }

    /// <summary>
    /// Returns the status of the goal from the time marker.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsReachTimeGoal()
    {
        float now = chronometer != null ? chronometer.ElapsedMilliseconds / 1000.0f : 0;

        if (timeGoal > 0 && now >= timeGoal)
        {
            if (ITimedObject != null)
            {
                ITimedObject.OnReachTimeGoal();
            }

            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Overrides: ToString
    public override string ToString()
    {
        return "" + GetElapsed();
    }
    #endregion

}
