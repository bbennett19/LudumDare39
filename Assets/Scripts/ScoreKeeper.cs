using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    public Text scoreText;
    private int _score = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreaseScore()
    {
        _score++;
        scoreText.text = _score.ToString();
    }
}
