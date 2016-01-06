using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////
// Joystick.js
// Penelope iPhone Tutorial
//
// Joystick creates a movable joystick (via GUITexture) that 
// handles touch input, taps, and phases. Dead zones can control
// where the joystick input gets picked up and can be normalized.
//
// Optionally, you can enable the touchPad property from the editor
// to treat this Joystick as a TouchPad. A TouchPad allows the finger
// to touch down at any point and it tracks the movement relatively 
// without moving the graphic
//////////////////////////////////////////////////////////////

// Converted to C# from Javascript by Austin Zimmer

[RequireComponent(typeof(GUITexture))]
public class SrpgcJoystick : MonoBehaviour
{
	private struct Boundary
	{
		public Vector2 min;
		public Vector2 max;
	}

	public SimpleRpgCamera rpgCamera;

	public Animator animator;
	public bool instantRotation = false;

	public bool touchPad = false;
	public Rect touchZone;
	public Vector2 deadZone = Vector2.zero;
	public bool normalize = false;
	public Vector2 position = Vector2.zero;
	public int tapCount = 0;

	static private SrpgcJoystick[] joysticks;
	static private bool enumeratedJoysticks = false;
	static private float tapTimeDelta = 0.3f;

	private int lastFingerId = -1;
	private float tapTimeWindow;
	private Vector2 fingerDownPos;

	private GUITexture gui;
	private Rect defaultRect;
	private Boundary guiBoundary = new Boundary();
	private Vector2 guiTouchOffset;
	private Vector2 guiCenter;

	void Awake()
	{
		if(!rpgCamera)
		{
			rpgCamera = Camera.main.GetComponent<SimpleRpgCamera>();
		}
	}

	void Start()
	{
		gui = GetComponent<GUITexture>();

		defaultRect = gui.pixelInset;

		defaultRect.x += transform.position.x * Screen.width;// + gui.pixelInset.x; // -  Screen.width * 0.5;
		defaultRect.y += transform.position.y * Screen.height;// - Screen.height * 0.5;

		Vector3 position = new Vector3();
		transform.position = position;

		if(touchPad)
		{
			// If a texture has been assigned, then use the rect ferom the gui as our touchZone
			if(gui.texture)
				touchZone = defaultRect;
		}
		else
		{
			// This is an offset for touch input to match with the top left
			// corner of the GUI
			guiTouchOffset.x = defaultRect.width * 0.5f;
			guiTouchOffset.y = defaultRect.height * 0.5f;

			// Cache the center of the GUI, since it doesn't change
			guiCenter.x = defaultRect.x + guiTouchOffset.x;
			guiCenter.y = defaultRect.y + guiTouchOffset.y;

			// Let's build the GUI boundary, so we can clamp joystick movement
			guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
			guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
			guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
			guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
		}
	}

	private void Disable()
	{
		gameObject.SetActive(false);
		enumeratedJoysticks = false;
	}

	private void ResetJoystick()
	{
		// Release the finger control and set the joystick back to the default position
		gui.pixelInset = defaultRect;
		lastFingerId = -1;
		position = Vector2.zero;
		fingerDownPos = Vector2.zero;

		if(touchPad)
		{
			Color c = gui.color;
			c.a = 0.025f;
			gui.color = c;
		}
	}

	private bool IsFingerDown()
	{
		return (lastFingerId != -1);
	}

	private void LatchedFinger(int fingerId)
	{
		// If another joystick has latched this finger, then we must release it
		if(lastFingerId == fingerId)
			ResetJoystick();
	}

