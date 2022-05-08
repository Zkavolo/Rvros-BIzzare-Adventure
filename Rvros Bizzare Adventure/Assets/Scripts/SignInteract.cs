using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInteract : MonoBehaviour
{
    public GameObject SignCanvas;
    public Animator ani;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            SignCanvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            ani.SetTrigger("PlayerExit");
        }
    } 

    void DisableSign(){
        SignCanvas.SetActive(false);
    }   
}
