using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_NPCInteraction : MonoBehaviour
 {

	//Raycast
	public float raycastLength = 2;
	private Ray ray;
	private RaycastHit rayHit;

	private string switchToolTipState = string.Empty;


	void Start()
	{

		//Shoots our raycast.
		StartCoroutine(CastRayCoroutine(0.5f));
		//switchToolTipState = "EMPTY_TOOLTIP";
		
	}

	void Update()
	{

		switch(switchToolTipState)
		{

			case "EMPTY_TOOLTIP":
			break;

			case "SHOW_TOOLTIP":
			//ShowInteractiveToolTip();
			break;
			
			case "HIDE_TOOLTIP":
			//ExitInteractiveToolTip();
			break;

		}
		
	}
	
	//For performance Ray is only casted after given time and loops over. 
	IEnumerator CastRayCoroutine(float waitTime)
	{

		LayerMask npcs = LayerMask.NameToLayer("NPCs");

		Vector3 forwardDir = transform.TransformDirection(Vector3.forward);
		ray = new Ray(transform.position, forwardDir);

		if (Physics.Raycast(ray, out rayHit, raycastLength))
		{

			LayerMask layerHit = rayHit.transform.gameObject.layer;

			if (layerHit == npcs && rayHit.transform.CompareTag("NPC"))
			{
				Debug.Log("NPC in Sight: " + rayHit.transform.name);
				//switchToolTipState = "SHOW_TOOLTIP";
			}
		}
		else
		{
			if (switchToolTipState != "HIDE_TOOLTIP")
			{
				Debug.Log("NO NPC in Sight!");
				//switchToolTipState = "HIDE_TOOLTIP";
			}
		}

		Debug.DrawRay(transform.position, forwardDir * raycastLength, Color.blue);

		yield return new WaitForSeconds(waitTime);
		StartCoroutine(CastRayCoroutine(0.5f));

	}
}
