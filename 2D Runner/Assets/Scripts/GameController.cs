using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject obstacle;

    public float spawnTime;

    float m_spawnTime;
    bool m_isGameOver;
    int m_score;

    public int Score { get => m_score; set => m_score = value; }
    public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }

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
        if (m_isGameOver)
        {
            m_spawnTime = 0;
            m_ui.showGameOverPanel(true);
            return;
        }

        m_spawnTime -= Time.deltaTime;

        if(m_spawnTime <= 0)
        {
            SpawnObstacle();
            m_spawnTime = spawnTime;
        }
    }

    public void ScoreIncrement()
    {
        if (IsGameOver) return;
        m_score++;
        m_ui.SetScoreText("Score: " + m_score);
    }

    public void SpawnObstacle()
    {
        float randYPos = Random.Range(-2.5f, -0.8f);
        Vector2 spawnPos = new Vector2(13, randYPos);
        if (obstacle)
        {
            Instantiate(obstacle, spawnPos, Quaternion.identity);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("Gameplay");
    }

}
