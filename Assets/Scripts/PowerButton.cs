using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour {
    public enum State { OFF, POSITIVE, NEGATIVE }

    public GameObject PosPower;
    public GameObject NegPower;
    public GameObject NoPower;
    public ButtonController controller;
    public float downTime = 0.5f;

    private State _currentState;
    private float _elapsedTime = 0f;
    private float _timeToChange = 99999999f;

	// Use this for initialization
	void Start () {
        SetState(State.OFF);
	}
	
	// Update is called once per frame
	void Update () {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _timeToChange)
        {
            SetState(State.OFF);
        }
        if(_elapsedTime >= _timeToChange+downTime)
        {
            // Let the controller know this button is ready to be active again
            controller.Deactivate(this);
        }
	}

    public State GetState()
    {
        return _currentState;
    }

    public void Activate(float time, bool canBeNegative)
    {
        _timeToChange = time;
        _elapsedTime = 0f;
        if(Random.value < 0.99f && canBeNegative)
        {
            // Set Negative
            SetState(State.NEGATIVE);
        }
        else
        {
            // Set Positive
            SetState(State.POSITIVE);
        }
    }

    private void SetState(State s)
    {
        _currentState = s;
        if(s == State.NEGATIVE)
        {
            NegPower.SetActive(true);
            PosPower.SetActive(false);
            NoPower.SetActive(false);
        }
        else if(s == State.POSITIVE)
        {
            NegPower.SetActive(false);
            PosPower.SetActive(true);
            NoPower.SetActive(false);
        }
        else if(s == State.OFF)
        {
            NegPower.SetActive(false);
            PosPower.SetActive(false);
            NoPower.SetActive(true);
        }
    }

    public void Click()
    {
        Debug.Log("Click");
        if (_currentState != State.OFF)
        {
            // Update Power
            _elapsedTime = _timeToChange;
            SetState(State.OFF);
        }
    }
}
