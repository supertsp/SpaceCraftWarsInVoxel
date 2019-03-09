using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <license>
/// Copyright (C) 2016-05-08 Tiago Penha Pedroso
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

public class EnumConverter<E>
{
    
    public static E FromStringToMember(string memberName)
    {
        if (Enum.IsDefined(typeof(E), memberName))
        {
            return (E)Enum.Parse(typeof(E), memberName);
        }

        return default(E);
    }

    public static int FromStringToInt(string memberName)
    {
        if (Enum.IsDefined(typeof(E), memberName))
        {
            var enumMembers = Enum.GetValues(typeof(E));
            
            foreach (var member in enumMembers)
            {
                if (member.ToString() == memberName)
                {
                    return (int)member;
                }
            }
        }

        return -1;
    }

}