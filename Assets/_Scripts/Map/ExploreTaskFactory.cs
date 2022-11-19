using Assets._Scripts.Core.Task;
using Assets._Scripts.Drones;
using Assets._Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Assets._Scripts.Map
{
    public class ExploreTaskFactory : Task
    {
        private readonly MapTile _tile;
        private Drone _currentDrone;
        private Queue<Task> _subTasks = new Queue<Task>();

        public ExploreTaskFactory(MapTile tile)
        {
            this._tile = tile;


        }

        public override bool Valid => throw new NotImplementedException();

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override void Initialise()
        {
            var getIdleDroneTask = new DroneGetIdleTask();
            

            var moveTask = new DroneMoveTask(_tile);
            _subTasks.Enqueue(moveTask);        

            
        }

        public override bool IsFinished()
        {
            throw new NotImplementedException();
        }

       



    }
}
