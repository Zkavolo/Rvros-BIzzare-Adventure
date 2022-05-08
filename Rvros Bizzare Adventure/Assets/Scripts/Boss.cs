using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
   [Header("Boss Object")]
   public GameObject playerObject;
   public Transform player;
   public Rigidbody2D Bossrb;
   public BoxCollider2D Bosscol;

   [Header("public variables")]
   public float speed = 2f;
   public float RangeforAttacking = 2.5f;
   public bool isFlipped = false;
   public Animator ani;

   [Header("Boss Atk")]
    public int atkdmg = 30;
    public Transform atkpoint;
    public float atkrange = 0.5f;
    public LayerMask PlayerLayer;
    public bool isAttacking = false;

    [Header("Boss Hp")]
    public int maxhp = 100;
    public int currenthp;
    public HPbar hpbar;

   void Update(){
    Invoke("ChasePlayer", 1.10f);
   }

   void Start()
    {
        currenthp = maxhp;
        hpbar.SetMaxhp(maxhp);    
    }


   public void ChasePlayer(){
        if(Vector2.Distance(player.position, Bossrb.position) <= RangeforAttacking){
            ani.SetTrigger("Boss_Attack");
        }
        else{
            if(isAttacking == false){
            LookAtPlayer();
            Vector2 target = new Vector2(player.position.x, Bossrb.position.y);
            Vector2 newPos = Vector2.MoveTowards(Bossrb.position, target, speed * Time.fixedDeltaTime);
            Bossrb.MovePosition(newPos);            
            ani.ResetTrigger("Boss_Attack");
        }
        }    
   }

   public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void BossAttack(){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(atkpoint.position, atkrange, PlayerLayer);

        foreach(Collider2D player in hitPlayer){
            player.GetComponent<PlayerCombat>().Takedmg(atkdmg);
        }
    }

    void OnDrawGizmosSelected(){

        if(atkpoint == null)
            return;

        Gizmos.DrawWireSphere(atkpoint.position, atkrange);
        }

    void AtkCooldown(){
        isAttacking = false;
    }

    void StopBoss(){
        isAttacking = true;
    }

    public void Takedmg(int dmg){
        currenthp -= dmg;

        hpbar.SetHp(currenthp);
        if(currenthp <= 0){
            Die();
        }

    }

    void Die(){
        //die animation
        ani.SetBool("Boss_die", true);
        FindObjectOfType<AudioManager>().Play("SlimeDie");

        //disable enemy function
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
