using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SrpgcKeyboardMovementController))]
public class SrpgcKeyboardMovementEditor : Editor
{
	private SrpgcKeyboardMovementController _self;

	public override void OnInspectorGUI()
	{
		_self = (SrpgcKeyboardMovementController)target;

		_self.rootMotion = EditorGUILayout.Toggle("Use Root Motion", _self.rootMotion);

		if(_self.rootMotion)
		{
			_self.speedThreshold = EditorGUILayout.FloatField("Speed Threshold", _self.speedThreshold);
			_self.speedDamp = EditorGUILayout.FloatField("Speed Damp", _self.speedDamp);
			_self.directionDamp = EditorGUILayout.FloatField("Direction Damp", _self.directionDamp);
			_self.speedFloat = EditorGUILayout.TextField("Speed Float Name", _self.speedFloat);
			_self.directionFloat = EditorGUILayout.TextField("Direction Float Name", _self.directionFloat);
			_self.angleFloat = EditorGUILayout.TextField("Angle Float Name", _self.angleFloat);
		}
		else
		{
			_self.speedFloat = EditorGUILayout.TextField("Speed Float Name", _self.speedFloat);

			_self.keyStrafeLeft = (KeyCode)EditorGUILayout.EnumPopup("Key Strafe Left", _self.keyStrafeLeft);
			_self.keyStrafeRight = (KeyCode)EditorGUILayout.EnumPopup("Key Strafe Right", _self.keyStrafeRight);
			_self.walkSpeed = EditorGUILayout.FloatField("Walk Speed", _self.walkSpeed);
			_self.runSpeed = EditorGUILayout.FloatField("Run Speed", _self.runSpeed);
			_self.turnSpeed = EditorGUILayout.FloatField("Turn Speed", _self.turnSpeed);
			_self.jumpPower = EditorGUILayout.FloatField("Jump Power", _self.jumpPower);
			_self.gravity = EditorGUILayout.FloatField("Gravity", _self.gravity);
			_self.slopeLimit = EditorGUILayout.FloatField("Slope Limit", _self.slopeLimit);
			_self.antiBunny = EditorGUILayout.FloatField("Anti Bunny", _self.antiBunny);
		}

		if(GUI.changed)
		{
			EditorUtility.SetDirty(_self);
		}
	}
}