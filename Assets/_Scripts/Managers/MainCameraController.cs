using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class MainCameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera = null;
        private float _zoomValue = 0.0f;
        private float _zoomSensitivity = 30.0f;

        private float _minHeight = 10.0f;
        private float _maxHeight = 300.0f;

        private Speed _zoomSpeed = new Speed { value = 8.0f, smoothFactor = 0.05f };
        private Speed _panningSpeed = new Speed { value = 30.0f, smoothFactor = 0.1f };
        private PanningLimit _panLimit = new PanningLimit { enabled = true, minPosition = new Vector2(-20.0f, -20.0f), maxPosition = new Vector2(300.0f, 300.0f) };
        private Vector3 _currPanDirection;
        private float _screenEdgePanningSize = 20.0f;
        private Vector3 _lastPanDirection = Vector3.zero;

        void Update()
        {
            UpdatePanInput();
            UpdateZoomInput();
        }

        private void FixedUpdate()
        {
            Zoom();
            Pan();
        }

        private void Pan()
        {
            //smoothly update the last panning direction towards the current one
            _lastPanDirection = Vector3.Lerp(_lastPanDirection, _currPanDirection, _panningSpeed.smoothFactor);

            //move the camera
            _mainCamera.transform.Translate(Quaternion.Euler(new Vector3(0f, _mainCamera.transform.eulerAngles.y, 0f)) * _lastPanDirection * _panningSpeed.value * Time.deltaTime, Space.World);


            //if there's a panning limit defined, clamp the camera's movement
            _mainCamera.transform.position = ApplyPanLimit(_mainCamera.transform.position);
        }

        private Vector3 ApplyPanLimit(Vector3 position)
        {
            return _panLimit.enabled
              ? new Vector3(
                  Mathf.Clamp(position.x, _panLimit.minPosition.x, _panLimit.maxPosition.x),
                  position.y,
                  Mathf.Clamp(position.z, _panLimit.minPosition.y, _panLimit.maxPosition.y))
              : position;
        }

        private void UpdatePanInput()
        {
            _currPanDirection = Vector3.zero;

            //if the pan on screen edge is enabled and we either are ignoring UI elements on the edge of the screen or the player's mouse is not over one


            //if the mouse is in either one of the 4 edges of the screen then move it accordinly  
            if (Input.mousePosition.x <= _screenEdgePanningSize && Input.mousePosition.x >= 0.0f)
                _currPanDirection.x = -1.0f;
            else if (Input.mousePosition.x >= Screen.width - _screenEdgePanningSize && Input.mousePosition.x <= Screen.width)
                _currPanDirection.x = 1.0f;

            if (Input.mousePosition.y <= _screenEdgePanningSize && Input.mousePosition.y >= 0.0f)
                _currPanDirection.z = -1.0f;
            else if (Input.mousePosition.y >= Screen.height - _screenEdgePanningSize && Input.mousePosition.y <= Screen.height)
                _currPanDirection.z = 1.0f;
        }

        private void UpdateZoomInput()
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                _zoomValue += Input.GetAxis("Mouse ScrollWheel") * _zoomSensitivity
                    * Time.deltaTime;
            }
        }

        private void Zoom()
        {
            _zoomValue = Mathf.Clamp01(_zoomValue);
            float targetHeight = Mathf.Lerp(_minHeight, _maxHeight, _zoomValue);

            _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position,
                new Vector3(
                    _mainCamera.transform.position.x
                    , targetHeight
                    , _mainCamera.transform.position.z)
                    , Time.deltaTime * _zoomSpeed.value);

        }
    }

    [System.Serializable]
    public struct Speed
    {
        public float value;
        public float smoothFactor;
    }

    public struct PanningLimit
    {
        public bool enabled;
        public Vector2 minPosition; //the minimum (x,z) values that the camera is allowed to have as its position
        public Vector2 maxPosition; //the maximum (x,z) values that the camera is allowed to have as its position
    }
}
