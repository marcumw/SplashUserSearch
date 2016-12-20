using System;
using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Layers;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;

public sealed class ManagerTouch
{
    #region variables
    public static bool _isDragging = false;
    public static bool _holding = false;
    public static bool _isSmoothing = false;

    private static Tween _tweenHoldCheck;

    private float _smoothTime = .3f;
    private float _x = 0f, _y = 0f;

    private float _xDelta = 0f, _yDelta = 0f;
    public float _xDeltaSmooth = 0f, _yDeltaSmooth = 0f;

    public float _xVelocity = 0f, _yVelocity = 0f;
    public float _xVelocityDelta = 0f, _yVelocityDelta = 0f;

    private float _smoothStopFactor = 0.5f;

    private float _panThresholdX = 0.0f;
    private float _panThresholdY = 0.0f;

    public ITouch _currTouchBegin;
    private Vector2 _currTouchBeginPos;
    private Vector2 _currTouchEndPos;

    private Vector2 _screenPosition;
    private Vector2 _screenPositionPrev;

    private int _currTouchCount = 0;

    private static Vector2 _currTouchVector;
    private static float _currTouchDuration;
    private static float _currTouchDurationDoubleTap;

    private static Ray _currRay;
    public static RaycastHit _currRayCastHit;
    public static Vector3 _screenPositionRayCastPoint;

    private bool _rotateCamera = true;
    private bool _rotateTower = false;

    private static float _rotateFactorTower = .1f;
    private static float _rotateFactorCamera = .005f;
    private static float _rotateFactorCameraZoomed = .001f;

    private static float _rotateFactor;
    private static float _rotateFactorCenter = .005f;
    private static float _rotateFactorOuter = .2f;
    #endregion

    public ManagerTouch()
    {
        TouchManager.Instance.TouchesBegan += onTouchManagerBegin;
        TouchManager.Instance.TouchesEnded += onTouchManagerEnd;
        TouchManager.Instance.TouchesMoved += onTouchManagerMoved;
    }

    #region TouchManager logic
    private void onTouchManagerBegin(object sender, EventArgs e)
    {

        _currTouchBegin = ((TouchEventArgs)e).Touches[0];

        _screenPosition = _screenPositionPrev = _currTouchBeginPos = _currTouchBegin.Position;

        _currTouchDuration = Time.time;

        if (_currTouchCount == 0)
            _currTouchDurationDoubleTap = _currTouchDuration;


        //menuAtlasesHandler();
    }

    private void onTouchManagerMoved(object sender, EventArgs e)
    {
        _screenPosition = ((TouchEventArgs)e).Touches[0].Position;

        //horizontal scrolling
        if (Math.Abs(_screenPosition.x - _screenPositionPrev.x) > _panThresholdX)
            _isDragging = true;

        //vertical scrolling
        if (Math.Abs(_screenPosition.y - _screenPositionPrev.y) > _panThresholdY)
            _isDragging = true;


        if (_isDragging)
            _currTouchCount = 0;
    }

    private bool isDoubleTap()
    {
        bool isDoubleTap = false;

        if (_currTouchCount == 1)
        {
            Global.instance.onDelayedCall(.4f, resetTapCount);
        }

        if (_currTouchCount == 2)
        {
            _currTouchDurationDoubleTap -= Time.time;

            if (_currTouchDurationDoubleTap > -.4f)
            {
                isDoubleTap = true;
            
                onDoubleTap();
            }

            _currTouchCount = 0;
        }

        return isDoubleTap;
    }

    private void resetTapCount()
    {
        if (_currTouchCount == 1)
        {
            //above conditions then this
            onSingleTap();
        }

        _currTouchCount = 0;
        //Global._Global._managerGui._debug.text = "reset tap count: " + Time.time;
    }

    private void onTouchManagerEnd(object sender, EventArgs e)
    {
        _currTouchCount++;

        _currTouchEndPos = ((TouchEventArgs)e).Touches[0].Position;
        _currTouchDuration -= Time.time;
        _currTouchVector = _currTouchBeginPos - _currTouchEndPos;

        isDoubleTap();

        _isDragging = false;
    }

    private void onSingleTap()
    {
        if (_currTouchBegin != null)
        {
            if (_currTouchBegin.Target == null)
            {
                return;
            }
        }

        if (Mathf.Abs(_xVelocity) > .01
                || Mathf.Abs(_currTouchVector.x) > _panThresholdX
                || Mathf.Abs(_currTouchVector.y) > _panThresholdX)
        {

            return;
        }
    }

    public void onDoubleTap()
    {

    }
    #endregion

    #region Dragging / Smoothing
    public void update()
    {
        if (_isDragging)
        {
            onDrag();
        }
        else
        {
            if (_isSmoothing)
            {
                onDragEnded();
            }
        }
    }

    private void onDrag()
    {
        _xDelta = (_screenPosition.x - _screenPositionPrev.x) * _rotateFactor;
        _yDelta = (_screenPosition.y - _screenPositionPrev.y) * _rotateFactorOuter;

        _x += _xDelta;
        _y -= _yDelta;

        _screenPositionPrev = _screenPosition;

        Smooth();
    }

    private void onDragEnded()
    {
        _xDelta *= _smoothStopFactor;
        _yDelta *= _smoothStopFactor;

        _x += _xDelta;
        _y -= _yDelta;

        if (_isSmoothing && Mathf.Abs(_xVelocityDelta) <= .05f && Mathf.Abs(_yVelocityDelta) <= .05f)
        {
            _isSmoothing = false;
        }

        if (_isSmoothing)
            Smooth();
    }

    private void Smooth()
    {
        _xDeltaSmooth = Mathf.SmoothDamp(_xDeltaSmooth, _xDelta, ref _xVelocityDelta, _smoothTime);
        _yDeltaSmooth = Mathf.SmoothDamp(_yDeltaSmooth, _yDelta, ref _yVelocityDelta, _smoothTime);

        Global.instance.ManagerUsers.scroll(_yDeltaSmooth);

        if (!_isSmoothing)
            _isSmoothing = true;
    }

    public void resetTouchVars()
    {
        _xDelta = _yDelta = 0;
        _xDeltaSmooth = _yDeltaSmooth = 0;

        _xVelocity = _yVelocity = 0;
        _xVelocityDelta = _yVelocityDelta = 0;

        _isSmoothing = false;
        _isDragging = false;
    }
    #endregion
}