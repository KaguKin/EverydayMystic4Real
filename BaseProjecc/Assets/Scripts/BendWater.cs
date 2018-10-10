using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendWater : MonoBehaviour
{

    float cooldown = 1f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        cooldown -= Time.deltaTime;
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MoveableWater"))
        {
            if (Input.GetKeyDown(KeyCode.Q) && cooldown <= 0f)
            {
                // trigger it
                if (other.gameObject.GetComponent<MoveableWater>().NeedsToFlood())
                {
                    other.gameObject.GetComponent<MoveableWater>().flood = true;
                    Debug.Log("fllodign");
                }
                else
                {
                    other.gameObject.GetComponent<MoveableWater>().recede = true;
                    Debug.Log("no");
                }
                cooldown = 1f;
            }

        }
    }

}
