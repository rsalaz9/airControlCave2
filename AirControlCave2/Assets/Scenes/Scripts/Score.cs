using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Text scoreText;
    int score;
    AudioSource audioSource;
    [SerializeField] AudioClip audioWin;
    [SerializeField] AudioClip audioLose;
    bool playedSound;
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        audioSource = GetComponent<AudioSource>();
        playedSound = true;
    }

    public void AddTakeOffScore()
    {
        Debug.Log("point added");
        score = score+10;
        scoreText.text = "Score: " + score;
        playedSound = true;
        Invoke("playWinSound",0f);
    }


    public void DecreaseScore()
    {
        Debug.Log("point added");
        score = score-5;
        scoreText.text = "Score: " + score;
        playedSound = true;
        Invoke("playLoseSoundTakeOff",0f);

    }

    public void AddLandingScore()
    {
        Debug.Log("point added");
       
        playedSound = true;
        Invoke("playWinSoundLanding",15f);

    }



    public void playLoseSoundTakeOff(){
        if (!audioSource.isPlaying && playedSound ==  true ){
            audioSource.PlayOneShot(audioWin);
            playedSound = false;
        }
        
    }

    public void playWinSoundLanding(){
        score = score+5;
        scoreText.text = "Score: " + score;

        if (!audioSource.isPlaying && playedSound ==  true ){
            audioSource.PlayOneShot(audioWin);
            playedSound = false;
        }
        
    }

        public void playLoseSound(){
        if (!audioSource.isPlaying && playedSound ==  true ){
            audioSource.PlayOneShot(audioLose);
            playedSound = false;
        }
        
    }

    
}