using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpeningSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnterMainMenu", 15f);
    }

    public void EnterMainMenu(){
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
