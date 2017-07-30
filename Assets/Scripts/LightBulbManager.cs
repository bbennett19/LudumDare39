using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbManager : MonoBehaviour
{
    public PowerController powerController;
    public int maxAlpha;
    private SpriteRenderer _renderer;

    private Color _newColor;
	// Use this for initialization
	void Start ()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _newColor = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _renderer.color.a);
	}
	
	// Update is called once per frame
	void Update ()
    {
        _newColor.a = (maxAlpha * (1.0f-powerController.PowerPercentage()))/255f;
        _renderer.color = _newColor;        	
	}
}
