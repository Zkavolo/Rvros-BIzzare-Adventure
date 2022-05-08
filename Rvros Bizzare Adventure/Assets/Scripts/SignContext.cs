using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignContext : MonoBehaviour
{
    public Text context;
    public string fill;

    void Start()
    {
        context.text = fill;
    }

}
