using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SrpgcLegacyAnimator))]
public class SrpgcLegacyPlayerController : MonoBehaviour
{
	public bool clickToMove = false;
	public List<string> clickableTags = new List<string>();
	public bool keyboardControl = true;
	public float walkSpeed = 3;
	public float runSpeed = 6;
	public float backpedalSpeed = 3;
	public float turnSpeed = 6;
	public float jumpPower = 8;
	public float gravity = 20;
	public float slopeLimit = 55;
	public float fallThreshold = 10;
	public float antiBunny = 0.75f;

	public KeyCode keyAutoRun = KeyCode.Mouse2;
	public KeyCode keyRun = KeyCode.R;
	public bool toggleRun = true;
	public bool mouseLockMovement = false;
	public bool alwaysFaceMouse = false;
	public bool moveRelativeToCamera = false;

	public string animationIdle = "idle";
	public string animationWalk = "walk";
	public string animationRun = "run";
	public string animationJump = "jump";
	public string animationFall = "fall";
	public string animationTurnLeft = "shimmy_left";
	public string animationTurnRight = "shimmy_right";
	public string animationWalkStrafeLeft45 = "walk_strafe_left_45";
	public string animationWalkStrafeLeft90 = "walk_strafe_left_90";
	public string animationWalkStrafeRight45 = "walk_strafe_right_45";
	public string animationWalkStrafeRight90 = "walk_strafe_right_90";
	public string animationRunStrafeLeft45 = "run_strafe_left_45";
	public string animationRunStrafeLeft90 = "run_strafe_left_90";
	public string animationRunStrafeRight45 = "run_strafe_right_45";
	public string animationRunStrafeRight90 = "run_strafe_right_90";

	private bool _controllable = true;
	private bool _running = true;
	private bool _grounded = false;
	private float _speed = 0;
	private bool _autorun = false;
	private Vector3 _velocity = Vector3.zero;
	private float _fall_start = 0;
	private float _input_x = 0;
	private float _input_y = 0;
	private float _input_s = 0;
	private float _rotation = 0;
	private Vector3 _last_position = Vector3.zero;
	private float _animation_speed = 1;
	private float _move_speed = 0;
	private Vector3 _wanted_position = Vector3.zero;
	private Vector3 _last_wanted_position = Vector3.zero;
	private float _last_distance = 0;

	private Transform _t;
	private CharacterController _controller;
	private SrpgcLegacyAnimator _animator;

	public bool Grounded
	{
		get { return _grounded; }
		set { _grounded = value; }
	}

	public Vector3 Velocity
	{
		get { return _velocity; }
		set { _velocity = value; }
	}

	public float InputX
	{
		get { return _input_x; }
	}

	public float InputY
	{
		get { return _input_y; }
	}

	public float InputS
	{
		get { return _input_s; }
	}

	public float Rotation
	{
		get { return _rotation; }
	}

	public float FallPosition
	{
		get { return _fall_start; }
		set { _fall_start = value; }
	}

	public bool Controllable
	{
		get { return _controllable; }
		set { _controllable = value; }
	}

	void Start()
	{
		_t = transform;
		_controller = GetComponent<CharacterController>();
		_animator = GetComponent<SrpgcLegacyAnimator>();

		_controller.slopeLimit = slopeLimit;
		_wanted_position = _t.position;
	}

	void Update()
	{
		// If user middle-clicks, toggle autorun on/off
		if(Input.GetKeyDown(keyAutoRun))
		{
			_autorun = !_autorun;
		}

		// Toggle running / walking
		if(Input.GetKeyDown(keyRun))
		{
			_running = !_running;
		}
		else if(!toggleRun && !Input.GetKey(keyRun))
		{
			_running = false;
		}

		// If on the ground, allow jumping
		if(_grounded)
		{
			if(_controllable)
			{
				if(Input.GetButtonDown("Jump"))
				{
					_velocity.y = jumpPower;
					_grounded = false;
				}
			}
		}
	}

	void FixedUpdate()
	{
		_animator.SetSpeed(_animation_speed);

		if(_controllable)
		{
			if(clickToMove)
			{
				if(Input.GetMouseButton(0))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit[] hits = Physics.RaycastAll(ray);

					foreach(RaycastHit hit in hits)
					{
						bool done = false;

						foreach(string tag in clickableTags)
						{
							if(hit.transform.CompareTag(tag))
							{
								_wanted_position = hit.point;
								_last_distance = Vector3.Distance(_t.position, _wanted_position);
								done = true;
								break;
							}
						}

						if(done)
						{
							break;
						}
					}
				}
			}
		}

		_animation_speed = 1;
		float input_modifier = (_input_x != 0.0f && _input_y != 0.0f) ? 0.7071f : 1.0f;

		if(_controllable)
		{
			if(keyboardControl)
			{
				_input_x = Input.GetAxis("Horizontal");
				_input_y = Input.GetAxis("Vertical");

				/*
				 * Uncomment this code if you want to add a strafing axis
				_input_s = Input.GetAxis("Strafe");
				*/
			}
		}

