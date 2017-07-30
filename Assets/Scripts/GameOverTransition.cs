using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverTransition : MonoBehaviour {
    public GameObject[] disableOnGameOver;
    public GameObject[] enableOnGameOver;
    public ScoreKeeper scoreKeeper;
    public Text newHighScoreText;
    public Text highScoreText;
    public AudioSource bulbClick;

    public void GameOver()
    {
        StartCoroutine(Transition());   
    }

    IEnumerator Transition()
    {
        for (int i = 0; i < disableOnGameOver.Length; i++)
        {
            disableOnGameOver[i].SetActive(false);
        }
        bulbClick.Play();
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < enableOnGameOver.Length; i++)
        {
            enableOnGameOver[i].SetActive(true);
        }

        newHighScoreText.gameObject.SetActive(scoreKeeper.NewHighScore());
        highScoreText.text = ScoreKeeper.highScore.ToString();
        yield return null;
    }

    public void Reset()
    {
        SceneManager.LoadScene("main");
    }
}
