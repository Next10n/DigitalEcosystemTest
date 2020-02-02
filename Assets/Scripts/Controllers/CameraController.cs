using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraMaxFieldOfView;
    [SerializeField] private float _cameraMinFieldOfView;
    [SerializeField] private float _cameraXminPos;
    [SerializeField] private float _cameraXmaxPos;
    [SerializeField] private float _cameraZminPos;
    [SerializeField] private float _cameraZmaxPos;
    private float _minDeltaTouchToMove = 10;
    private TimeController _timeController;
    private Transform _target;
    private float _cameraToObjectMinDistance = 1.5f;
    private Camera MainCamera;

    private void Awake()
    {
        _timeController = GameObject.Find("MainController").GetComponent<TimeController>();
        MainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if (!_timeController.IsPause())
        {
            if (Input.touchCount == 1)
            {
                CameraMoveTo(Input.touches[0].deltaPosition);
            }

            if (Input.touchCount == 2)
            {
                if (DistanceBetweenTouches() > DistanceBetweenLastTouches())
                {
                    CameraZoomIn();
                }
                if (DistanceBetweenTouches() < DistanceBetweenLastTouches())
                {
                    CameraZoomOut();
                }
            }
            if(_target != null) //тяжелая проверка
            {
                ZoomTarget();
            }
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void ZoomTarget()
    {

        if(Vector3.Distance(transform.position, _target.position) > _cameraToObjectMinDistance)
        {
            MainCamera.transform.position = Vector3.MoveTowards(MainCamera.transform.position,
                new Vector3(_target.position.x, MainCamera.transform.position.y, _target.position.z),
                Time.deltaTime * 1);
            CameraZoomIn();
        }
        else if (Mathf.Abs(transform.position.x - _target.position.x) > 0)
        {
            MainCamera.transform.position = Vector3.MoveTowards(MainCamera.transform.position,
                new Vector3(_target.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z),
                Time.deltaTime * 1);
        }
        else
            _target = null;
    }

    private void CameraMoveTo(Vector2 deltaPosition)
    {
        if(deltaPosition.magnitude > _minDeltaTouchToMove)
        {
            MainCamera.transform.position = Vector3.MoveTowards(MainCamera.transform.position, new Vector3(MainCamera.transform.position.x - deltaPosition.x,
                MainCamera.transform.position.y,
                MainCamera.transform.position.z - deltaPosition.y), Time.deltaTime * deltaPosition.magnitude / 4);
            CameraBorder();
        }

    }

    private void CameraBorder()
    {
        if (MainCamera.transform.position.x > _cameraXmaxPos)
            MainCamera.transform.position = new Vector3(_cameraXmaxPos, MainCamera.transform.position.y, MainCamera.transform.position.z);
        if (MainCamera.transform.position.x < _cameraXminPos)
            MainCamera.transform.position = new Vector3(_cameraXminPos, MainCamera.transform.position.y, MainCamera.transform.position.z);
        if (MainCamera.transform.position.z < _cameraZminPos)
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.z, MainCamera.transform.position.y, _cameraZminPos);
        if (MainCamera.transform.position.z > _cameraZmaxPos)
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.z, MainCamera.transform.position.y, _cameraZmaxPos);
    }

    private void CameraZoomIn()
    {
        if (MainCamera.fieldOfView > _cameraMinFieldOfView)
        {
            MainCamera.fieldOfView--;
        }
    }

    private void CameraZoomOut()
    {
        if (MainCamera.fieldOfView < _cameraMaxFieldOfView)
        {
            MainCamera.fieldOfView++;
        }
    }

    private float DistanceBetweenTouches()
    {
        if (Input.touchCount == 2)
        {
            return Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
        }
        else
        {
            return 0;
        }
    }

    private float DistanceBetweenLastTouches()
    {
        if (Input.touchCount == 2)
        {
            return Vector2.Distance(GetLastTouchPosition(Input.touches[0].position, Input.touches[0].deltaPosition),
                                    GetLastTouchPosition(Input.touches[1].position, Input.touches[1].deltaPosition));
        }
        else
        {
            return 0;
        }
    }

    private Vector2 GetLastTouchPosition(Vector2 position, Vector2 deltaPosotion)
    {
        return new Vector2(position.x - deltaPosotion.x, position.y - deltaPosotion.y);
    }

}
