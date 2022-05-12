using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResoluSett : MonoBehaviour
{
    List<int> widths  = new List<int>(){1280, 960, 1366, 1920};
    List<int> heights = new List<int>(){800, 540, 768, 1080};


    void Awake () {

    	SetRsl(0);
	}

    public void SetRsl(int index){
    	
    	int width  = widths[index];
    	int height = heights[index];
    	Screen.SetResolution(width, height, true);

    }
}
