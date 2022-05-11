using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    [Header("Componenets")]
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Animator ani;
    [SerializeField] private GameObject currentPlatform;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Vector3 playerTransform;
    public PlayerCombat checkatk;

    [Header("Player Movement")]
    public float velocity_moving;
    public float velocity_jumping;
    public bool Grounded;
    public bool Falling;
    private bool Airatk = true;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        checkatk = GetComponent<PlayerCombat>();
    }

    void Start(){
        FindObjectOfType<AudioManager>().Play("PlayerRun");
    }

    void Update(){
        playerMoving();
        playerJumping();
        playerFalling();
        PlayerCrouching();
        if(Input.GetMouseButtonDown(0) && !Grounded){
            Airatk = false;
            Invoke("Timer", 0.30f);
        }
    } 

    void playerMoving(){
       float horizontalmove= Input.GetAxis("Horizontal");
       player.velocity = new Vector2(horizontalmove * velocity_moving ,player.velocity.y);

       //flipping the player position
       if(horizontalmove > 0.01f){
        transform.localScale = new Vector3(playerTransform.x ,playerTransform.y ,playerTransform.z);
       }
       else if(horizontalmove < - 0.01f){
        transform.localScale = new Vector3((-playerTransform.x), playerTransform.y ,playerTransform.z);
       }

       //running animation
       ani.SetBool("Run", horizontalmove != 0);
       ani.SetBool("Grounded", Grounded);

       //running sounds
       if(horizontalmove != 0 && Grounded){
            FindObjectOfType<AudioManager>().UnPause("PlayerRun");
       }
       else{
            FindObjectOfType<AudioManager>().Pause("PlayerRun");
       }

       player.freezeRotation = true;
    }

    void playerJumping(){
        //condition for player only jumping once
       if(Input.GetKeyDown(KeyCode.Space) && Grounded){
        ani.SetTrigger("Jump");
        FindObjectOfType<AudioManager>().Play("PlayerJump");
        player.velocity = new Vector2(player.velocity.x, velocity_jumping);
        Grounded = false;
        checkatk.LastAtk();
       }   
    }

    void playerFalling(){

        if (Airatk == true){
        if(player.velocity.y < -2f){
            Falling = true;
        }
        else{
            Falling = false;
        }
        ani.SetBool("falling", Falling);
        }
    }

    void PlayerCrouching(){
        if((Input.GetKeyDown(KeyCode.S) && Grounded) || (Input.GetKeyDown(KeyCode.DownArrow) && Grounded)){
            ani.SetTrigger("crouching");
            if(currentPlatform != null){
                StartCoroutine(DisableCollision());
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D other){
        // collision with tile or platform
        if(other.collider.tag == "Default tile" || other.collider.tag == "Platform" && player.velocity.y == 0){
            Grounded = true;   
        }

        if(other.gameObject.CompareTag("Platform")){
            currentPlatform = other.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D other){
        Grounded = false;

        if(other.gameObject.CompareTag("Platform")){
            currentPlatform = null;
        }
    }

    private IEnumerator DisableCollision(){
        BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    void Timer(){
        Airatk = true;
    }
}
