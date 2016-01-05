using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour 
{
	public float Radius;
	public int Amount;
	public GameObject Prefab;

	private Animator animator;

	// Use this for initialization
	void Start () 
	{
		for(int i = 1; i <= Amount; i++)
		{
			Vector2 newPos  = Random.insideUnitCircle * Radius;
			GameObject obj = Instantiate(Prefab, new Vector3(newPos.x, 0, newPos.y), Quaternion.identity) as GameObject;
			obj.name = i.ToString();
			obj.transform.parent = transform;
			obj.transform.position += transform.position;
		}
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
	
}
