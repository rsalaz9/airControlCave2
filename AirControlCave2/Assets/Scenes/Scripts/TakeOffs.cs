using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeOffs : MonoBehaviour
{
 
    public Text text;
    int score;
    void Start()
    {
      score = 0;
      text.text = "Take Offs: " + score;
    }

    public void AddTakeOffsPoint()
    {
        Debug.Log("take off added");
        score = score+1;
        text.text = "Take Offs: " + score;

    }

}
