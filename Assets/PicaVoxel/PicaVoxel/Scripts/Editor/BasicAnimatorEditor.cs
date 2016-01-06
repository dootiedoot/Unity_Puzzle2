/////////////////////////////////////////////////////////////////////////
// 
// PicaVoxel - The tiny voxel engine for Unity - http://picavoxel.com
// By Gareth Williams - @garethiw - http://gareth.pw
// 
// Source code distributed under standard Asset Store licence:
// http://unity3d.com/legal/as_terms
//
/////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace PicaVoxel
{

    [CustomEditor(typeof (BasicAnimator))]
    public class BasicAnimatorEditor : Editor
    {

        private bool playOnAwake;
        private bool pingPong;
        private bool randomStartFrame;
        private float interval;

        private BasicAnimator basicAnimator;

        private void OnEnable()
        {
            basicAnimator = (BasicAnimator) target;

            interval = basicAnimator.Interval;
            playOnAwake = basicAnimator.PlayOnAwake;
            pingPong = basicAnimator.PingPong;
            randomStartFrame = basicAnimator.RandomStartFrame;

        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            interval = EditorGUILayout.FloatField("Frame interval:", interval);
            if (interval != basicAnimator.Interval)
            {
                if (interval < 0f) basicAnimator.Interval = 0f;
                basicAnimator.Interval = interval;
            }
            pingPong = EditorGUILayout.ToggleLeft(new GUIContent(" Ping pong"), pingPong);
            if (pingPong != basicAnimator.PingPong) basicAnimator.PingPong = pingPong;
            randomStartFrame = EditorGUILayout.ToggleLeft(new GUIContent(" Random start frame"), randomStartFrame);
            if (randomStartFrame != basicAnimator.RandomStartFrame) basicAnimator.RandomStartFrame = randomStartFrame;
            playOnAwake = EditorGUILayout.ToggleLeft(new GUIContent(" Play on Awake"), playOnAwake);
            if (playOnAwake != basicAnimator.PlayOnAwake) basicAnimator.PlayOnAwake = playOnAwake;
        }
    }
}