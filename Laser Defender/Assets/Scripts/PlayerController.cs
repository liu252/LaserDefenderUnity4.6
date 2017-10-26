using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed = 15f;
	public float projectileSpeed = 7f;
	public float fireRate = .2f;
	public float padding = .5f;
	public GameObject projectile;
	public float health = 100;
	
	private LevelManager levelManager;
	
	float xmin = -5;
	float xmax = 5;
	
	// Use this for initialization
	void Start () 
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.000001f, fireRate);
		}
		if(Input.GetKeyUp (KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
		
		if(Input.GetKey (KeyCode.RightArrow))
		{
			//this.transform.position += new Vector3 (speed * Time.deltaTime, 0 ,0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.LeftArrow))
		{
			//this.transform.position += new Vector3 (-speed * Time.deltaTime, 0 ,0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		
		transform.position = new Vector3(newX, transform.position.y,0);
	}
	
	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0,+1,0);
		GameObject laser = Instantiate(projectile,startPosition, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		GetComponent<AudioSource>().Play ();
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if (laser)
		{
			health -= laser.GetDamage();
			laser.Hit ();
			if (health <= 0)
			{
				Destroy(gameObject);
				levelManager.LoadLevel("Lose");
			}
			
		}
	}
}
