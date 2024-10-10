using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerScript : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
