using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MovementRecorder : MonoBehaviour
{
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private bool record = false;
    private GameObjectRecorder _gameObjectRecorder;

    private void Start()
    {
        _gameObjectRecorder = new GameObjectRecorder(gameObject);
        _gameObjectRecorder.BindComponentsOfType<Transform>(gameObject,true);
    }

    private void LateUpdate()
    {
        if (animationClip == null)
        {
            return;
        }
        _gameObjectRecorder.TakeSnapshot(Time.deltaTime);
    }

    private void OnDisable()
    {
        if (animationClip == null)
        {
            return;
        }

        if (_gameObjectRecorder.isRecording)
        {
            _gameObjectRecorder.SaveToClip(animationClip);
        }
    }
}
