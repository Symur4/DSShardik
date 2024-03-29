﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Core.Task
{
    //https://forum.unity.com/threads/task-based-ai-system-for-all.326401/
    public abstract class Task
    {
        //Returns true if the conditions declared by the Task are met.
        public abstract bool Valid { get; }

        //Returns true if the Task has had Initialise() called.
        public bool IsInitialised { get; set; }

        //Returns True if the Task has been started.
        public bool Started { get; set; }

        //Used for sorting Tasks.
        public int Priority { get; set; }
        public long TaskID { get; set; }

        public DateTime Created { get; set; }

        //Constructor.
        public Task()
        {
            IsInitialised = false;
            Started = false;
            Created = DateTime.Now;
        }

        //To be called before Execute or the Task will (probably) fail, depending on whether the task needs to initialise at all.
        public abstract void Initialise();

        //OnTaskStart() is called when the task is Valid AND Initialised!
        public virtual void OnTaskStart() { }

        //Execute() needs to be called in update of the TaskManager. This will probably hold the majority of the game logic for a task.
        //It is executed every update of the TaskManager, use this function as if you were to use Update();
        public abstract void Execute();

        //Allows the TaskManager to check if a task has finished, each task defines it's own rules as to what finished means.
        public abstract bool IsFinished();
        protected bool _isFinished = false;

        //OnTaskEnd() is called after the Task decides or is told it is finished.
        public virtual void OnTaskEnd() { }

        public virtual void Reset()
        {
            IsInitialised = false;
            Started = false;            
        }

    }
}
