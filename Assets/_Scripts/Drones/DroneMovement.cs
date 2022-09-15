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
        private float _maxSpeed = 10f;
        private float _maxHeight = 5f;
        private Vector3 _targetPosition = Vector3.zero;
        private event Action OnMoveFinished;

        private void Update()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {            
            if(_targetPosition == Vector3.zero)
            {
                return;
            }

            if(_targetPosition.x != this.transform.position.x
                || _targetPosition.y != this.transform.position.y)
            {
                float step = _maxSpeed * Time.deltaTime;

                this.transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

                if (this.transform.position == _targetPosition)
                {
                    _targetPosition = Vector3.zero;
                    if(OnMoveFinished != null)
                    {
                        OnMoveFinished();
                    }
                }

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

        public void Move(Vector3 pos, Action onMoveFinished)
        {
            this.OnMoveFinished = onMoveFinished;
            _targetPosition = pos;
        }
    }
}
