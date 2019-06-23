using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour {
    public GameObject bullet;
    public int fireIntervalInSeconds = 2;
    List<GameObject> liveBullets;
    const int maxLiveBullets = 5;
    const float bulletVelocity = 10;
    bool readyToFire;

    void Start(){
        liveBullets = new List<GameObject>();
        readyToFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(readyToFire){
            GameObject lastBullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
            lastBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(-bulletVelocity, 0, 0);
            liveBullets.Add(lastBullet);
            readyToFire = false;
            StartCoroutine("waitAndSetFlag");

            if (liveBullets.Count > maxLiveBullets)
            {
                GameObject bulletToDestroy = liveBullets[0];
                liveBullets.Remove(bulletToDestroy);
                Destroy(bulletToDestroy);
            }
        }
    }

    private IEnumerator waitAndSetFlag(){
        yield return new WaitForSeconds(fireIntervalInSeconds);
        readyToFire = true;
    }
}
