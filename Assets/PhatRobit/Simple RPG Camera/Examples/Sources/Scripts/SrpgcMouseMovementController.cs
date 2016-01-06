using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class SrpgcMouseMovementController : MonoBehaviour
{
	public LayerMask validLayers = new LayerMask();
	public float destinationBuffer = 0.5f;
	public float speedDamp = 0.1f;
	public string speedFloat = "Speed";
	public bool moveWithMecanim = true;
	public float speed = 1;
	public float gravity = 1;
	public bool useTouch = false;
	public float touchDelay = 0.2f;

	private float _touchTimer = 0;

	private Vector3 _destinationPosition = new Vector3();
	private bool _destinationReached = true;

	private Vector3 _velocity = new Vector3();

	private Transform _t;
	private Animator _animator;
	private CharacterController _controller;
	private Rigidbody _rigidbody;

	void Start()
	{
		_t = transform;
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController>();
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		float distance = Vector3.Distance(_destinationPosition, _t.position);

		if(distance < destinationBuffer || _destinationReached)
		{
			_animator.SetFloat(speedFloat, 0, speedDamp, Time.deltaTime);
			_destinationReached = true;
			_velocity = Vector3.zero;
		}
		else
		{
			_t.LookAt(new Vector3(_destinationPosition.x, _t.position.y, _destinationPosition.z));
			_animator.SetFloat(speedFloat, 1, speedDamp, Time.deltaTime);
			_velocity = _t.TransformDirection(new Vector3(0, 0, 1)) * speed;
		}

		if(GUIUtility.hotControl == 0)
		{
			Vector3 position = new Vector3();
			bool canMove = false;

			if(useTouch)
			{
				if(Input.touchCount == 1)
				{
					Touch touch = Input.GetTouch(0);

					if(touch.phase == TouchPhase.Began)
					{
						_touchTimer = touchDelay;
					}

					if(_touchTimer <= 0)
					{
						canMove = true;
						position = touch.position;
					}
					else
					{
						_touchTimer -= Time.deltaTime;
					}
				}
			}
			else
			{
				if(Input.GetMouseButton(0))
				{
					canMove = true;
					position = Input.mousePosition;
				}
			}

			if(canMove)
			{
				_destinationReached = false;

				Ray ray = Camera.main.ScreenPointToRay(position);
				RaycastHit[] hits = Physics.RaycastAll(ray, 1000, validLayers);

				foreach(RaycastHit hit in hits)
				{
					if(!hit.collider.isTrigger)
					{
						_destinationPosition = hit.point;
						break;
					}
				}
			}
		}
	}

	void FixedUpdate()
	{
		if(!moveWithMecanim)
		{
			if(_controller)
			{
				_velocity.y -= gravity;
				_velocity.y = Mathf.Clamp(_velocity.y, -90, 90);
				_controller.Move(_velocity * Time.deltaTime);
			}
			else if(_rigidbody)
			{
				_rigidbody.velocity = _velocity;
			}
		}
	}
}