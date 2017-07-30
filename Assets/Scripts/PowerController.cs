using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    public float minTimeToFinish;
    public float maxTimeToFinish;
    public WireManager wireManager;
    public ScoreKeeper scoreKeeper;
    public AnimationCurve difficultyCurve;
    public int targetHighDifficulty = 1;

    private float _timeToFinish;
    private float _elapsedTime = 0f;
    private int _count = 0;
    private bool _active = false;

    private void Start()
    {
        _timeToFinish = maxTimeToFinish;
    }

    // Update is called once per frame
    void Update ()
    {
        if (_active)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _timeToFinish)
            {
                // Lose
                scoreKeeper.GameOver();
            }
        }
	}

    public void Activate()
    {
        _active = true;
    }

    public void Deactivate()
    {
        _active = false;
    }

    public float PowerPercentage()
    {
        return 1f - (_elapsedTime / _timeToFinish);
    }

    public void Recharge()
    {
        _count++;
        _active = false;
        _elapsedTime = 0f;
        _timeToFinish = minTimeToFinish + (difficultyCurve.Evaluate((float)_count/(float)targetHighDifficulty) * (maxTimeToFinish-minTimeToFinish));
        wireManager.PlayAnimation();
        scoreKeeper.IncreaseScore();
    }
}
