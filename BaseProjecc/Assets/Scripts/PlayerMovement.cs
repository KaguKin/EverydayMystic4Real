using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController charController;
    Vector3 moveDir;
    public float speed;
    public GameObject target;

    Animator myAnim;

    float rotX;
    float rotZ;

    bool running;

    public static bool canMove = true;

	// Use this for initialization
	void Start ()
    {
        charController = GetComponent<CharacterController>();
        myAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!canMove)
        {
            return;
        }

        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");

        if(moveDir.x == 0 && moveDir.z == 0)
        {
            running = false;
            myAnim.SetBool("run", false);
        }
        else
        {
            running = true;
            myAnim.SetBool("run", true);
        }

        /*
        if(Input.GetAxis("Horizontal") > 0f)
        {
            moveDir.x = 1;
            moveDir.z = -1;
        }
        if(Input.GetAxis("Horizontal") < 0f)
        {
            moveDir.x = -1;
            moveDir.z = 1;
        }
        if (Input.GetAxis("Vertical") > 0f)
        {
            moveDir.x = 1;
            moveDir.z = 1;
        }
        if (Input.GetAxis("Vertical") < 0f)
        {
            moveDir.x = -1;
            moveDir.z = -1;
        }

        if(Input.GetAxis("Vertical") == 0f && Input.GetAxis("Horizontal") == 0f)
        {
            moveDir.x = 0;
            moveDir.z = 0;
        }
        */
        CheckInput();
        //moveDir = transform.TransformDirection(moveDir);

        

        moveDir.y = -3f;

        charController.Move(moveDir * speed * Time.deltaTime);


        rotX = transform.eulerAngles.x;
        rotZ = transform.eulerAngles.z;

        transform.LookAt(target.transform);

        transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y, rotZ);
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
