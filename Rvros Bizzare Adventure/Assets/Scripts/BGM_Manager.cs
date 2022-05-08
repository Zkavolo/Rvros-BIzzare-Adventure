using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    void Start(){
        FindObjectOfType<AudioManager>().Play("MenuTheme");
    }
}
