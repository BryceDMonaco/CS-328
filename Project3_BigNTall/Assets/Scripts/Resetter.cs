using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {

    public GameObject wideCharacter;
    public GameObject tallCharacter;
    public GameObject firstBall, secondBall, thirdBall;

    public Vector2 wideStartPos;
    public Vector2 tallStartPos;
    public Vector2 firstBallStartPos, secondBallStartPos, thirdBallStartPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 widePos = wideCharacter.transform.position;
        Vector2 tallPos = tallCharacter.transform.position;
        Vector2 firstBallPos = firstBall.transform.position;
        Vector2 secondBallPos = secondBall.transform.position;
        Vector2 thirdBallPos = thirdBall.transform.position;

        if (widePos.y < -7 || tallPos.y < -7)
        {
            Respawn();
        }

        if (firstBallPos.y < -10)
        {
            resetBall(1);
        }

        if (secondBallPos.y < -10)
        {
            resetBall(2);
        }

        if (thirdBallPos.y < -10)
        {
            resetBall(3);
        }

    }

    void Respawn()
    {
        wideCharacter.transform.position = wideStartPos;
        tallCharacter.transform.position = tallStartPos;
    }

    void resetBall(int ballNum)
    {
        switch (ballNum)
        {
            case 1:
                firstBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                firstBall.transform.position = firstBallStartPos;
                break;

            case 2:
                secondBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                secondBall.transform.position = secondBallStartPos;
                break;

            case 3:
                thirdBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                thirdBall.transform.position = thirdBallStartPos;
                break;
        }

        
    }
}
