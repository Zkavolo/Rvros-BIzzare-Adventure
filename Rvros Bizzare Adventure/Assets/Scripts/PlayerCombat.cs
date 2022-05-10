using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //hp attribut
    [Header ("HP")]
    public int maxhp = 100;
    public int currenthp;
    public HPbar hpbar;
    

    // atk attribut
    [Header("Attack")]
    public Transform AtkHitBox;
    public int Atkdamage = 25;
    public float Atkrange = 0.97f;
    public bool cooldown = false;
    public bool attacking;
    public int combo;

    //get components attribut
    [Header("Animation and Misc")]
    public Animator ani;
    public LayerMask enemyLayers;
    public LayerMask bossLayers;
    public GameObject deadUI;
    public GameObject wonUI;
    private SpriteRenderer spriteRend;
    [SerializeField] private Rigidbody2D player;
    private bool iframemoment = false;
    public PlayerMoment groundcheck;

    void Awake(){
        spriteRend = GetComponent<SpriteRenderer>();
        groundcheck = GetComponent<PlayerMoment>();
    }

    private void Start(){
        currenthp = maxhp;
        hpbar.SetMaxhp(maxhp);
    }

    void Update()
    {
    if (Input.GetMouseButtonDown(0) && attacking == false ){
        //Attack Animation
        if(player.velocity.y == 0){
        Combos_();
        } 

        else if(player.velocity.y != 0 && groundcheck.Grounded == false){
        ani.SetTrigger("AttackinAir");
        player.gravityScale = 0;
        
        Invoke("EnableGravity", 0.30f);

        //Attack cooldown
        Invoke("ResetCooldown",0.4f);
        cooldown = true;
        } 
        }
    }

    void AtkSounds(){
       //Attack Sound
        FindObjectOfType<AudioManager>().Play("PlayerAttack"); 
    }

    void Combos_(){
        attacking = true;
        ani.SetTrigger(""+combo);

        //Attack cooldown
        Invoke("ResetCooldown",0.3f);
        cooldown = true;
    }

    void Start_Combo(){
        attacking = false;
        if (combo<3){
            combo++;
        }
    }

    public void LastAtk(){
        attacking = false;
        combo = 0;
    }

    void PlayerAttack(){
        //Attack Detection
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AtkHitBox.position, Atkrange, enemyLayers);
        Collider2D[] hitBoss = Physics2D.OverlapCircleAll(AtkHitBox.position, Atkrange, bossLayers);

        //Attack Damage
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<EnemySlime>().Takedmg(Atkdamage);
        }

        foreach(Collider2D boss in hitBoss){
            boss.GetComponent<Boss>().Takedmg(Atkdamage);
        }
    }

    void OnDrawGizmosSelected(){

        if(AtkHitBox == null)
        return;

        Gizmos.DrawWireSphere(AtkHitBox.position,Atkrange);
    }

    void ResetCooldown(){
        cooldown = false;
    }

    public void Takedmg(int dmg){
        if(iframemoment == false){
        currenthp -= dmg;
        //hpbar interaction

        hpbar.SetHp(currenthp);
        //hurt animation
        ani.SetTrigger("Gettinghit");
        FindObjectOfType<AudioManager>().Play("Playerhurt");

        LastAtk();

        StartCoroutine(Invulnerability());
        }
        if(currenthp <= 0){
        Die();
        }
    }

    public void Die(){
        //die animation
        ani.SetBool("Die", true);

        //disable player function
        DisablePlayer();

         //Activates Player Dead screen
        deadUI.SetActive(true);
    }

    public void DisablePlayer(){
        this.enabled = false;
        GetComponent<PlayerMoment>().enabled = false;
    }

    public void Won(){
         //Activates Win screen
        this.enabled = false;
        FindObjectOfType<AudioManager>().Stop("PlayerRun");
        GetComponent<PlayerMoment>().enabled = false;
        wonUI.SetActive(true);
    }

    public void OnCollisionEnter2D(Collision2D other){
        if(other.collider.tag == "SpikeTrap"){
            Takedmg(20);
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Cave"){
            Won();
        }   
    }

    private IEnumerator Invulnerability(){

            Physics2D.IgnoreLayerCollision(6, 9, true);
            iframemoment = true;
            
                spriteRend.color = new Color(1, 1, 1, 0.5f);
                yield return new WaitForSeconds(1);

                spriteRend.color = Color.white;
                yield return new WaitForSeconds(1);
              
            Physics2D.IgnoreLayerCollision(6, 9, false);
            iframemoment = false;
    }
    void EnableGravity(){
        player.gravityScale = 2;
    }

    public void Healing(int heal){
        currenthp += heal;
        if(currenthp > 100){
            currenthp = 100;
        }
        hpbar.SetHp(currenthp);
    }
}
