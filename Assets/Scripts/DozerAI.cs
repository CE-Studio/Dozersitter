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
    float speed = 10f;
    float targRot;
    Rigidbody rb;
    int turnDirection;
    public static bool legacyAI = false;
    
    void Start() {
        dozerState = "Wait";
        randomInt = Random.Range(1, 30);
        waitWeight = Random.Range(25, 40);
        turnWeight = Random.Range(20, 30);
        moveWeight = Random.Range(20, 30);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (rb.position.x > 14 || rb.position.x < -14 || rb.position.z > 24 || rb.position.z < -24) {
            dozerState = "Reverse";
            randomInt = 20;
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
                        print(targPosX);
                    }
                } else {
                    dozerState = "Inspect";
                }
                break;
            case "Wait":
                randomInt--;
                if (randomInt <= 0) {
                    dozerState = "Idle";
                }
                print("Dozer is waiting for another " + randomInt + " ticks.");
                break;
            case "Turn":
                if (turnDirection == 1) {
                    transform.Rotate(Vector3.up, speed * 5 * Time.deltaTime);
                }
                else if (turnDirection == 2) {
                    transform.Rotate(Vector3.up, -speed * 5 * Time.deltaTime);
                }
                randomInt--;
                if (randomInt <= 0) {
                    dozerState = "Idle";
                }
                print("Dozer is turning for another " + randomInt + " ticks.");
                break;
            case "Move":
                rb.AddForce(transform.forward * speed, ForceMode.Force);
                randomInt--;
                if (randomInt <= 0) {
                    dozerState = "Idle";
                }
                print("Dozer is moving for another " + randomInt + " ticks.");
                break;
            case "Inspect":
                dozerState = "Idle";
                break;
            case "Reverse":
                rb.AddForce(transform.forward * -speed, ForceMode.Force);
                randomInt--;
                if (randomInt <= 0) {
                    dozerState = "Idle";
                }
                print("Dozer is reversing for another " + randomInt + " ticks.");
                break;
            case "NewMove":
                targRot = Mathf.Atan((targPosZ - transform.position.z) / (targPosX - transform.position.x));
                break;
        }
        //print("The dozer had a " + waitWeight + "% chance to wait, a " + turnWeight + "% chance to turn, and a " + moveWeight + "% chance to move. It chose to " + dozerState);
    }
}
