using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonStart(){
        SceneManager.LoadScene(1);
    }

    public void ButtonOptions(){
        SceneManager.LoadScene(2);
    }

    public void ButtonExit(){
        Application.Quit();
    }

    public void ButtonMenu(){
        SceneManager.LoadScene(0);
    }
}
