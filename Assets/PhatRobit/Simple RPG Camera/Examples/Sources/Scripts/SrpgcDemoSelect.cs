using UnityEngine;
using System.Collections;

public class SrpgcDemoSelect : MonoBehaviour
{
	public string[] demoScenes;
	public GUISkin skin;
	public GUISkin mobileSkin;

	private Rect _selectRect;

	void Start()
	{
		_selectRect = new Rect(Screen.width / 2f - 100, Screen.height / 2f - 50, 200, 32);
	}

	void OnGUI()
	{
		if(skin && GUI.skin != skin)
		{
			GUI.skin = skin;
		}

		if(mobileSkin && Application.isMobilePlatform)
		{
			GUI.skin = mobileSkin;
		}

		_selectRect = GUILayout.Window(0, _selectRect, SelectWindow, "Select A Demo");
		_selectRect.x = Screen.width / 2f - _selectRect.width / 2f;
		_selectRect.y = Screen.height / 2f - _selectRect.height / 2f;
	}

	private void SelectWindow(int id)
	{
		foreach(string scene in demoScenes)
		{
			if(GUILayout.Button(scene))
			{
				Application.LoadLevel(scene);
			}
		}
	}
}