using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public Vector3 oriPos;
    //public float speed = 3f;


    void Start()
    {
        oriPos = transform.position;
    }

    void Update()
    {
        if (GameManager.instance.isPlay)
        {
            if (transform.position.x <= -24f)
            {
                transform.position = oriPos;
            }

            transform.Translate(Vector3.left * GameManager.instance.gameSpeed * Time.deltaTime);
        }
        
    }


}
