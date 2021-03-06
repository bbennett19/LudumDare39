﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {
    public PowerButton[] buttons;
    public const float DOWN_TIME = .5f;
    public PowerController powerController;
    public float minPositivePercentage;
    public float positivePercentageChange;
    public ScoreKeeper scoreKeeper;
    public AudioSource negativeClickSound;
    public AudioSource music;

    private float _elapsedTime = 0f;
    private int _maxActiveButtons = 9;
    private float _positivePercentage = .80f;
    private List<PowerButton> _activeButtons = new List<PowerButton>();
    private List<PowerButton> _inactiveButtons = new List<PowerButton>();
    private int _readyCount = 0;
    private bool _active = true;

	// Use this for initialization
	void Start ()
    {
        _inactiveButtons.AddRange(buttons);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(_readyCount == buttons.Length && _activeButtons.Count == 0 && _active)
        {
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime >= DOWN_TIME)
            {
                GenerateNewConfiguration();
                _elapsedTime = 0f;
            }
        }
	}

    private void GenerateNewConfiguration()
    {
        powerController.Activate();
        for(int i = 0; i < _maxActiveButtons; i++)
        {
            ActivateButton();
        }
    }

    private bool OnePositiveActiveButton()
    {
        bool positive = false;
        for(int i = 0; i < _activeButtons.Count; i++)
        {
            positive |= (_activeButtons[i].GetState() == PowerButton.State.POSITIVE);
        }
        return positive;
    }

    private void ActivateButton()
    {
        PowerButton b = _inactiveButtons[(int)Random.Range(0, _inactiveButtons.Count)];
        _inactiveButtons.Remove(b);
        // If setting the last button and there are no active positive buttons force this one to be positive
        b.Activate(_positivePercentage, _activeButtons.Count == _maxActiveButtons-1 && !OnePositiveActiveButton());
        _activeButtons.Add(b);
    }

    public void ButtonReady()
    {
        _readyCount++;

        if(_readyCount == buttons.Length)
        {
            // Start the music
            music.Play();
        }
    }

    public void ButtonClicked(PowerButton b)
    {
        if(b.GetState() == PowerButton.State.NEGATIVE)
        {
            // Handle negative
            DeactivateAllButtons(b);
            _active = false;
            powerController.Deactivate();
            negativeClickSound.Play();
            scoreKeeper.GameOver(negativeClickSound.clip.length);
        }
        else if(b.GetState() == PowerButton.State.POSITIVE)
        {
            // Handle positive
            DeactivateButton(b);

            if(_activeButtons.Count == 0 || !OnePositiveActiveButton())
            {
                // Get power
                DeactivateAllButtons();
                powerController.Recharge();
                IncreaseDifficulty();
            }
        }
    }

    private void IncreaseDifficulty()
    {
        _positivePercentage = Mathf.Clamp(_positivePercentage - positivePercentageChange, minPositivePercentage, 1.0f);
    }

    private void DeactivateButton(PowerButton b)
    {
        b.Deactivate();
        if (!_inactiveButtons.Contains(b))
            _inactiveButtons.Add(b);
        if (_activeButtons.Contains(b))
            _activeButtons.Remove(b);
    }

    private void DeactivateAllButtons(PowerButton b = null)
    {
        for(int i = 0; i < _activeButtons.Count; i++)
        {
            if (_activeButtons[i] != b)
            {
                _inactiveButtons.Add(_activeButtons[i]);
                _activeButtons[i].Deactivate();
            }
        }
        _activeButtons.Clear();
    }
}
