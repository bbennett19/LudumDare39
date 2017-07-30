using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            RayCast(Input.mousePosition);
        }
	}

    private void RayCast(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            hit.collider.gameObject.SendMessage("Click");
        }
    }
}
