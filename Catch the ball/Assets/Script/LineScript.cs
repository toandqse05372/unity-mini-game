using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public float moveSpeed;
    float xDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //kh bam sang trai -1, bam sang phai +1
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = xDirection * Time.deltaTime * moveSpeed;
        if (transform.position.x <= -9 && xDirection < 0 || transform.position.x >= 9 && xDirection > 0) return;
        transform.position += new Vector3(moveStep, 0, 0);
    }
}
