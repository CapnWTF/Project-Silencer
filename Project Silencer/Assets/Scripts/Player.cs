using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameManager manager;

    public float moveSpeed;
    public float maxSpeed = 5f;
    private Vector3 input;
    public GameObject deathParticles;
    public bool usesManager = true;
    
    //player spawn point
    private Vector3 spawn;

    // audio 
    public AudioClip[] audioClip;

    // Use this for initialization
	void Start () {
        spawn = transform.position;
        if (usesManager)
        {
            manager = manager.GetComponent<GameManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
        {
            GetComponent<Rigidbody>().AddRelativeForce(input * moveSpeed);
        }
       
       //print(input);

        if(transform.position.y <= -2)
        {
            Die();
        }
        

	}
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.transform.tag == "Enemy")
        {
            Die();
        }

        if (other.transform.tag == "Token")
        {
            if (usesManager)
            { 
                manager.AddToken();
            }
            PlaySound(0);
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Goal")
        {

            PlaySound(1);
            Time.timeScale = 0f;
            manager.completeLevel();
        }
    }

    //plays a specific sound effect/audio clip
    void PlaySound(int clip)
    {

        AudioSource au = GetComponent<AudioSource>();
        au.clip = audioClip[clip];
        au.Play();

    }

    void Die()
    {
        print("I hit enemy");
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        transform.position = spawn;
    }
}
