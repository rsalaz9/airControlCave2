using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSongs : MonoBehaviour
{
 public AudioSource gameMusic;
    public AudioClip [] tracks;
    int count; 
    // Start is called before the first frame update
    void Start()
    {
        count = 0;   
        print(tracks.Length);     
        
    }


    public void ChangeGameMusic(){
        if(count < tracks.Length-1)
            count ++;
        else 
            count =0;
        gameMusic.Stop();
        gameMusic.clip = tracks[count];
        gameMusic.Play();
    }





}