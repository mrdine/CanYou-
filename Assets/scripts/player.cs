using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {

    // velocidade do player
    float vel = 5f;

    public bool face = true;
    public Transform heroiT;

    public float force = 4;
    public Rigidbody2D heroiRB;


    public Animator anim;
    public bool vivo = true;
    public int vidas = 5;

    public bool liberaPulo = false;

    public GameObject blood;

    public GameObject tentar;

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
            anim.SetBool("Agachado", false);
        }
        // esquerda
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-vel * Time.deltaTime, 0, 0));
            anim.SetBool("Idle", false);
            anim.SetBool("Andar", true);
            anim.SetBool("Agachado", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Agachado", true);
        }

        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Andar", false);
            anim.SetBool("Agachado", false);
        }
        // pular
        if (Input.GetKey(KeyCode.Space) && liberaPulo)
        {
            heroiRB.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            anim.SetBool("Pulo", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Andar", false);
            anim.SetBool("Agachado", false);
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
            anim.SetBool("Agachado", false);
        }
        if( outro.gameObject.CompareTag("enemy") || outro.gameObject.CompareTag("killpane"))
        {
            morrer();
        }

    }

    private void OnCollisionExit2D(Collision2D outro)
    {
        if (outro.gameObject.CompareTag("chao"))
        {
            liberaPulo = false;
            anim.SetBool("Andar", false);
            anim.SetBool("Agachado", false);
        }
    }


    void morrer()
    {
        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(gameObject);

        Instantiate(tentar, transform.position, Quaternion.identity);
        

    }

    

    IEnumerator LoadYourAsyncScene(string cena)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(cena);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    
}
