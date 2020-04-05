using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_trial : MonoBehaviour
{
    public Button pushButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = pushButton.GetComponent<Button>();
		    btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick(){
		Debug.Log ("You have clicked the button!");
	}
    
}
