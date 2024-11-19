using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : Singleton<MenuManager>
{
    public void changeScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
    }

}
