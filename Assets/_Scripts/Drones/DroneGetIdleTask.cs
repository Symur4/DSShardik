using Assets._Scripts.Core.Task;
using Assets._Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets._Scripts.Drones
{
    public class DroneGetIdleTask : Task
    {
        private Drone _idleDrone;

        public DroneGetIdleTask()
        {
        }

        public override bool Valid => throw new NotImplementedException();

        public override void Execute()
        {
            _idleDrone = DroneManager.Instance.GetIdleDrone();
            if( _idleDrone != null )
            {
                this.Started = true;
            }
        }

        public override void Initialise()
        {
            this.IsInitialised = true;            
        }

        public override bool IsFinished()
        {
            if( _idleDrone != null )
            {
                _isFinished = true;            
            }

            return _isFinished;
        }

        public Drone GetIdelDrone()
        {
            return _idleDrone;
        }        
    }
}
