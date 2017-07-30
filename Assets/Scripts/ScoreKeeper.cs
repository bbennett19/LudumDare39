using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    public Text scoreText;
    public AudioSource music;
    public GameOverTransition gameOverTransition;
    private int _score = 0;
    public static int highScore = 0;
    private bool _newHighScore = false;

    public void SetScoreText()
    {
        scoreText.text = _score.ToString();
    }

    public void IncreaseScore()
    {
        _score++;
        SetScoreText();
    }

    public void GameOver(float waitTime = 0f)
    {
        if (_score > highScore)
        {
            highScore = _score;
            _newHighScore = true;
        }
        else
        {
            _newHighScore = false;
        }
        StartCoroutine(Transition(waitTime));
    }

    IEnumerator Transition(float time)
    {
        music.Stop();
        yield return new WaitForSeconds(time);
        gameOverTransition.GameOver();
    }

    public bool NewHighScore()
    {
        return _newHighScore;
    }
}
