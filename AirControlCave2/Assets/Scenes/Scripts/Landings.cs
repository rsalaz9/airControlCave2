using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Landings : MonoBehaviour
{
    public Text scoreText;
    int score;
    void Start()
    {
    score = 0;
      scoreText.text = "Landings: " + score;
    }

    public void AddLandingsPoint()
    {
        Debug.Log("take off added");
        score = score+1;
        scoreText.text = "Landings: " + score;

    }
}
