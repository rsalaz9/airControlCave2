using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    public Text scoreText;
    int score;
    AudioSource audioSource;
    [SerializeField] AudioClip audioWin;
    [SerializeField] AudioClip audioLose;
    bool playedSound;
    [SerializeField] AudioClip audioWinGame;
    [SerializeField] ParticleSystem particleSystem1;
    [SerializeField] ParticleSystem particleSystem2;
    GameObject winText;
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        audioSource = GetComponent<AudioSource>();
        playedSound = true;
        // particleSystem1.enableEmission = false;
        // particleSystem2.enableEmission = false;
        winText = GameObject.Find("WinText");
        winText.active = false;
    }

    void Update(){
        if(score == 50){
            Invoke("PlayWinScenario", 5f);
        }
    }
    public void AddTakeOffScore()
    {
  
        score = score+10;
        scoreText.text = "Score: " + score;
        playedSound = true;
        Invoke("playWinSound",0.1f);
    }

    public void PlayWinScenario(){
        playedSound = true;
        particleSystem1.Play();
        particleSystem2.Play();
        winText.active = true;
        Invoke("EndGame", 6f);
        if (!audioSource.isPlaying && playedSound ==  true ){
            audioSource.PlayOneShot(audioWinGame);
            playedSound = false;
        }

    }

    public void EndGame(){
        SceneManager.LoadScene(1);
    }

    public void DecreaseScore()
    {
        Debug.Log("point added");
        score = score-5;
        scoreText.text = "Score: " + score;
        playedSound = true;
        Invoke("playLoseSoundTakeOff",1f);

    }

    public void AddLandingScore()
    {
       
        playedSound = true;
        Invoke("playWinSoundLanding",15f);

    }



    public void playWinSound(){
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