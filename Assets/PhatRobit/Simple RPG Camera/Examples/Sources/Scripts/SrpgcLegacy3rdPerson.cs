using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SrpgcLegacyAnimator))]
public class SrpgcLegacy3rdPerson : MonoBehaviour
{
	public float speed = 6;

	private Transform _camera;

	private CharacterController _controller;
	private SrpgcLegacyAnimator _animator;

	private Transform _t;

	void Start()
	{
		_t = transform;
		_controller = GetComponent<CharacterController>();
		_animator = GetComponent<SrpgcLegacyAnimator>();

		_camera = Camera.main.transform;
	}

	void Update()
	{
		Vector3 forward = _camera.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 walkDirection = ((h * right + v * forward) * speed);

		if(walkDirection != Vector3.zero)
		{
			_t.rotation = Quaternion.LookRotation(walkDirection);
			_animator.Action = "run";
		}
		else
		{
			_animator.Action = "idle";
		}

		_controller.Move((walkDirection + Physics.gravity) * Time.deltaTime);
	}
}