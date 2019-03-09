using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// Attention! The method SendDataByUnityCoroutine() must be called within StartCoroutine().
/// 
/// <!--
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
/// along with this program.  If not, see http://www.gnu.org/licenses/.
/// -->
/// </summary>
public class WebService
{
    string serviceURL;
    public string DataRequest { get; set; }
    string response;
    WWW postMethod;

    #region Constructor
    public WebService(string serviceURL)
    {
        this.serviceURL = serviceURL;
    }
    #endregion

    public bool IsArrivedResponse()
    {
        if (postMethod == null)
        {
            return false;
        }

        return postMethod.isDone;
    }
    
    public string GetResponse()
    {
        return response;
    }
    
    public IEnumerator SendDataByCoroutine()
    {
        string postURL = serviceURL + WWW.EscapeURL(DataRequest);

        //postMethod.uploadProgress
        postMethod = new WWW(postURL);

        // Wait until the download is done
        yield return postMethod;

        if (postMethod.error != null)
        {
            response = postMethod.error;
        }
        else
        {
            response = postMethod.text;
        }
    }

}