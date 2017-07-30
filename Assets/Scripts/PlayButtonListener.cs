using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonListener : MonoBehaviour {
	public void Click()
    {
        SceneManager.LoadScene("main");
    }
}
