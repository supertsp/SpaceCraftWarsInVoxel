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

public class Stack<E>
{

    List<E> stack;

    //Auxs
    int indexCurrent = -1;

    #region Constructors
    public Stack()
    {
        stack = new List<E>();
    }

    public Stack(E firstElement) : this()
    {
        Add(firstElement);
    }

    public Stack(E firstElement, E secondElement) : this(firstElement)
    {
        Add(secondElement);
    }

    public Stack(E[] elements) : this()
    {
        Add(elements);
    }
    #endregion

    #region Basic Methods: Add, GetAt, GetCurrentIndex, TakeAndRemove, Remove, Length & ToString
    public void Add(E newElement)
    {
        stack.Add(newElement);
        UpdateIndexCurrent();
    }

    public void Add(E firstElement, E secondElement)
    {
        Add(firstElement);
        Add(secondElement);
    }

    public void Add(E[] elements)
    {
        foreach (var element in elements)
        {
            Add(element);
        }
    }

    public E GetAt(int index)
    {
        return stack[index];
    }

    public int GetCurrentIndex()
    {
        if (Length() > 0)
        {
            return indexCurrent;
        }

        return -1;
    }

    public E TakeAndRemove()
    {
        if (Length() > 0)
        {
            E current = stack[indexCurrent];
            Remove();
            return current;
        }

        return default(E);
    }

    public void Remove()
    {
        if (Length() > 0)
        {
            stack.RemoveAt(indexCurrent);
            UpdateIndexCurrent();
        }
    }

    public int Length()
    {
        return stack.Count;
    }

    public override string ToString()
    {
        string message =
            "Stak {\n" +
            "  + Elements: ";

        foreach (var item in stack)
        {
            message += item.ToString() + ", ";
        }

        message = message.Substring(0, (message.Length - 2));

        if (Length() > 0)
        {
            message += "\n  + Current Element: [" + GetCurrentIndex() + "] "
                    + stack[GetCurrentIndex()] + "\n}";
        }
        else
        {
            message += "\n}";
        }

        return message;
    }
    #endregion

    #region Auxiliary Method: UpdateIndexCurrent()
    void UpdateIndexCurrent()
    {
        indexCurrent = Length() - 1;
    }
    #endregion

}