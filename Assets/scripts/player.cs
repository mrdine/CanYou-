using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    // velocidade do player
    float vel = 3f;

    public bool face = true;
    public Transform heroiT;

    public float force = 4;
    public Rigidbody2D heroiRB;


    public Animator anim;
    public bool vivo = true;

    public bool liberaPulo = false;
	// Use this for initialization
	void Start () {

        heroiT = GetComponent<Transform>();
        heroiRB = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        force = 4;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(vivo)
        {
            movePlayer();
        }
        

	}


    // movimentar player
    void movePlayer()
    {
        // direita
        if(Input.GetKey(KeyCode.D) && !face)
        {
            flip();
            transform.Translate(new Vector3(vel * Time.deltaTime, 0, 0));
        }
        // esquerda
        else if (Input.GetKey(KeyCode.A) && face)
        {
            flip();
            transform.Translate(new Vector3(-vel * Time.deltaTime, 0, 0));
        }

        // direita
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(vel * Time.deltaTime, 0, 0));
            anim.SetBool("Idle", false);
            anim.SetBool("Andar", true);
        }
        // esquerda
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-vel * Time.deltaTime, 0, 0));
            anim.SetBool("Idle", false);
            anim.SetBool("Andar", true);
        }

        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Andar", false);
        }
        // pular
        if (Input.GetKey(KeyCode.Space) && liberaPulo)
        {
            heroiRB.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            anim.SetBool("Pulo", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Andar", false);
        }
    }

    // girar player
    void flip()
    {
        face = !face;

        Vector3 scale = heroiT.localScale;
        scale.x *= -1;
        heroiT.localScale = scale;
    }



    private void OnCollisionEnter2D(Collision2D outro)
    {
        if( outro.gameObject.CompareTag("chao"))
        {
            liberaPulo = true;
            anim.SetBool("Pulo", false);
            anim.SetBool("Idle", true);
        }

    }

    private void OnCollisionExit2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("chao"))
        {
            liberaPulo = false;
            anim.SetBool("Andar", false);
        }
    }




}
