using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerTransform;

    void OnCollisionEnter2D(Collision2D other){
        if(other.collider.tag == "Player"){
            playerTransform = new Vector3 (5,5,1);
            player.transform.parent = transform;
            player.transform.localScale = playerTransform;
        }   
    }

    void OnCollisionExit2D(Collision2D other){
        if(other.collider.tag == "Player"){
            player.transform.parent = null;
        }   
    }
}    
