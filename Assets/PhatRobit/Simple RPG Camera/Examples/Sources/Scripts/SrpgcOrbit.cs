using UnityEngine;
using System.Collections;

public class SrpgcOrbit : MonoBehaviour
{
	public Transform target;
	public float orbitSpeed = 20;
	public float rotationSpeed = 10;

	private Animator _animator;

	private Transform _t;

	void Start()
	{
		_t = transform;
		_animator = GetComponent<Animator>();

		if(_animator)
		{
			_animator.SetFloat("Speed", 0.21f);
		}
	}

	void Update()
	{
		if(target)
		{
			_t.RotateAround(target.position, _t.right, Time.deltaTime * orbitSpeed);
			_t.RotateAround(_t.position, _t.up, Time.deltaTime * rotationSpeed);
		}
	}
}