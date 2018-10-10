using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableWater : MonoBehaviour
{

    public float maxY;
    public float minY;
    public float speed;

    public bool flood;
    public bool recede;

    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (flood)
        {
            if(target.transform.position.y < maxY)
            {
                float s = target.transform.position.y;
                s += speed * Time.deltaTime;
                target.transform.position = new Vector3(target.transform.position.x, s, 
                    target.transform.position.z);
            }
            else
            {
                target.transform.position = new Vector3(target.transform.position.x, maxY,
                   target.transform.position.z);

                flood = false;
            }
        }

        if (recede)
        {
            if (target.transform.position.y > minY)
            {
                float s = target.transform.position.y;
                s -= speed * Time.deltaTime;
                target.transform.position = new Vector3(target.transform.position.x, s, 
                    target.transform.position.z);
            }
            else
            {
                target.transform.position = new Vector3(target.transform.position.x, minY,
                   target.transform.position.z);
                
                recede = false;
            }
        }
	}

    public bool NeedsToFlood()
    {
        if(target.transform.position.y == minY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
