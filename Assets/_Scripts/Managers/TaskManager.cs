using Assets._Scripts.Core.Task;
using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class TaskManager : Singleton<TaskManager>
    {
        private List<Task> _taskList;

        private bool _paused = false;

        void Awake()
        {
            _taskList = new List<Task>();
        }
        void Update()
        {
            if (!_paused)
            {
                if (_taskList.Count > 0)
                {
                    //StartCoroutine("UpdateTimedTaskCounters"); << I'm not completely sure if this is necessary, possibly with hundreds of objects in the scene?                    
                    ProcessList();
                }
                else
                {
                   // Debug.Log("TaskManager - TaskList is empty!");
                }
            }
        }

        void ProcessList()
        {

            //If this Task decides it is invalid, then delete it.
            if (_taskList[0].Valid)
            {
                //If its not initialised, intialise it.
                if (_taskList[0].Initialised)
                {
                    //If the task isn't finished, execute it.
                    if (!_taskList[0].Finished())
                    {
                        if (_taskList[0].Started == false)
                        {
                            _taskList[0].Started = true;
                            _taskList[0].OnTaskStart();
                        }
                        _taskList[0].Execute();
                    }
                    else if (_taskList[0].Finished())
                    {
                        Debug.Log("TaskManager - Task finished, removing!");
                        //Call OnTaskEnd() and then remove the task.
                        _taskList[0].OnTaskEnd();
                        _taskList.RemoveAt(0);
                        //If PrioritySort is on, sort the list now, before we start the next task.
                        //if (PrioritySort)
                        //{
                        //    SortListByPriority();
                        //}
                    }
                }
                else
                {
                    _taskList[0].Initialise();
                }
            }
            else if (!_taskList[0].Valid)
            {
                Debug.LogWarning("TaskManager - Invalid Task detected, removing!");
                _taskList.RemoveAt(0);
            }
        }
    }
}
