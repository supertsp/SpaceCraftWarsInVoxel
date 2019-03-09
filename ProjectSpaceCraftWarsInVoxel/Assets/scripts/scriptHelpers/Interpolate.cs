using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

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

public class Interpolate
{

    float initialValue, finalValue, timeGoalInSeconds;
    Chronometer timeGoal;
    float sizeIncrement;
    float increment;
    float result;
    bool reachEnd;
    bool startFirstTime;

    float divSec = 30.0f;
    float timeIncrement;
    float microTimeLimit;

    #region Constructor
    public Interpolate(float initialValue, float finalValue, float timeGoalInSeconds)
    {
        Start(initialValue, finalValue, timeGoalInSeconds);
    }
    #endregion

    #region Method: GetCurrentValue()
    public float GetCurrentValue()
    {
        if (!reachEnd)
        {
            if (!startFirstTime)
            {
                startFirstTime = true;
                timeGoal.Start();
            }

            if (timeGoal.GetElapsed() >= microTimeLimit)
            {
                result += initialValue < finalValue ? increment : -increment;
                microTimeLimit += timeIncrement;
            }

            if (timeGoal.IsReachTimeGoal())
            {
                reachEnd = true;
                return finalValue;
            }

            if (initialValue < finalValue && result > finalValue)
            {
                reachEnd = true;
                return finalValue;
            }

            if (initialValue > finalValue && result < finalValue)
            {
                reachEnd = true;
                return finalValue;
            }

            return result;
        }

        return finalValue;
    }

    public float GetCurrentPingPongValue()
    {
        float tempValue = GetCurrentValue();

        if (tempValue == finalValue)
        {
            Start(finalValue, initialValue, timeGoalInSeconds);
        }

        return tempValue;
    }
    #endregion

    #region Auxiliary Method: Start(), GetPositiveValue()
    private void Start(float initialValue, float finalValue, float timeGoalInSeconds)
    {
        this.initialValue = initialValue;
        this.finalValue = finalValue;
        this.timeGoalInSeconds = timeGoalInSeconds;

        timeIncrement = 1.0f / divSec;
        microTimeLimit = timeIncrement;

        timeGoal = new Chronometer(timeGoalInSeconds);

        sizeIncrement = timeGoalInSeconds * divSec;
        increment = GetPositiveValue(initialValue - finalValue) / sizeIncrement;

        result = initialValue;

        reachEnd = false;
        startFirstTime = false;
    }

    private float GetPositiveValue(float value)
    {
        return (float)Math.Sqrt(Math.Pow(value, 2));
    }
    #endregion

}