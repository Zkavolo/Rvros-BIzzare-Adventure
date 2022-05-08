using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private int Heal = 15;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            other.GetComponent<PlayerCombat>().Healing(Heal);
            Destroy(gameObject);
        }
    }
}
