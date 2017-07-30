using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireAnimation : MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void PowerUp()
    {
        anim.SetTrigger("Activate");
    }
}
