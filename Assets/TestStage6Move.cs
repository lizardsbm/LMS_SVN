using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStage6Move : MonoBehaviour
{
    private const float MAX_Y = 10.8f;

    private const float MAX_SPEED = 2.1f;

    public float speed = 0.3f;

    private float moveSpeed;

    private Transform TrfCam;

    private void Start() {
        TrfCam = Camera.main.transform;
    }

    private float PosY {
        get {
            return TrfCam.transform.localPosition.y;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            if(PosY < MAX_Y) {
                moveSpeed = (speed * Time.deltaTime) + moveSpeed;

                if(moveSpeed > MAX_SPEED) {
                    moveSpeed = MAX_SPEED;
                }

                if(PosY < MAX_Y) {
                    TrfCam.setLocalY(PosY + moveSpeed);
                } else {
                    TrfCam.setLocalY(MAX_Y);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            moveSpeed = 0;
        }

        if (Input.GetKey(KeyCode.S)) {
            if (PosY > 0) {
                moveSpeed = (speed * Time.deltaTime) + moveSpeed;

                if (moveSpeed > MAX_SPEED) {
                    moveSpeed = MAX_SPEED;
                }

                if (PosY > 0) {
                    TrfCam.setLocalY(PosY - moveSpeed);
                } else {
                    TrfCam.setLocalY(0);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.S)) {
            moveSpeed = 0;
        }
    }
}
