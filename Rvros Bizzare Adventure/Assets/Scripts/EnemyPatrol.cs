using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Edge Points")]
    [SerializeField] private Transform leftedge;
    [SerializeField] private Transform rightedge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Enemy Idle")]
    [SerializeField] private float idling;
    private float idletime;

    [SerializeField] private Animator ani;

    void Awake(){
        initScale = enemy.localScale;
    }
    
    void Update()
    {
        if(movingLeft){
            if(enemy.position.x >= leftedge.position.x)
                 MoveInDirection(-1);
            else
                DirectionChange();
        }
        else{
            if(enemy.position.x <= rightedge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void OnDisable(){
        ani.SetBool("Move", false);
    }

    private void MoveInDirection(int _direction){

        idletime = 0;
        ani.SetBool("Move", true);

        //Enemy flip
        if(_direction > 0){
        enemy.localScale = new Vector3((-initScale.x), initScale.y, initScale.z);
        }
        else if(_direction < 0){
        enemy.localScale = new Vector3(initScale.x, initScale.y, initScale.z);    
        }

        //Enemy Move
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private void DirectionChange(){
        ani.SetBool("Move", false);

        idletime += Time.deltaTime;

        if(idletime > idling)
        movingLeft = !movingLeft;
    }

}
