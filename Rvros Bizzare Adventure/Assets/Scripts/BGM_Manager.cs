using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    public string MenuSong;

    void Start(){
        FindObjectOfType<AudioManager>().Play(MenuSong);
    }
}
