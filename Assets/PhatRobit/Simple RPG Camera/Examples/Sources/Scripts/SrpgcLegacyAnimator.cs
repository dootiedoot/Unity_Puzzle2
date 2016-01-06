using UnityEngine;
using System.Collections;

public class SrpgcLegacyAnimator : MonoBehaviour
{
	public GameObject model;

	private bool _active = true;
	private string _action = string.Empty;
	private string _animation = string.Empty;

	private Animation _modelAnimation;

	public string Action
	{
		get { return _action; }
		set { _action = value; }
	}

	void Start()
	{
		// Check to make sure the model is selected and has animation
		if(!model)
		{
			Debug.LogWarning("SrpgcLegacyAnimator: No model selected");
			_active = false;
		}
		else
		{
			_modelAnimation = model.GetComponent<Animation>();

			if(!_modelAnimation)
			{
				Debug.LogWarning("SrpgcLegacyAnimator: Selected model has no animation");
				_active = false;
			}
		}
	}
	
	void Update()
	{
		if(_active)
		{
			// CrossFade the animation to match the action
			if(_animation != _action)
			{
				_animation = _action;
				_modelAnimation.CrossFade(_animation);
			}
		}
	}

	public void SetSpeed(float n)
	{
		if(_active)
		{
			// Set the current animation's speed
			if(_modelAnimation[_animation])
			{
				if(_modelAnimation[_animation].speed != n)
				{
					_modelAnimation[_animation].speed = n;
				}
			}
		}
	}
}