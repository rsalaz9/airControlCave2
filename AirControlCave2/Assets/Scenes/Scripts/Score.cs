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

    public void AddTakeOffScore()
    {
        Debug.Log("point added");
        score = score+10;
        scoreText.text = "Score: " + score;

    }


    public void DecreaseScore()
    {
        Debug.Log("point added");
        score = score-5;
        scoreText.text = "Score: " + score;

    }

    public void AddLandingScore()
    {
        Debug.Log("point added");
        score = score+5;
        scoreText.text = "Score: " + score;

    }

}