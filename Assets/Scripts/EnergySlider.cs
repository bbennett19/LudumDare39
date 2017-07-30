using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour {
    public Slider slider;
    public PowerController powerController;
    public float rechargeTime = 0.5f;

    private bool _recharge = true;
    private float _rechargeRate;
	
	// Update is called once per frame
	void Update ()
    {
        if (_recharge)
        {
            slider.value = powerController.PowerPercentage();
        }
        else
        {
            slider.value = Mathf.Clamp(slider.value + _rechargeRate * Time.deltaTime, 0f, 1f);
            if (slider.value == 1f)
                _recharge = false;
        }
	}

    public void Recharge()
    {
        _recharge = true;
        _rechargeRate = (1f - slider.value) / rechargeTime;
    }
}
