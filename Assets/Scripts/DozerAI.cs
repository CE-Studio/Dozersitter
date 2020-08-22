using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DozerAI : MonoBehaviour {
    string dozerState;
    // Idle ---- Ready for a command
    // Wait ---- Waiting around for a bit
    // Turn ---- Turning at a set speed to a random direction
    // Move ---- Moving at a set speed for a random time
    // Box ----- Sttaches itself to the closest box, marking it off a global list as chosen, to move to and push for a certain amount of time
    //           Once the time is up, it will move on. To ensure it doesn't get stuck pushing the same box, it keeps the box as marked as used until it finds a new box to push
    //           It will also have a box cooldown timer so that it won't automatically gravitate to any and all boxes it drives near
    // Inspect - Driving to and looking at the player
    // Reverse - Backing up away from a wall
    int randomInt;
    int waitWeight;
    int turnWeight;
    int moveWeight;
    float targPosX = 0;
    float targPosZ = 0;
    float speed = 6f;
    Rigidbody rb;
    int turnDirection;
    public static bool legacyAI = false;
    Vector3 lastPos;
    Vector3 lastRot;
    Transform eyes;
    Transform camPan;
    Transform camLift;
    Transform wheelRF;
    Transform wheelRR;
    Transform wheelLF;
    Transform wheelLR;

    void Start() {
        dozerState = "Wait";
        randomInt = Random.Range(1, 30);
        waitWeight = Random.Range(25, 40);
        turnWeight = Random.Range(20, 30);
        moveWeight = Random.Range(20, 30);
        rb = GetComponent<Rigidbody>();

        eyes = this.gameObject.transform.GetChild(0).GetChild(2);
        camPan = this.gameObject.transform.GetChild(0).GetChild(1);
        camLift = this.gameObject.transform.GetChild(0).GetChild(1).GetChild(0);
        wheelLF = this.gameObject.transform.GetChild(0).GetChild(4);
        wheelLR = this.gameObject.transform.GetChild(0).GetChild(5);
        wheelRF = this.gameObject.transform.GetChild(0).GetChild(6);
        wheelRR = this.gameObject.transform.GetChild(0).GetChild(7);
    }

    void FixedUpdate() {
        if (rb.position.x > 14 || rb.position.x < -14 || rb.position.z > 24 || rb.position.z < -24) {
            dozerState = "Reverse";
            randomInt = 20;
        }
        if (randomInt <= 0) {
            dozerState = "Idle";
        }
        switch (dozerState) {
            case "Idle":
                randomInt = Random.Range(1, 100);
                if (randomInt <= waitWeight) {
                    dozerState = "Wait";
                    randomInt = Random.Range(25, 75);
                } else if (randomInt > waitWeight && randomInt <= waitWeight + turnWeight) {
                    dozerState = "Turn";
                    randomInt = Random.Range(25, 75);
                    turnDirection = Random.Range(1, 3);
                } else if (randomInt > waitWeight + turnWeight && randomInt <= waitWeight + turnWeight + moveWeight) {
                    if (legacyAI) {
                        dozerState = "Move";
                        randomInt = Random.Range(25, 75);
                    } else {
                        randomInt = 20;
                        targPosX = Random.Range(-15f, 15f);
                        targPosZ = Random.Range(-25f, 25f);
                        dozerState = "NewMove";
                        //print(targPosX);
                    }
                } else {
                    dozerState = "Inspect";
                }
                break;
            case "Wait":
                randomInt--;
                //print("Dozer is waiting for another " + randomInt + " ticks.");
                break;
            case "Turn":
                if (turnDirection == 1) {
                    transform.Rotate(Vector3.up, speed * 5 * Time.deltaTime);
                }
                else if (turnDirection == 2) {
                    transform.Rotate(Vector3.up, -speed * 5 * Time.deltaTime);
                }
                randomInt--;
                //print("Dozer is turning for another " + randomInt + " ticks.");
                break;
            case "Move":
                rb.AddForce(transform.forward * speed, ForceMode.Force);
                randomInt--;
                //print("Dozer is moving for another " + randomInt + " ticks.");
                break;
            case "Inspect":
                dozerState = "Idle";
                break;
            case "Reverse":
                rb.AddForce(transform.forward * -speed, ForceMode.Force);
                randomInt--;
                //print("Dozer is reversing for another " + randomInt + " ticks.");
                break;
            case "NewMove":
                Quaternion OriginalRot = transform.rotation;
                transform.LookAt(new Vector3(targPosX, 0.5f, targPosZ));
                Quaternion NewRot = transform.rotation;
                transform.rotation = OriginalRot;
                transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, speed * 0.3f * Time.deltaTime);

                if (1f < Mathf.Sqrt(Mathf.Pow(targPosZ - transform.position.z, 2f) + Mathf.Pow(targPosX - transform.position.x, 2))) {
                    rb.AddForce(transform.forward * speed, ForceMode.Force);

                    camPan.LookAt(new Vector3(targPosX, 0.5f, targPosZ));
                    camLift.LookAt(new Vector3(targPosX, 0f, targPosZ));

                    Debug.DrawLine(transform.position, new Vector3(targPosX, 0, targPosZ));
                    Debug.DrawLine(camLift.position, new Vector3(targPosX, 0, targPosZ));
                } else {
                    randomInt--;
                }

                break;
        }

        float movement;
        movement = Mathf.Sqrt(Mathf.Pow(lastPos.z - transform.position.z, 2f) + Mathf.Pow(lastPos.x - transform.position.x, 2));
        float turnDist;
        turnDist = transform.eulerAngles.y - lastRot.y;

        wheelLF.Rotate(new Vector3(movement * 200, 0, 0));
        wheelLR.Rotate(new Vector3(movement * 200, 0, 0));
        wheelRF.Rotate(new Vector3(movement * 200, 0, 0));
        wheelRR.Rotate(new Vector3(movement * 200, 0, 0));

        lastPos = transform.position;
        lastRot = transform.eulerAngles;
        
        //print("The dozer had a " + waitWeight + "% chance to wait, a " + turnWeight + "% chance to turn, and a " + moveWeight + "% chance to move. It chose to " + dozerState);
    }
}
