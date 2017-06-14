using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public Text text; 
	
	private static int score = 0;
	// Use this for initialization
	void Start () {
		if (score <= 9999)
			text.text = "Score: " + score;
		else
			text.text = "Score: 9999";
		Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Score(int points)
	{
		score += points;
		if (score <= 9999)
			text.text = "Score: " + score;
		else
			text.text = "Score: 9999";
	}
	
	public void Reset()
	{
		score = 0;
	}
}
