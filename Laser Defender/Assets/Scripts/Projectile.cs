using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	void Start ()
	{
	
	}
	
	void Update()
	{
		
	}
	
	public float damage = 100f;
	
	public float GetDamage()
	{
		return damage;
	}
	
	public void Hit()
	{
		Destroy(gameObject);
	}
}
