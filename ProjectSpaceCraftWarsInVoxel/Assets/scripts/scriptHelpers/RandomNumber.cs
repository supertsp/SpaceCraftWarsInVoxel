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

public class RandomNumber
{

    float minNumber, maxNumber;    
    List<int> arrayInt;
    List<float> arrayFloat;

    #region Constructor
    public RandomNumber(float minNumber, float maxNumber)
    {
        if (minNumber < maxNumber)
        {
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
            arrayInt = new List<int>();
            arrayFloat = new List<float>();
        }
    }
    #endregion

    #region Basic Methods: GetUnique and ToString
    Chronometer timeLimitRaffle = new Chronometer(0.003f);

    public float GetUniqueFloat()
    {
        float num;

        timeLimitRaffle.Start();
        do
        {
            num = GetFloat(minNumber, maxNumber);
        } while (arrayFloat.Contains(num) && !timeLimitRaffle.IsReachTimeGoal());
        
        if (timeLimitRaffle.IsReachTimeGoal())
        {
            num = -1;
        }
        else
        {
            arrayFloat.Add(num);
        }

        return num;
    }

    public int GetUniqueInt()
    {
        int num;

        do
        {
            num = GetInt((int)minNumber, (int)maxNumber);
        } while (arrayInt.Contains(num) && arrayInt.Count <= ((int)maxNumber - (int)minNumber - 1));

        if (arrayInt.Count > ((int)maxNumber - (int)minNumber - 1))
        {
            num = -1;
        }
        else
        {
            arrayInt.Add(num);
        }

        return num;
    }

    public override string ToString()
    {
        string message =
            "RandomNumber {\n" +
            "  + Elements Integer (" + arrayInt.Count + "):\n   ";

        int countCol = 1;
        foreach (var item in arrayInt)
        {
            if (countCol < 11)
            {
                message += item.ToString() + ", ";
            }
            else
            {
                message += "\n   " + item.ToString() + ", ";
                countCol = 0;
            }

            countCol++;
        }

        message = message.Substring(0, (message.Length - 2));

        message += "\n\n  + Elements Float (" + arrayFloat.Count + "):\n   ";

        countCol = 1;
        foreach (var item in arrayFloat)
        {
            if (countCol < 11)
            {
                message += item.ToString() + ", ";
            }
            else
            {
                message += "\n   " + item.ToString() + ", ";
                countCol = 0;
            }

            countCol++;
        }

        message = message.Substring(0, (message.Length - 2));

        message += "\n}";

        return message;
    }
    #endregion    

    #region Static Methods: Get and GetInteger
    static System.Random random = new System.Random();

    /// <summary>
    /// Returns a random number between 0.0f [inclusive] and 1.0[exclusive].
    /// </summary>
    /// <returns>float</returns>
    public static float GetFloat()
    {
        return (float)random.NextDouble();
    }

    /// <summary>
    /// Returns a random number, based on machine time, between 0 [inclusive] and 10[exclusive].
    /// </summary>
    /// <returns>int</returns>
    public static int GetInt()
    {
        return random.Next();
    }

    /// <summary>
    /// Returns a random number, based on machine time, between minNumber and maxNumber.
    /// When the difference between minNumber and maxNumber is very SMALL (for example: 0.002) 
    /// the raffle is usually INCLUSIVE, as for very LARGE differences (for example: 1) 
    /// the raffle is usually EXCLUSIVE.
    /// </summary>
    /// <returns>float</returns>
    public static float GetFloat(float minNumber, float maxNumber)
    {
        if (minNumber < maxNumber)
        {
            float numSorted = minNumber + (GetFloat() * (maxNumber - minNumber));
            return numSorted;
        }

        return -1;
    }

    /// <summary>
    /// Returns a random number, based on machine time, between minNumber [inclusive] and maxNumber[exclusive].
    /// </summary>
    /// <returns>int</returns>
    public static int GetInt(int minNumber, int maxNumber)
    {
        if (minNumber < maxNumber)
        {
            return random.Next(minNumber, maxNumber);
        }

        return -1;
    }

    static List<float> floatList;

    public static List<float> GetFloatList(float minNumber, float maxNumber, int sizeList)
    {
        floatList = new List<float>();

        for (int i = 0; i < sizeList; i++)
        {
            floatList.Add(GetFloat(minNumber, maxNumber));
        }

        return floatList;
    }

    public static List<float> GetFloatListOrderly(float minNumber, float maxNumber, int sizeList)
    {
        floatList = GetFloatList(minNumber, maxNumber, sizeList);
        floatList.Sort();
        return floatList;
    }

    static List<int> intList;

    public static List<int> GetIntList(int minNumber, int maxNumber, int sizeList)
    {
        intList = new List<int>();

        for (int i = 0; i < sizeList; i++)
        {
            intList.Add(GetInt(minNumber, maxNumber));
        }

        return intList;
    }

    public static List<int> GetIntListOrderly(int minNumber, int maxNumber, int sizeList)
    {
        intList = GetIntList(minNumber, maxNumber, sizeList);
        intList.Sort();
        return intList;
    }
    #endregion

}