using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;

    Rigidbody2D m_rb;

    bool m_isGround;

    GameController m_gc;

    public AudioSource aus;
    public AudioClip jumpSound;
    public AudioClip loseSound;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumpKeyPressed = Input.GetKeyDown(KeyCode.Space);
        if (isJumpKeyPressed && m_isGround && !m_gc.IsGameOver)
        {
            //Vector2.up = (0,1)
            m_rb.AddForce(Vector2.up * jumpForce);
            m_isGround = false;

            if(aus && jumpSound)
            {
                aus.PlayOneShot(jumpSound);
            }
        }
    }

    public void Jump()
    {
        if (!m_isGround || m_gc.IsGameOver) return;
        m_rb.AddForce(Vector2.up * jumpForce);
        m_isGround = false;

        if (aus && jumpSound)
        {
            aus.PlayOneShot(jumpSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if (aus && loseSound && !m_gc.IsGameOver)
            {
                aus.PlayOneShot(loseSound);
            }
            m_gc.IsGameOver = true;
        }
    }
}