	void Update()
	{
		if(!enumeratedJoysticks)
		{
			// Collect all joysticks in the game, so we can relay finger latching messages
			joysticks = (SrpgcJoystick[])FindObjectsOfType(typeof(SrpgcJoystick));
			enumeratedJoysticks = true;
		}

		var count = Input.touchCount;

		// Adjust the tap time window while it still available
		if(tapTimeWindow > 0)
			tapTimeWindow -= Time.deltaTime;
		else
			tapCount = 0;

		if(count == 0)
			ResetJoystick();
		else
		{
			for(int i = 0; i < count; i++)
			{
				Touch touch = Input.GetTouch(i);
				Vector2 guiTouchPos = touch.position - guiTouchOffset;

				var shouldLatchFinger = false;
				if(touchPad)
				{
					if(touchZone.Contains(touch.position))
						shouldLatchFinger = true;
				}
				else if(gui.HitTest(touch.position))
				{
					shouldLatchFinger = true;
				}

				// Latch the finger if this is a new touch
				if(shouldLatchFinger && (lastFingerId == -1 || lastFingerId != touch.fingerId))
				{

					if(touchPad)
					{
						Color c = gui.color;
						c.a = 0.15f;
						gui.color = c;

						lastFingerId = touch.fingerId;
						fingerDownPos = touch.position;
					}

					lastFingerId = touch.fingerId;

					// Accumulate taps if it is within the time window
					if(tapTimeWindow > 0)
						tapCount++;
					else
					{
						tapCount = 1;
						tapTimeWindow = tapTimeDelta;
					}

					// Tell other joysticks we've latched this finger
					foreach(SrpgcJoystick j in joysticks)
					{
						if(j != this)
							j.LatchedFinger(touch.fingerId);
					}
				}

				if(lastFingerId == touch.fingerId)
				{
					// Override the tap count with what the iPhone SDK reports if it is greater
					// This is a workaround, since the iPhone SDK does not currently track taps
					// for multiple touches
					if(touch.tapCount > tapCount)
						tapCount = touch.tapCount;

					if(touchPad)
					{
						// For a touchpad, let's just set the position directly based on distance from initial touchdown
						position.x = Mathf.Clamp((touch.position.x - fingerDownPos.x) / (touchZone.width / 2), -1, 1);
						position.y = Mathf.Clamp((touch.position.y - fingerDownPos.y) / (touchZone.height / 2), -1, 1);
					}
					else
					{
						// Change the location of the joystick graphic to match where the touch is
						Rect inset = gui.pixelInset;
						inset.x = Mathf.Clamp(guiTouchPos.x, guiBoundary.min.x, guiBoundary.max.x);
						inset.y = Mathf.Clamp(guiTouchPos.y, guiBoundary.min.y, guiBoundary.max.y);
						gui.pixelInset = inset;

						if(rpgCamera)
						{
							rpgCamera.Controllable = false;
						}
					}

					if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
					{
						ResetJoystick();

						if(rpgCamera)
						{
							rpgCamera.Controllable = true;
						}
					}
				}
			}
		}

		if(!touchPad)
		{
			// Get a value between -1 and 1 based on the joystick graphic location
			position.x = (gui.pixelInset.x + guiTouchOffset.x - guiCenter.x) / guiTouchOffset.x;
			position.y = (gui.pixelInset.y + guiTouchOffset.y - guiCenter.y) / guiTouchOffset.y;
		}

		// Adjust for dead zone	
		var absoluteX = Mathf.Abs(position.x);
		var absoluteY = Mathf.Abs(position.y);

		if(absoluteX < deadZone.x)
		{
			// Report the joystick as being at the center if it is within the dead zone
			position.x = 0;
		}
		else if(normalize)
		{
			// Rescale the output after taking the dead zone into account
			position.x = Mathf.Sign(position.x) * (absoluteX - deadZone.x) / (1 - deadZone.x);
		}

		if(absoluteY < deadZone.y)
		{
			// Report the joystick as being at the center if it is within the dead zone
			position.y = 0;
		}
		else if(normalize)
		{
			// Rescale the output after taking the dead zone into account
			position.y = Mathf.Sign(position.y) * (absoluteY - deadZone.y) / (1 - deadZone.y);
		}

		if(animator)
		{
			animator.SetFloat("Speed", position.sqrMagnitude);
			animator.SetFloat("Direction", 0);

			if(!instantRotation)
			{
				animator.SetFloat("Direction", StickToWorldspace());
			}
			else
			{
				if(position != Vector2.zero)
				{
					float playerAngle = Mathf.Atan2(position.x, position.y) * Mathf.Rad2Deg;
					float cameraAngle = 0;

					if(rpgCamera)
					{
						cameraAngle = rpgCamera.transform.eulerAngles.y;
					}

					animator.transform.rotation = Quaternion.Euler(0, playerAngle + cameraAngle, 0);
				}
			}
		}
	}

	public float StickToWorldspace()
	{
		if(rpgCamera)
		{
			Transform t = animator.transform;

			Vector3 rootDirection = t.forward;

			// Get camera direction
			Vector3 cameraDirection = rpgCamera.transform.forward;
			cameraDirection.y = 0;
			Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

			// Convert joystick input in worldspace coords
			Vector3 moveDirection = referentialShift * new Vector3(position.x, 0, position.y);
			Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

			float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

			angleRootToMove /= 180f;
			return angleRootToMove;
		}

		return 0;
	}
}