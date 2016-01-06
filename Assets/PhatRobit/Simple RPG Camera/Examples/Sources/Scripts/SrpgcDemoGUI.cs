using UnityEngine;
using System.Collections;

public class SrpgcDemoGUI : MonoBehaviour
{
	public SimpleRpgCamera rpgCamera;
	public string demoSelectScene = string.Empty;
	public GUISkin skin;
	public GUISkin mobileSkin;

	private Rect _window_rect;
	private string _version = "2.1.0";

	void Start()
	{
		_window_rect = new Rect(10, 10, 200, 32);
	}

	void OnGUI()
	{
		_window_rect = GUILayout.Window(0, _window_rect, DemoWindow, "Simple RPG Camera Demo");

		bool enableMouse = !_window_rect.Contains(Event.current.mousePosition);

		if(rpgCamera)
		{
			rpgCamera.Controllable = enableMouse;
		}
	}

	private void DemoWindow(int id)
	{
		if(skin && GUI.skin != skin)
		{
			GUI.skin = skin;
		}

		if(mobileSkin && Application.isMobilePlatform)
		{
			GUI.skin = mobileSkin;
		}

		GUILayout.Label("v" + _version);

		if(demoSelectScene != string.Empty)
		{
			if(GUILayout.Button("Return To Demo Selection"))
			{
				Application.LoadLevel(demoSelectScene);
			}
		}

		if(rpgCamera)
		{
			rpgCamera.useTargetAxis = GUILayout.Toggle(rpgCamera.useTargetAxis, "Use Target Axis");
			rpgCamera.lockToTarget = GUILayout.Toggle(rpgCamera.lockToTarget, "Lock To Target");

			rpgCamera.allowEdgeMovement = GUILayout.Toggle(rpgCamera.allowEdgeMovement, "Allow Edge Movement");
			rpgCamera.allowEdgeKeys = GUILayout.Toggle(rpgCamera.allowEdgeKeys, "Allow Key Movement");

			rpgCamera.allowRotation = GUILayout.Toggle(rpgCamera.allowRotation, "Allow Rotation");

			if(rpgCamera.allowRotation)
			{
				rpgCamera.mouseLook = GUILayout.Toggle(rpgCamera.mouseLook, "Mouse Look");

				if(rpgCamera.mouseLook)
				{
					rpgCamera.lockCursor = GUILayout.Toggle(rpgCamera.lockCursor, "Lock Cursor");
					rpgCamera.disableWhileUnlocked = GUILayout.Toggle(rpgCamera.disableWhileUnlocked, "Disable While Unlocked");
				}
				else
				{
					rpgCamera.allowRotationLeft = GUILayout.Toggle(rpgCamera.allowRotationLeft, "Allow Left Button");
					rpgCamera.allowRotationMiddle = GUILayout.Toggle(rpgCamera.allowRotationMiddle, "Allow Middle Button");
					rpgCamera.allowRotationRight = GUILayout.Toggle(rpgCamera.allowRotationRight, "Allow Right Button");

					if(rpgCamera.allowRotationLeft)
					{
						rpgCamera.lockLeft = GUILayout.Toggle(rpgCamera.lockLeft, "Lock Left Button");
					}

					if(rpgCamera.allowRotationMiddle)
					{
						rpgCamera.lockMiddle = GUILayout.Toggle(rpgCamera.lockMiddle, "Lock Middle Button");
					}

					if(rpgCamera.allowRotationRight)
					{
						rpgCamera.lockRight = GUILayout.Toggle(rpgCamera.lockRight, "Lock Right Button");
					}
				}

				rpgCamera.stayBehindTarget = GUILayout.Toggle(rpgCamera.stayBehindTarget, "Stay Behind Target");
				rpgCamera.returnToOrigin = GUILayout.Toggle(rpgCamera.returnToOrigin, "Return To Origin");
				rpgCamera.invertRotationX = GUILayout.Toggle(rpgCamera.invertRotationX, "Invert X");
				rpgCamera.invertRotationY = GUILayout.Toggle(rpgCamera.invertRotationY, "Invert Y");
				rpgCamera.rotateObjects = GUILayout.Toggle(rpgCamera.rotateObjects, "Rotate Objects");
			}

			rpgCamera.fadeObjects = GUILayout.Toggle(rpgCamera.fadeObjects, "Fade Objects [" + rpgCamera.fadeDistance + "]");

			if(rpgCamera.fadeObjects)
			{
				rpgCamera.fadeDistance = GUILayout.HorizontalSlider(rpgCamera.fadeDistance, rpgCamera.minDistance, rpgCamera.maxDistance);
			}

			GUILayout.Label("Target Offset\n[X:" + rpgCamera.targetOffset.x.ToString("F2") + ", Y:" + rpgCamera.targetOffset.y.ToString("F2") + ", Z:" + rpgCamera.targetOffset.z.ToString("F2") + "]");
			rpgCamera.targetOffset.x = GUILayout.HorizontalSlider(rpgCamera.targetOffset.x, -2, 2);
			rpgCamera.targetOffset.y = GUILayout.HorizontalSlider(rpgCamera.targetOffset.y, 0, 2);
			rpgCamera.targetOffset.z = GUILayout.HorizontalSlider(rpgCamera.targetOffset.z, -2, 2);
		}
	}
}