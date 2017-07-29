using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {
    public PowerButton[] buttons;

    private int _maxActiveButtons = 3;
    private List<PowerButton> _activeButtons = new List<PowerButton>();
    private List<PowerButton> _inactiveButtons = new List<PowerButton>();
	// Use this for initialization
	void Start ()
    {
        _inactiveButtons.AddRange(buttons);
	}
	
	// Update is called once per frame
	void Update () {
		if(_activeButtons.Count < _maxActiveButtons)
        {
            for(int i = _activeButtons.Count; i < _maxActiveButtons; i++)
            {
                Activate();
            }
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

    private void Activate()
    {
        PowerButton b = _inactiveButtons[(int)Random.Range(0f, _inactiveButtons.Count - .001f)];
        _inactiveButtons.Remove(b);
        // If setting the last button and there are no active positive buttons force this one to be positive
        b.Activate(2f, !(_activeButtons.Count == _maxActiveButtons-1 && !OnePositiveActiveButton()));
        _activeButtons.Add(b);
    }

    public void Deactivate(PowerButton b)
    {
        if (!_inactiveButtons.Contains(b))
            _inactiveButtons.Add(b);
        if (_activeButtons.Contains(b))
            _activeButtons.Remove(b);
    }
}
