using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {

    // velocidade do player
    public float vel = 5f;

    public bool face = true;
    public Transform heroiT;

    public float force = 5;
    public float additionalForce = 5;
    public Rigidbody2D heroiRB;


    public Animator anim;
    public bool vivo = true;
    public int vidas = 5;

    public bool liberaPulo = false;

    public GameObject blood;

    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip bgMusic;
    AudioSource audioPlayer;

    bool holdingJumpKey;
    bool jumping;

    void Awake(){
        this.audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = bgMusic;
        audioPlayer.Play();
    }

	// Use this for initialization
	void Start () {

        heroiT = GetComponent<Transform>();
        heroiRB = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(vivo)
        {
            movePlayer();
        }
	}

    void FixedUpdate(){
        if(jumping){
            heroiRB.AddForce(new Vector2(0, additionalForce));
        }
    }


    // movimentar player
    void movePlayer()
    {
        float movement = Input.GetAxisRaw("Horizontal");
        heroiRB.velocity = new Vector2(movement * vel + Time.deltaTime, heroiRB.velocity.y);
        anim.SetInteger("velocity", (int) movement);
        if(movement != 0){
            transform.localScale = new Vector3(movement, 1, 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Agachado", true);
        } else {
            anim.SetBool("Agachado", false);
        }
        // pular
        if (Input.GetKeyDown(KeyCode.Space) && liberaPulo)
        {
            holdingJumpKey = true;
            jumping = true;
            audioPlayer.PlayOneShot(jumpSound);
            heroiRB.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            anim.SetBool("Pulo", true);
        } else if(Input.GetKeyUp(KeyCode.Space)){
            jumping = false;
            holdingJumpKey = false;
            anim.SetBool("Pulo", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D outro)
    {
        if( outro.gameObject.CompareTag("chao"))
        {
            liberaPulo = true;
            anim.SetBool("Pulo", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Agachado", false);
        }
        if( outro.gameObject.CompareTag("enemy") || outro.gameObject.CompareTag("killplane"))
        {
            morrer();
        }
        if(outro.gameObject.CompareTag("winplane")){
            LevelManager.NextLevel();
        }

    }

    private void OnCollisionExit2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("chao"))
        {
            liberaPulo = false;
        }
    }

    void morrer()
    {
        audioPlayer.PlayOneShot(dieSound);
        GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(blood, transform.position, Quaternion.identity);
        StartCoroutine("waitAndGameOver", 1);
    }

    private IEnumerator waitAndGameOver(int seconds){
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
        LevelManager.GameOver();        
    }
}
