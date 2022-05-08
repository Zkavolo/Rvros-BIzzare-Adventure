using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
   public Text finalText;
   public GameObject score;

    void Update()
    {
        finalText.text = score.GetComponent<UnityEngine.UI.Text>().text.ToString();
    }
}
