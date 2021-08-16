using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject ball;
    public float spawnTime;

    float m_spawnTime;
    int m_score;
    bool m_isGameOver;

    UIManager m_ui;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnTime = 0;
        m_ui = FindObjectOfType<UIManager>();
        m_ui.SetScoreText("Score: " + m_score);
    }

    // Update is called once per frame
    void Update()
    {
        m_spawnTime -= Time.deltaTime;

        if (m_isGameOver)
        {
            m_spawnTime = 0;
            m_ui.ShowGameOverPanel(true);
            return;
        }

        if(m_spawnTime <= 0)
        {
            SpawnBall();
            m_spawnTime = spawnTime;
        }
    }
    public int Score { get => m_score; set => m_score = value; }
    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }

    public void IncrementScore()
    {
        m_score++;
        m_ui.SetScoreText("Score: " + m_score);
    }

    public void SpawnBall()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-9, 9), 6);

        if (ball)
        {
            Instantiate(ball, spawnPos, Quaternion.identity);
        }
    }

    public void Replay()
    {
        m_score = 0;
        m_isGameOver = false;
        m_ui.SetScoreText("Score: " + m_score);
        m_ui.ShowGameOverPanel(false);
    }
}
