using Assets._Scripts.Core.Task;
using Assets._Scripts.Map;
using Assets._Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts.Drones
{
    public class DroneTransferResourceTask : Task
    {
        private readonly Drone _currentDrone;
        private readonly MapTile _tile;
        private readonly List<ResourceData> _resources;

        public DroneTransferResourceTask(Drone drone,
            MapTile tile,
            List<ResourceData> resources)
        {
            this._currentDrone = drone;
            this._tile = tile;
            this._resources = resources;

        }

        public override bool Valid => throw new NotImplementedException();

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void Initialise()
        {
            
        }

        public override bool IsFinished()
        {
            throw new NotImplementedException();
        }
    }
}