		// If enabled, always rotate to look at the mouse position
		if(alwaysFaceMouse)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit))
			{
				_t.LookAt(new Vector3(hit.point.x, _t.position.y, hit.point.z));
			}
		}

		// If autorun is enabled, set Y input to always be 1 until user uses the Y axis
		if(_autorun)
		{
			if(_input_y == 0)
			{
				_input_y = 1;
			}
			else
			{
				_autorun = false;
			}
		}

		// If user is using strafe keys, override X axis
		if(_input_s != 0)
		{
			_input_x = _input_s;
		}

		// If the user is not holding right-mouse button, rotate the player with the X axis instead of strafing
		if(!Input.GetMouseButton(1) && !mouseLockMovement && _input_x != 0 && _input_s == 0)
		{
			_t.Rotate(new Vector3(0, _input_x * (turnSpeed / 2.0f), 0));
			_rotation = _input_x;
			_input_x = 0;
		}
		else
		{
			_rotation = 0;
		}

		// Movement direction and speed
		if(_input_y < 0)
		{
			_speed = backpedalSpeed;
		}
		else
		{
			if(_running)
			{
				_speed = runSpeed;
			}
			else
			{
				_speed = walkSpeed;
			}
		}

		if(clickToMove)
		{
			if(_last_wanted_position != _wanted_position)
			{
				float d = Vector3.Distance(_t.position, _wanted_position);

				if(d > _last_distance)
				{
					d = 0;
				}
				else
				{
					_last_distance = d;
				}

				if(d >= 0.1f)
				{
					_t.LookAt(new Vector3(_wanted_position.x, _t.position.y, _wanted_position.z));
					_input_y = Mathf.Clamp(d / 2f, 0, 1);
				}
				else
				{
					_last_wanted_position = _wanted_position;
					_input_y = 0;
				}
			}
		}

		// If on the ground, test to see if still on the ground and apply movement direction
		if(_grounded)
		{
			if(moveRelativeToCamera)
			{
				Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
				forward.y = 0f;
				forward = forward.normalized;
				Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

				float h = _input_x * input_modifier;
				float v = _input_y * input_modifier;

				Vector3 walkDirection = (h * right + v * forward);
				_velocity = walkDirection * _speed;
			}
			else
			{
				_velocity = new Vector3(_input_x * input_modifier, -antiBunny, _input_y * input_modifier);
				_velocity = _t.TransformDirection(_velocity) * _speed;
			}

			// Animation
			_move_speed = (_t.position - _last_position).magnitude;
			_last_position = _t.position;

			if(_move_speed > 0)
			{
				if(_move_speed > 0.07f)
				{
					if((Input.GetMouseButton(1) || mouseLockMovement) &&
					_input_x != 0)
					{
						if(_input_x < 0)
						{
							if(_input_y > 0)
							{
								_animator.Action = animationRunStrafeLeft45;
							}
							else
							{
								_animator.Action = animationRunStrafeLeft90;
							}
						}
						else
						{
							if(_input_y > 0)
							{
								_animator.Action = animationRunStrafeRight45;
							}
							else
							{
								_animator.Action = animationRunStrafeRight90;
							}
						}
					}
					else
					{
						_animator.Action = animationRun;
					}

					_animation_speed = 1;
				}
				else
				{
					if((Input.GetMouseButton(1) || mouseLockMovement) &&
					_input_x != 0)
					{
						if(_input_x < 0)
						{
							if(_input_y > 0)
							{
								_animator.Action = animationWalkStrafeLeft45;
							}
							else if(_input_y < 0)
							{
								_animator.Action = animationWalkStrafeRight45;
							}
							else
							{
								_animator.Action = animationWalkStrafeLeft90;
							}
						}
						else
						{
							if(_input_y > 0)
							{
								_animator.Action = animationWalkStrafeRight45;
							}
							else if(_input_y < 0)
							{
								_animator.Action = animationWalkStrafeLeft45;
							}
							else
							{
								_animator.Action = animationWalkStrafeRight90;
							}
						}
					}
					else
					{
						_animator.Action = animationWalk;
					}

					_animation_speed = _move_speed * 13 + 1;

					if(_input_y < 0)
					{
						_animation_speed = -_animation_speed;
					}
				}
			}
			else
			{
				if(_rotation < 0)
				{
					_animator.Action = animationTurnLeft;
				}
				else if(_rotation > 0)
				{
					_animator.Action = animationTurnRight;
				}
				else
				{
					_animator.Action = animationIdle;
				}
			}

			if(!Physics.Raycast(_t.position, -Vector3.up, 0.2f))
			{
				_grounded = false;
			}
		}
		else
		{
			if(_velocity.y > 0)
			{
				_animator.Action = animationJump;
			}
			else
			{
				_animator.Action = animationFall;
			}

			// Sets the falling start position to the highest point the player reaches
			if(_fall_start < _t.position.y)
			{
				_fall_start = _t.position.y;
			}
		}

		_velocity.y -= gravity * Time.deltaTime;
		_controller.Move(_velocity * Time.deltaTime);
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
			// If player falls too far, trigger falling damage
			if(_t.position.y < _fall_start - fallThreshold)
			{
				FallingDamage(_fall_start - fallThreshold - _t.position.y);
			}

			_fall_start = _t.position.y;

			_grounded = true;
			_velocity.y = 0;
		}
	}

	public void FallingDamage(float fall_distance)
	{
		Debug.Log("Fell " + fall_distance + " units.");
	}
}