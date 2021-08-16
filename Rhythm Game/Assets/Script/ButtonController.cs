using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite m_defaaultImage;
    public Sprite m_pressedImage;

    public KeyCode keyToPress;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = m_pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = m_defaaultImage;
        }
    }
}
