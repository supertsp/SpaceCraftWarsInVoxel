using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <license>
/// Copyright (C) 2016-05-02 Tiago Penha Pedroso
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

public class MathProgression
{

    Type progressionType;
    float firstTerm;
    float ratioConstant;

    public enum Type
    {
        ARITHMETIC, GEOMETRIC
    }

    #region Constructor
    public MathProgression(Type progressionType, float firstTerm, float ratioConstant)
    {
        this.progressionType = progressionType;
        this.firstTerm = firstTerm;
        this.ratioConstant = ratioConstant;
    }
    #endregion

    #region Basic Methods: CalculateTermN and CalculateSumUntilN
    public float CalculateTermN(int termIndex)
    {
        if (progressionType == Type.ARITHMETIC)
        {
            return firstTerm + ((termIndex - 1) * ratioConstant);
        }

        if (progressionType == Type.GEOMETRIC)
        {
            return (float)(firstTerm * Math.Pow(ratioConstant, (termIndex - 1)));
        }

        return 0;
    }       

    public float CalculateSumUntilN(int termIndex)
    {
        if (progressionType == Type.ARITHMETIC)
        {
            return ((firstTerm + CalculateTermN(termIndex)) * termIndex) / 2.0f;
        }

        if (progressionType == Type.GEOMETRIC)
        {
            return (float)(firstTerm * ((Math.Pow(ratioConstant, termIndex) - 1) / (ratioConstant - 1)));
        }

        return 0;
    }
    #endregion

    #region Arrays Methods: GetArray and GetIntegerArray
    public float[] GetArray(int quantityTerms, int initialIndex)
    {
        float[] array = new float[quantityTerms];

        for (int i = 0; i < quantityTerms; i++)
        {
            array[i] = CalculateTermN(initialIndex);
            initialIndex++;
        }

        return array;
    }

    public float[] GetArray(int quantityTerms)
    {
        return GetArray(quantityTerms, 1);
    }

    public int[] GetIntegerArray(int quantityTerms, int initialIndex)
    {
        int[] array = new int[quantityTerms];

        for (int i = 0; i < quantityTerms; i++)
        {
            array[i] = (int)CalculateTermN(initialIndex);
            initialIndex++;
        }

        return array;
    }

    public int[] GetIntegerArray(int quantityTerms)
    {
        return GetIntegerArray(quantityTerms, 1);
    }
    #endregion

}
