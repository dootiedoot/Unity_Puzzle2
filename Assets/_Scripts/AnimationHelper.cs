using UnityEngine;
using System.Collections;

public class AnimationHelper : MonoBehaviour
{
    Chick _chick;

    void Awake()
    {
        _chick = GetComponentInParent<Chick>();
    }

    //  General
    public void ProcessHopEvent()
    {
        _chick.StartCoroutine("Hop");
    }
    public void ProcessHopReset()
    {
        _chick.animator.SetBool("isHopping", false);
    }
}
