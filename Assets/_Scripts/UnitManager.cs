using UnityEngine;
using System.Collections;
using PathologicalGames;

public class UnitManager : MonoBehaviour 
{
	public float Radius;
	public int Amount;
	public Transform Unit;

    //  References
    FenceManager _fenceManager;
    //GameManager _gameManager;

    void Awake()
    {
        _fenceManager = GameObject.FindGameObjectWithTag(Tags.FenceManager).GetComponent<FenceManager>();
        //_gameManager = GameObject.FindGameObjectWithTag(Tags.GameManager).GetComponent<GameManager>();
    }

	// Use this for initialization
	void Start () 
	{
        transform.position = new Vector3((float)_fenceManager.SizeX / 2, 0, (float)_fenceManager.SizeY / 2);

		for(int i = 0; i < Amount; i++)
		{
			Vector2 newPos  = Random.insideUnitCircle * Radius;
            Transform tempUnit = PoolManager.Pools["Units"].Spawn(Unit);
            tempUnit.name = i.ToString();
            //Transform obj = Instantiate(Prefab, new Vector3(newPos.x, 0, newPos.y), Quaternion.identity) as Transform;
            tempUnit.SetParent(transform);
            tempUnit.transform.position = new Vector3(newPos.x, 0, newPos.y) + transform.position;

            //  Update Game Manager
            GameManager.AmountInField++;
		}
	}
}
