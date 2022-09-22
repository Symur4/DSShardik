using Assets._Scripts.Core.Task;
using Assets._Scripts.Managers;
using Assets._Scripts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets._Scripts.Drones
{
    public class DroneMoveTask : Task
    {
        private Drone _currentDrone = null;
        private readonly MapTile _tile;

        public override bool Valid => throw new NotImplementedException();

        public DroneMoveTask(MapTile tile, Drone drone)
        {
            this._tile = tile;
            _currentDrone = drone;
        }

        public override void Execute()
        {
            _currentDrone.OnMoveFinished += OnMoveFinished;
            _currentDrone.Move(_tile);

        }

        private void OnMoveFinished()
        {            
            this._isFinished = true;
        }

        public override bool IsFinished()
        {
            return _isFinished;
        }

        public override void Initialise()
        {            
            if (_currentDrone == null)
            {
                _currentDrone = DroneManager.Instance.GetIdleDrone();
                this.IsInitialised = false;
                return;
            }

            _currentDrone.SetBusy();
            this.IsInitialised = true;
        }
    }
}
