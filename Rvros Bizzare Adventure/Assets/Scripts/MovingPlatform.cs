using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [Header ("Edge Points")]
    [SerializeField] private Transform pointa;
    [SerializeField] private Transform pointb;

    [Header("Plat")]
    [SerializeField] private Transform plat;
    [SerializeField] private Transform square;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private bool movingUp;
    private bool movingLeft;
    public string xory;

    [Header("Plat Idle")]
    [SerializeField] private float idling;
    private float idletime;

    void Update()
    {
        if(xory == "y"){
            if(movingUp){
                if(plat.position.y >= pointb.position.y)
                     MoveInDirection(-1);
                else
                    DirectionChange();
                }
            else{
                if(plat.position.y <= pointa.position.y)
                    MoveInDirection(1);
                else
                    DirectionChange();
                }
        }
        else if(xory == "x"){
            if(movingLeft){
               if(plat.position.x >= pointa.position.x)
                     MoveInDirection(-1);
                else
                    DirectionChange();
                }
            else{
                if(plat.position.x <= pointb.position.x)
                    MoveInDirection(1);
                else
                    DirectionChange();
        }
        }
    }

    private void MoveInDirection(int _direction){

        idletime = 0;

        //Plat Move
        if(xory == "y"){
            plat.position = new Vector3(plat.position.x ,
        plat.position.y  + Time.deltaTime * _direction * speed, plat.position.z);
        }
        else if(xory == "x"){
            plat.position = new Vector3(plat.position.x   + Time.deltaTime * _direction * speed,
        plat.position.y, plat.position.z);
        }

        if(xory == "y"){
            square.position = new Vector3(square.position.x ,
        square.position.y  + Time.deltaTime * _direction * speed, plat.position.z);
        }
        else if(xory == "x"){
            square.position = new Vector3(plat.position.x   + Time.deltaTime * _direction * speed,
        square.position.y, square.position.z);
        }
    }

    private void DirectionChange(){

        idletime += Time.deltaTime;

        if(xory == "y"){
        if(idletime > idling)
        movingUp = !movingUp;
        }

        else if(xory == "x"){
        if(idletime > idling)
        movingLeft = !movingLeft;
        }    
    }
}

