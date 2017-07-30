using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    public float timeToFinish;
    public float minTimtToFinish;
    public WireManager wireManager;
    public ScoreKeeper scoreKeeper;

    private float _elapsedTime = 0f;
    private bool _active = false;
	
	// Update is called once per frame
	void Update ()
    {
        if (_active)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= timeToFinish)
            {
                // Lose
                Debug.Log("YOU LOSE");
            }
        }
	}

    public void Activate()
    {
        _active = true;
    }

    public float PowerPercentage()
    {
        return 1f - (_elapsedTime / timeToFinish);
    }

    public void Recharge()
    {
        _active = false;
        _elapsedTime = 0f;
        timeToFinish -= .05f;
        wireManager.PlayAnimation();
        scoreKeeper.IncreaseScore();
    }
}
