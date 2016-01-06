using UnityEngine;
using System.Collections;

public class SrpgcKeyboardMovementController : MonoBehaviour
{
	public bool rootMotion = true;

	public float speedThreshold = 0.2f;
	public float speedDamp = 0.05f;
	public float directionDamp = 0.05f;

	public string speedFloat = "Speed";
	public string directionFloat = "Direction";
	public string angleFloat = "Angle";
	public string jumpTrigger = "Jump";

	public KeyCode keyStrafeLeft = KeyCode.Q;
	public KeyCode keyStrafeRight = KeyCode.E;
	public float walkSpeed = 3;
	public float runSpeed = 6;
	public float turnSpeed = 6;
	public float jumpPower = 8;
	public float gravity = 20;
	public float slopeLimit = 55;
	public float antiBunny = 0.75f;

	private float _strafe = 0;

	private Animator _animator;

	private float _direction = 0;
	private float _angle = 0;
	private bool _grounded = false;
	private float _speed = 0;
	private bool _running = true;
	private Vector3 _velocity = new Vector3();
	private CharacterController _controller;

	private Vector3 _input = new Vector3();

	private Transform _t;

	private int _locomotionPivotLID = 0;
	private int _locomotionPivotRID = 0;
	private int _locomotionPivotTransLID = 0;
	private int _locomotionPivotTransRID = 0;
	private int _idlePivotLID = 0;
	private int _idlePivotRID = 0;
	private int _idlePivotTransLID = 0;
	private int _idlePivotTransRID = 0;

	private AnimatorStateInfo _stateInfo;
	private AnimatorTransitionInfo _transInfo;

	void Start()
	{
		_t = transform;
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController>();

		_locomotionPivotLID = Animator.StringToHash("Base Layer.Locomotion Pivot Left");
		_locomotionPivotRID = Animator.StringToHash("Base Layer.Locomotion Pivot Right");
		_locomotionPivotTransLID = Animator.StringToHash("Base Layer.Locomotion -> Base Layer.Locomotion Pivot Left");
		_locomotionPivotTransRID = Animator.StringToHash("Base Layer.Locomotion -> Base Layer.Locomotion Pivot Right");
		_idlePivotLID = Animator.StringToHash("Base Layer.Idle Pivot Left");
		_idlePivotRID = Animator.StringToHash("Base Layer.Idle Pivot Right");
		_idlePivotTransLID = Animator.StringToHash("Base Layer.Idle -> Base Layer.Idle Pivot Left");
		_idlePivotTransRID = Animator.StringToHash("Base Layer.Idle -> Base Layer.Idle Pivot Right");
	}

	void Update()
	{
		_input.x = Input.GetAxis("Horizontal");
		_input.y = Input.GetAxis("Vertical");

		_strafe = Input.GetKey(keyStrafeLeft) ? -1 : (Input.GetKey(keyStrafeRight) ? 1 : 0);

		if(rootMotion)
		{
			RootMotionMovement();
		}
		else
		{
			NonRootMotionMovement();
		}
	}

	void FixedUpdate()
	{
		if(!rootMotion)
		{
			_velocity.y -= gravity * Time.deltaTime;
			_controller.Move(_velocity * Time.deltaTime);
		}
	}

	private void RootMotionMovement()
	{
		_stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
		_transInfo = _animator.GetAnimatorTransitionInfo(0);

		StickToWorldspace();

		float speed = _input.sqrMagnitude;

		_animator.SetFloat(speedFloat, speed, speedDamp, Time.deltaTime);

		if(speed < speedThreshold)
		{
			_direction = 0;
		}

		_animator.SetFloat(directionFloat, _direction, directionDamp, Time.deltaTime);

		if(!IsInPivot())
		{
			if(speed > speedThreshold)
			{
				_animator.SetFloat(angleFloat, _angle);
			}
			else
			{
				_animator.SetFloat(directionFloat, 0);
				_animator.SetFloat(angleFloat, 0);
			}
		}
	}

	private void NonRootMotionMovement()
	{
		// If the user is not holding right-mouse button, rotate the player with the X axis instead of strafing
		if(!Input.GetMouseButton(1) && _input.x != 0 && _strafe == 0)
		{
			_t.Rotate(new Vector3(0, _input.x * (turnSpeed / 2.0f), 0));
			_input.x = 0;
		}
		else
		{
			if(_strafe != 0)
			{
				_input.x = _strafe;
			}
		}

		_speed = _input.y >= 0 ? (_running ? runSpeed : walkSpeed) : walkSpeed;

		// If on the ground, test to see if still on the ground and apply movement direction
		if(_grounded)
		{
			_animator.SetFloat(speedFloat, _input.y);
			_animator.SetFloat(directionFloat, _input.x);

			_velocity = new Vector3(_input.x, -antiBunny, _input.y);
			_velocity = _t.TransformDirection(_velocity) * _speed;

			if(Input.GetButtonDown("Jump"))
			{
				_animator.SetTrigger(jumpTrigger);
				_velocity.y = jumpPower;
				_grounded = false;
			}

			if(!Physics.Raycast(_t.position, -Vector3.up, 0.2f))
			{
				_grounded = false;
			}
		}
	}

	private void StickToWorldspace()
	{
		Vector3 rootDirection = _t.forward;

		// Get camera direction
		Vector3 cameraDirection = Camera.main.transform.forward;
		cameraDirection.y = 0;
		Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

		// Convert joystick input in worldspace coords
		Vector3 moveDirection = referentialShift * new Vector3(_input.x, 0, _input.y);
		Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

		float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

		if(!IsInPivot())
		{
			_angle = angleRootToMove;
		}

		angleRootToMove /= 180f;
		_direction = angleRootToMove * 3;
	}

	private bool IsInPivot()
	{
		int stateHash;

#if UNITY_5_0
		stateHash = _stateInfo.fullPathHash;
#else
		stateHash = _stateInfo.nameHash;
#endif

		return stateHash == _locomotionPivotLID ||
				stateHash == _locomotionPivotRID ||
				_transInfo.nameHash == _locomotionPivotTransLID ||
				_transInfo.nameHash == _locomotionPivotTransRID ||
				stateHash == _idlePivotLID ||
				stateHash == _idlePivotRID ||
				_transInfo.nameHash == _idlePivotTransLID ||
				_transInfo.nameHash == _idlePivotTransRID;
	}

	void OnControllerColliderHit(ControllerColliderHit col)
	{
		// This keeps the player from sticking to walls
		float angle = col.normal.y * 90;

		if(angle < slopeLimit)
		{
			if(_grounded)
			{
				_velocity = Vector3.zero;
			}

			if(_velocity.y > 0)
			{
				_velocity.y = 0;
			}
			else
			{
				_velocity += new Vector3(col.normal.x, 0, col.normal.z).normalized;
			}

			_grounded = false;
		}
		else
		{
			// Player is grounded here
			_grounded = true;
			_velocity.y = 0;
		}
	}
}