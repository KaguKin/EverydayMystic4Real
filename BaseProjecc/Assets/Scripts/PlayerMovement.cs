using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController charController;
    Vector3 moveDir;
    public float speed;
    public float jumpPower;
    public GameObject target;

    float usedSpeed;

    Animator myAnim;

    float rotX;
    float rotZ;

    bool running;

    public static bool canMove = true;

    CombatController CC;

    Camera cam;
    float yVel;

    GameObject targetEnemy;

    bool canPlayLanding = false;

    GameObject climbTarget;
    GameObject climbMoveToTarget;
    bool climbing;
    bool movingTo = false;

    bool canRotate = true;
    

	// Use this for initialization
	void Start ()
    {
        charController = GetComponent<CharacterController>();
        myAnim = GetComponent<Animator>();
        cam = Camera.main;
        usedSpeed = speed;

        CC = GetComponent<CombatController>();
	}

  
    public GameObject GetTargetEnemy()
    {
        return targetEnemy;
    }

    public void SetTargetEnemy(GameObject enemy)
    {
        targetEnemy = enemy;
    }

    public void SetSpeed(float newSpeed)
    {
        usedSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        usedSpeed = speed;
        moveDir.y = -0.1f;
    }

    public void FinishClimb()
    {
        climbing = false;
    }

    public void MovingTo()
    {
        movingTo = !movingTo;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!canMove)
        {
            return;
        }

        if(movingTo)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, climbMoveToTarget.transform.position, 1f);
            transform.position = newPos;
        }

        //move dir effected by camera direction.
        moveDir = (cam.transform.forward * Input.GetAxis("Vertical")) + (cam.transform.right * Input.GetAxis("Horizontal"));

        //When not running
        if(moveDir == Vector3.zero)
        {
            running = false;
        }
        //When running.
        else
        {
            running = true;

            if (targetEnemy == null && CC.InCombat()) { CC.SetInCombat(false); }

            if (!CC.InCombat()) { target.transform.position = transform.position + moveDir; }
            else
            {
                if (target != null) { target.transform.position = targetEnemy.transform.position; }
            }
        }

        myAnim.SetBool("run", running);

        
        //Change y velocity of the player if they are not grounded.
        if(!charController.isGrounded)
        {
            //Should be in line with real world gravity - ish.
            yVel -= 9.8f * Time.deltaTime;
            canPlayLanding = true;
        }else
        {
            if (canPlayLanding) { myAnim.SetTrigger("land"); canPlayLanding = false; }
            if (yVel < -0.09f) { yVel = -0.09f; }
        }
        //If the player presses space and the player is on the ground then jump
        if (Input.GetKeyDown(KeyCode.Space) && charController.isGrounded)
        {
            yVel = jumpPower;
            myAnim.Play("Jump_Start");
            canPlayLanding = true;
        }

        moveDir.y = yVel;
        myAnim.SetFloat("yVel", yVel);

        if (canRotate) { transform.LookAt(target.transform); }
        else { moveDir.y = -0.1f;}

        transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y, rotZ);

        charController.Move(moveDir * usedSpeed * Time.deltaTime);

        //Look in direction the player has input based on the placement of the gameobject.
        rotX = transform.eulerAngles.x;
        rotZ = transform.eulerAngles.z;     

        CheckForClimableWall();
	}


    void CheckForClimableWall()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
        {
            if (hit.transform.tag == "Climable")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    GameObject rightTarget = hit.transform.GetChild(0).gameObject;
                    climbTarget = rightTarget;
                    climbMoveToTarget = hit.transform.GetChild(1).gameObject;
                    //myAnim.MatchTarget(rightTarget.transform.position, rightTarget.transform.rotation, AvatarTarget.RightHand, new MatchTargetWeightMask(Vector3.one, 1f), 0.141f, 0.78f);
                    myAnim.SetTrigger("climb");
                    //HI MAX - PLACED 'climbing = true' IN OWN METHOD BELOW, ALLOWS US TO TIME THE POINT WHEN THE HAND SNAPS TO IN THE ANIMATOR EVENTS - KANE :)
                    //climbing = true;
                }
            }
        }
    }

    private void OnAnimatorIK()
    {
        if(climbing)
        {
            if(climbTarget != null)
            {
                myAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                myAnim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                myAnim.SetIKPosition(AvatarIKGoal.RightHand, climbTarget.transform.position);
                myAnim.SetIKRotation(AvatarIKGoal.RightHand, climbTarget.transform.rotation);
            }
        }
    }

    public void ToggleRotation()
    {
        canRotate = !canRotate;
    }

    public void StartClimb()
    {
        climbing = true;
    }

    void CheckInput()
    {
        if (Input.GetAxis("Vertical") == 0f)
        {
            if (Input.GetAxis("Horizontal") < 0f)
            {
                // look left
                target.transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
            }
            if (Input.GetAxis("Horizontal") > 0f)
            {
                // look right
                target.transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal") == 0f)
            {
                if (Input.GetAxis("Vertical") < 0f)
                {
                    // look back
                    target.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3);
                }
                if (Input.GetAxis("Vertical") > 0f)
                {
                    // look forward
                    target.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3);
                }
            }
            else
            {
                if(Input.GetAxis("Vertical") < 0f && Input.GetAxis("Horizontal") < 0f)
                {
                    // look back + left
                    target.transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z - 3);
                }
                if (Input.GetAxis("Vertical") > 0f && Input.GetAxis("Horizontal") < 0f)
                {
                    // look forward + left
                    target.transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z + 3);
                }
                if (Input.GetAxis("Vertical") > 0f && Input.GetAxis("Horizontal") > 0f)
                {
                    // look forward + right
                    target.transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z + 3);
                }
                if (Input.GetAxis("Vertical") < 0f && Input.GetAxis("Horizontal") > 0f)
                {
                    // look back + right
                    target.transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z - 3);
                }
            }
        }


    }
}
