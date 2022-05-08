using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int score_give;
    public Score scorecount;

    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            scorecount.GetComponent<Score>().addScore(score_give);
            Destroy(gameObject);
        }
    }
}
