using Assets._Scripts.Core.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts.Buildings
{
    public class BuildTask : Task
    {

        public override bool Valid
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Initialise()
        {
            this.Initialised = true;
        }

        //Execute() needs to be called in update of the TaskManager.
        public override void Execute()
        {

        }

        public override bool Finished()
        {
            throw new NotImplementedException();
        }

    }
}
