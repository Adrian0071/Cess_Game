using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlCenter : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
