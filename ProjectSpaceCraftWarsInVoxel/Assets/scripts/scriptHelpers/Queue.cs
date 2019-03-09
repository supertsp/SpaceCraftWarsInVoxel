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

public class Queue<E>
{

    List<E> queue;

    //Auxs
    int indexCurrent = 0;

    #region Constructors
    public Queue()
    {
        queue = new List<E>();
    }

    public Queue(E firstElement) : this()
    {
        Add(firstElement);
    }

    public Queue(E firstElement, E secondElement) : this(firstElement)
    {
        Add(secondElement);
    }

    public Queue(E[] elements) : this()
    {
        Add(elements);
    }
    #endregion

    #region Add, GetAt, GetCurrentIndex, TakeAndRemove, Remove, Length & ToString
    public void Add(E newElement)
    {
        queue.Add(newElement);
    }

    public void Add(E firstElement, E secondElement)
    {
        Add(firstElement);
        Add(secondElement);
    }


    public void Add(E[] elements)
    {
        foreach (E item in elements)
        {
            Add(item);
        }        
    }

    public E GetAt(int index)
    {
        return queue[index];
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
            E current = queue[indexCurrent];
            Remove();
            return current;
        }
        
        return default(E);
    }

    public void Remove()
    {
        if (Length() > 0)
        {
            queue.RemoveAt(indexCurrent);
        }
    }

    public int Length()
    {
        return queue.Count;
    }

    public override string ToString()
    {
        string message =
            "Queue {\n" +
            "  + Elements: ";

        foreach (var item in queue)
        {
            message += item.ToString() + ", ";
        }

        message = message.Substring(0, (message.Length - 2));

        if (Length() > 0)
        {
            message += "\n  + Current Element: [" + GetCurrentIndex() + "] "
                    + queue[GetCurrentIndex()] + "\n}";
        }
        else
        {
            message += "\n}";
        }
        
        return message;
    }
    #endregion

}