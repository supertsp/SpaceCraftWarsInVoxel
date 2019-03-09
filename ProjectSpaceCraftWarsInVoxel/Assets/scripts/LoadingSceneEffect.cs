using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


/// <license>
/// Copyright(C) Tiago Penha Pedroso
/// 2017.05.30
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

public class LoadingSceneEffect : MonoBehaviour
{

    #region Public Attributes of the Component [Allowed only classes that inherit MonoBehaviour, primitive types or Vectors]       
    [Range(0.1f, 2.0f)]
    [Tooltip("Wait time to load next scene")]
    public float waitTimeToLoad = 1f;
    public TextPercent textPercent;
    public bool isMovementEnable;
    public float xPositionStart;
    public float xPositionTarget;
    [Range(0.1f, 5.0f)]
    public float speedMoviment = 1f;
    #endregion

    #region Auxiliary Tools for the Class [Aren't visible in Editor]

    #region Publics Properties 
    public string SceneNameToLoading { get; set; }
    #endregion

    #region Private Attributes or Properties
    private bool loadingActive;
    private float currentProgress;
    #endregion

    #region Enumerations

    #endregion

    #endregion

    #region Messages Methods of MonoBehaviour    
    private void Update()
    {
        if (isMovementEnable)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(
                    xPositionTarget,
                    transform.position.y,
                    transform.position.z),
                (Time.time / speedMoviment) * Time.deltaTime
            );
        }
    }

    private void OnDisable()
    {
        transform.position = new Vector3(
            xPositionStart,
            transform.position.y,
            transform.position.z
        );
    }
    #endregion

    #region Implemented interfaces methods

    #endregion

    #region Public Methods
    public void StartLoading()
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        StartCoroutine(EnableLoading());
    }
    #endregion

    #region Private Methods [Auxiliary Methods]
    private IEnumerator EnableLoading()
    {
        loadingActive = true;

        Application.backgroundLoadingPriority = ThreadPriority.High;
        StartCoroutine(AsyncUpdate());
        yield return new WaitForSeconds(waitTimeToLoad);

        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneNameToLoading);

        while (!loading.isDone)
        {
            currentProgress = (int)(loading.progress * 100) + 11;
            textPercent.SetText(currentProgress + "%");
            yield return null;
        }
    }

    private IEnumerator AsyncUpdate()
    {
        while (loadingActive)
        {
            textPercent.SetText(currentProgress + "%");
            yield return null;
        }
    }
    #endregion

}

#region Structs for Inspector
[System.Serializable]
public struct TextPercent
{
    public GameObject back;
    public GameObject front;

    public void SetText(string newText)
    {
        back.GetComponent<Text>().text = newText;
        front.GetComponent<Text>().text = newText;
    }

    public string GetText()
    {
        return back.GetComponent<Text>().text;
    }
}
#endregion