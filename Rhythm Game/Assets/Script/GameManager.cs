using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource m_music;

    public bool m_startPlaying;

    public BeatScoller m_BS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 120;
    public int scorePerPerfectNote = 150;
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missHits;

    public GameObject resultScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_startPlaying)
        {
            if (Input.anyKeyDown)
            {
                m_startPlaying = true;
                m_BS.hasStarted = true;

                m_music.Play();
            }
        }
        else
        {
            if(!m_music.isPlaying && !resultScreen.activeInHierarchy)
            {
                resultScreen.SetActive(true);
                normalsText.text = "" + normalHits;
                goodsText.text = "" + goodHits;
                perfectsText.text = "" + perfectHits;
                missesText.text = "" + missHits;

                float totalHits = normalHits + goodHits + perfectHits;
                float percentHit = (totalHits / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";
                string rankVal = "F";
                if(percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 85)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }
                rankText.text = rankVal;
                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    public void NoteHit()
    {
        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        multiText.text = "Multiplier: x" + currentMultiplier;
        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;
    }

    public void NoteMiss()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;

        missHits++;
    }
}
