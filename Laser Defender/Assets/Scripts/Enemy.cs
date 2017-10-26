using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	private ScoreKeeper scorekeeper;
	
	public GameObject projectile;
	public int enemyScoreValue = 100;
	public float projectileSpeed = 7f;
	public float shotsPerSeconds = 0.5f;
	public float health = 200f;
	
	public AudioClip fireAudio;
	public AudioClip destroyedAudio;
	
	// Use this for initialization
	void Start () 
	{
		scorekeeper = GameObject.FindObjectOfType<ScoreKeeper>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value <= probability)
		{
			Fire ();
		}
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
				Destroyed ();
			}
			
		}
	}
	
	void Destroyed()
	{
		AudioSource.PlayClipAtPoint(destroyedAudio, transform.position);
		Destroy(gameObject);
		scorekeeper.Score(enemyScoreValue);
	}
	
	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0,-1,0);
		GameObject laser = Instantiate(projectile,startPosition, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireAudio, transform.position);
	}
}
