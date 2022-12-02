using Assets._Scripts.Managers;
using Assets._Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Drones
{
    [RequireComponent(typeof(Drone))]

    public class DroneMovement : MonoBehaviour
    {
        private float _maxSpeed = 20f;
        private float _maxHeight = 5f;
        private Vector3? _targetPosition = null;
        private Drone _drone;


        private void Start()
        {
            _drone = GetComponent<Drone>();
        }
        public Vector3? TargetPosition => _targetPosition; 
        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {            
            if(_targetPosition == null)
            {
                return;
            }
            var targetPosition = (Vector3)_targetPosition;

            if(targetPosition.x != this.transform.position.x
                || targetPosition.y != this.transform.position.y)
            {
                float step = _maxSpeed * Time.deltaTime;
                _drone.IsMoving = true;

                this.transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);                
            }

            if (this.transform.position == targetPosition)
            {
                _targetPosition = null;
                Debug.Log("DroneMovement finished id:" + _drone.Id);
                TmpEventManager.Instance.TriggerEvent(nameof(EventName.DronMovementComplete),
                    new Dictionary<string, object>() { { "id", _drone.Id } });
                _drone.IsMoving = false;
            }
        }

        public void Init(float maxSpeed, float maxHeight)
        {
            _maxHeight = maxHeight;
            _maxSpeed = maxSpeed;
        }

        public void Move(Vector3 pos)
        {
            _targetPosition = pos;            
        }        
    }
}
