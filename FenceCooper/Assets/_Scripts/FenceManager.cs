using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FenceManager : MonoBehaviour 
{
	public GameObject FencePrefab;
	public GameObject FenceCornerPrefab;
	public int SizeX;
	public int SizeY;
	public List<GameObject> Fences;
	
	// Use this for initialization
	void Start () 
	{
		GenerateGrid(SizeX, SizeY);
	}

	public void GenerateGrid(int x, int y)
	{
		int count = 1;
		GameObject Fence;
		for (y = 0; y <= SizeY; y++) {
			for (x = 0; x <= SizeX; x++) {
				// Corner Fences from
				if(x == 0 && y == 0)
				{
					Fence = Instantiate(FenceCornerPrefab, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
					Fence.name = count.ToString();
					Fence.transform.parent = transform;
					Fences.Add(Fence);
					count++;
				}
				else if(x == SizeX && y == 0)
				{
					Fence = Instantiate(FenceCornerPrefab, new Vector3(x, 0, y), Quaternion.Euler(0,270,0)) as GameObject;
					Fence.name = count.ToString();
					Fence.transform.parent = transform;
					Fences.Add(Fence);
					count++;
				}
				else if(x == 0 && y == SizeY)
				{
					Fence = Instantiate(FenceCornerPrefab, new Vector3(x, 0, y), Quaternion.Euler(0,90,0)) as GameObject;
					Fence.name = count.ToString();
					Fence.transform.parent = transform;
					Fences.Add(Fence);
					count++;
				}
				else if(x == SizeX && y == SizeY)
				{
					Fence = Instantiate(FenceCornerPrefab, new Vector3(x, 0, y), Quaternion.Euler(0,180,0)) as GameObject;
					Fence.name = count.ToString();
					Fence.transform.parent = transform;
					Fences.Add(Fence);
					count++;
				}

				//Side fences
				else if(x == 0 || x == SizeX)
				{
					Fence = Instantiate(FencePrefab, new Vector3(x, 0, y), Quaternion.Euler(0,90,0)) as GameObject;
					Fence.name = count.ToString();
					Fence.transform.parent = transform;
					Fences.Add(Fence);
					count++;
				}
				else if (y == 0 || y == SizeY)
				{
					Fence = Instantiate(FencePrefab, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
					Fence.name = count.ToString();
					Fence.transform.parent = transform;
					Fences.Add(Fence);
					count++;
				}
			}
		}
	}
}
