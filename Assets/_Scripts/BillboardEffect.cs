using UnityEngine;
using System.Collections;

public class BillboardEffect : MonoBehaviour 
{

	public Transform target;

	// Update is called once per frame
	void Update() 
	{
        if (target)
            transform.LookAt(transform.position + target.transform.rotation * Vector3.forward, target.transform.rotation * Vector3.up);
        else
            target = GameObject.Find("LOCAL Player").GetComponentInChildren<Camera>().transform;
	}
}
