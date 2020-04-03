using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Text scoreText;
    int score;
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
       
    }

    public void AddPoint()
    {
        Debug.Log("point added");
        score++;
        scoreText.text = "Score: " + score;

    }

}