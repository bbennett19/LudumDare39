using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    public enum State { OFF, POSITIVE, NEGATIVE }

    public GameObject PosPower;
    public GameObject NegPower;
    public GameObject NoPower;
    public ButtonController buttonController;

    private State _currentState;

	// Use this for initialization
	void Start ()
    {
        SetState(State.OFF);
	}

    private void SetState(State s)
    {
        _currentState = s;

        if (s == State.NEGATIVE)
        {
            NegPower.SetActive(true);
            PosPower.SetActive(false);
            NoPower.SetActive(false);
        }
        else if (s == State.POSITIVE)
        {
            NegPower.SetActive(false);
            PosPower.SetActive(true);
            NoPower.SetActive(false);
        }
        else if (s == State.OFF)
        {
            NegPower.SetActive(false);
            PosPower.SetActive(false);
            NoPower.SetActive(true);
        }
    }

    public void Activate(float positivePercentage, bool forcePositive)
    {
        if(Random.value < positivePercentage || forcePositive)
        {
            // Set Positive
            SetState(State.POSITIVE);
        }
        else
        {
            // Set Negative
            SetState(State.NEGATIVE);
        }
    }

    public void Deactivate()
    {
        SetState(State.OFF);
    }

    public State GetState()
    {
        return _currentState;
    }

    public void Click()
    {
        if (_currentState != State.OFF)
        {
            buttonController.ButtonClicked(this);
        }
            
    }
}
