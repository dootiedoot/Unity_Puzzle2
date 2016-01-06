using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FenceManager : MonoBehaviour 
{
	public Transform FencePrefab;
	public Transform FenceCornerPrefab;
	public int SizeX;
	public int SizeY;
	public List<Transform> Fences;
	
	// Use this for initialization
	void Start () 
	{
		GenerateGrid(SizeX, SizeY);
	}

	public void GenerateGrid(int x, int y)
	{
		int count = 1;
		Transform Fence = null;
		for (y = 0; y <= SizeY; y++)
        {
			for (x = 0; x <= SizeX; x++)
            {
				// Corner Fences from
				if(x == 0 && y == 0)
				{
					Fence = Instantiate(FenceCornerPrefab, new Vector3(x, 0, y), Quaternion.identity) as Transform;
                    Fence.SetParent(transform);
					Fence.name = count.ToString();
                    Fences.Add(Fence);
					count++;
				}
				else if(x == SizeX && y == 0)
				{
					Fence = Instantiate(FenceCornerPrefab, new Vector3(x, 0, y), Quaternion.Euler(0,270,0)) as Transform;
                    Fence.SetParent(transform);
					Fence.name = count.ToString();
                    Fences.Add(Fence);
					count++;
				}
				else if(x == 0 && y == SizeY)
				{
					Fence = Instantiate(FenceCornerPrefab, new Vector3(x, 0, y), Quaternion.Euler(0,90,0)) as Transform;
                    Fence.SetParent(transform);
					Fence.name = count.ToString();
                    Fences.Add(Fence);
					count++;
				}
				else if(x == SizeX && y == SizeY)
				{
					Fence = Instantiate(FenceCornerPrefab, new Vector3(x, 0, y), Quaternion.Euler(0,180,0)) as Transform;
                    Fence.SetParent(transform);
					Fence.name = count.ToString();
                    Fences.Add(Fence);
					count++;
				}

				//Side fences
				else if(x == 0 || x == SizeX)
				{
					Fence = Instantiate(FencePrefab, new Vector3(x, 0, y), Quaternion.Euler(0,90,0)) as Transform;
                    Fence.SetParent(transform);
					Fence.name = count.ToString();
                    Fences.Add(Fence);
					count++;
				}
				else if (y == 0 || y == SizeY)
				{
					Fence = Instantiate(FencePrefab, new Vector3(x, 0, y), Quaternion.identity) as Transform;
                    Fence.SetParent(transform);
					Fence.name = count.ToString();
                    Fences.Add(Fence);
					count++;
				}
            }
		}
	}
}
