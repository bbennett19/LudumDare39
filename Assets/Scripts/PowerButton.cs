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
    public float startDelay;
    public float scaleRate;
    public float scaleTime;

    private State _currentState;
    private bool _startUp = true;
    private bool _expand = false;
    private bool _shrink = false;

	// Use this for initialization
	void Start ()
    {
        SetState(State.OFF);
        StartCoroutine(Intro());
	}

    private void Update()
    {
        if(_startUp)
        {
            if (_expand)
                this.transform.localScale = this.transform.localScale + (Vector3.one * scaleRate * Time.deltaTime);
            if(_shrink)
                this.transform.localScale = this.transform.localScale - (Vector3.one * scaleRate * Time.deltaTime);
        }
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(startDelay);
        _expand = true;
        yield return new WaitForSeconds(scaleTime);
        _expand = false;
        _shrink = true;
        yield return new WaitForSeconds(scaleTime);
        _shrink = false;
        _startUp = false;
        this.transform.localScale = Vector3.one;
        buttonController.ButtonReady();
        yield return null;
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
