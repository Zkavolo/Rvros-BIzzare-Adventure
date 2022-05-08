using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int playerscore;

    void Start(){
        playerscore = 0;
    }

    void Update(){
        scoreText.text = playerscore.ToString();
    }

    public void addScore(int point){
        playerscore += point;
    }
}
