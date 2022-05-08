using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other){
        if(other.collider.tag == "Player"){
            GameObject.Find("Boss").GetComponent<Boss>().enabled = true;
        }
        Destroy(gameObject);
    }
}
