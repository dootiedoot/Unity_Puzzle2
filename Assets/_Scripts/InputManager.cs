using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public float CheckRadius;
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonUp("Fire1"))
		{
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, 100, Layers.Ground))
			{
				Collider[] hitColliders = Physics.OverlapSphere(hit.point, CheckRadius);
                for (int i = 0; i < hitColliders.Length; i++)
                {
					if(hitColliders[i].tag == Tags.Chick)
					{
                        hitColliders[i].GetComponent<Chick>().StartHopAnimation(hit.point);
					}
				}
			}
		}
	}
}
