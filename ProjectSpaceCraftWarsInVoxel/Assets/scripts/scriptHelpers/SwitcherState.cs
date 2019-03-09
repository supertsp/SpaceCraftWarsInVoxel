using UnityEngine;
using UnityEngine.UI;
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

public class SwitcherState<S>
{

    List<S> states;

    //Auxs
    int indexCurrent = -1;
    bool firstUse = true;

    #region Constructors
    public SwitcherState()
    {
        states = new List<S>();
    }

    public SwitcherState(S firstState) : this()
    {
        Add(firstState);
    }

    public SwitcherState(S firstState, S secondState) : this(firstState)
    {
        Add(secondState);
    }
    #endregion

    #region Add, Get, Remove, Length & ToString
    public void Add(S newState)
    {
        if (firstUse)
        {
            firstUse = false;
            indexCurrent++;
        }

        states.Add(newState);
    }

    public void Add(S firstState, S secondState)
    {
        Add(firstState);
        Add(secondState);
    }

    public S Get(int index)
    {
        return states[index];
    }

    public void Remove(int index)
    {
        if (index == indexCurrent)
        {
            indexCurrent++;

            if (indexCurrent >= Length())
            {
                indexCurrent = 0;
            }
        }

        if (index > -1 && index < Length())
        {
            states.RemoveAt(index);
        }
        
        if (Length() == 0)
        {
            indexCurrent = -1;
            firstUse = true;
        }
    }

    public void Remove()
    {
        Remove(Length() - 1);
    }

    public int Length()
    {
        return states.Count;
    }

    public override string ToString()
    {
        string message = 
            "SwitcherState {\n" +
            "  + States: ";

        foreach (var item in states)
        {
            message += item.ToString() + ", ";
        }

        message = message.Substring(0, (message.Length - 2));

        message += "\n  + Current State: [" + GetCurrentIndex() + "] " + GetCurrent() + "\n}";

        return message;
    }
    #endregion

    #region GetCurrent, GetCurrentIndex, ChangeState, ChangeStateTo
    public S GetCurrent()
    {
        return states[indexCurrent];
    }

    public int GetCurrentIndex()
    {
        return indexCurrent;
    }

    public void ChangeState()
    {
        indexCurrent++;

        if (indexCurrent >= Length())
        {
            indexCurrent = 0;
        }

        if (Length() == 0)
        {
            indexCurrent = -1;
            firstUse = true;
        }
    }

    public void ChangeStateTo(int index)
    {
        indexCurrent = index;

        if (indexCurrent >= Length())
        {
            indexCurrent = 0;
        }

        if (Length() == 0)
        {
            indexCurrent = -1;
            firstUse = true;
        }
    }
    #endregion

}