using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    [SerializeField] int live = 3;
    [SerializeField] int score;
    [SerializeField] Text liveText;
    [SerializeField] Text scoreText;
    public static GameSession instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        liveText.text = live.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    public void ProcessPlayerDead()
    {
        if (live > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        live--;
        liveText.text = live.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
