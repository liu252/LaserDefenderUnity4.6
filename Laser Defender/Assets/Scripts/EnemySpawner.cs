using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float padding = 4f;
	public float spawnDelay = 0.1f;
	
	float xmin = -5;
	float xmax = 5;
	bool atEnd;
	
	// Use this for initialization
	void Start () 
	{
		atEnd = false;
		SpawnUntilFull();
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	void SpawnEnemies()
	{
		foreach (Transform child in transform)
		{
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position , Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		if(freePosition)
		{
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position , Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition())
			Invoke ("SpawnUntilFull", spawnDelay);
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height,0));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!atEnd)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (atEnd)
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		if(transform.position.x >= xmax)
		{ 
			atEnd = true;
		}
		else if(transform.position.x <= xmin)
		{
			atEnd = false;
		}
		if (AllMembersDead())
		{
			SpawnUntilFull();
		}
		
		
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		
		transform.position = new Vector3(newX, transform.position.y,0);
		
	}
	
	Transform NextFreePosition()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount > 0)
			{
				return false;
			}
		}
		return true;
	}
}
