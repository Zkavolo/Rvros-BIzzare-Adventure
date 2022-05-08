using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    [Header("Components")]
    public Animator ani;
    public Rigidbody2D Slime;
    public EnemyPatrol enemyPatrol;
    public BoxCollider2D boxcol;
    public LayerMask playerlayer;
    private PlayerCombat playerhp;
    public int scorekilled = 25;
    public Score scorecount;

    [Header("Enemy Components")]
    private float addGrav = 0f;
    public int maxhp = 50;
    int currenthp;

    [Header("Enemy Attack")]
    public int atkdmg = 10;
    public float atkcooldowm;
    public float range;
    private float cooldownTimer = Mathf.Infinity;


    void Awake(){
        ani = GetComponent<Animator>();
        Slime = GetComponent<Rigidbody2D>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    void Start()
    {
        currenthp = maxhp;    
    }

    void Update(){
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight()){
            if(cooldownTimer >= atkcooldowm){
                cooldownTimer = 0;
                ani.SetTrigger("Attack");
             }
        }

        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    public void Takedmg(int dmg){
        currenthp -= dmg;

        //hurt animation
        ani.SetTrigger("Hurt");

        //hurt sounds
        FindObjectOfType<AudioManager>().Play("SlimeHurt");

        if(currenthp <= 0){

            Die();
        }

    }

    void Die(){
        //die animation
        ani.SetBool("Dead", true);

        //hurt sounds
        FindObjectOfType<AudioManager>().Play("SlimeDie");

        //addScore
        scorecount.GetComponent<Score>().addScore(scorekilled);

        //disable enemy function
        GetComponent<Collider2D>().enabled = false;
        enemyPatrol.enabled = false;
        Slime.gravityScale -= addGrav;
        this.enabled = false;

    }    

    private bool PlayerInSight(){
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxcol.bounds.center + transform.right * range * transform.localScale.x,
            boxcol.bounds.size, 0, Vector2.left, 0, playerlayer);

            if(hit.collider != null)
                playerhp = hit.transform.GetComponent<PlayerCombat>();

        return hit.collider != null;
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(boxcol.bounds.center + transform.right * range  * transform.localScale.x, boxcol.bounds.size);
    }

    void AtkPlayer(){
        if(PlayerInSight())
            playerhp.Takedmg(atkdmg);
    }
}
