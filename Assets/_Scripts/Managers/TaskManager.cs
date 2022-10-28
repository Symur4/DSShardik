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
        private static readonly object _lock = new object();
        private List<Task> _taskList;
        private long taskCounter = 0;

        private bool _paused = false;

        void Awake()
        {
            _taskList = new List<Task>();
        }
        private void Update()
        {            
            if (!_paused)
            {
                InitializeTasks();

                ExecuteTasks();

                RemoveFinishedTasks();
            }
        }

        private void RemoveFinishedTasks()
        {
            lock (_lock)
            {
                _taskList.RemoveAll(w => w.IsFinished() == true);
            }
        }

        private void ExecuteTasks()
        {
            foreach (var t in _taskList.Where(w => w.IsInitialised == true
                                                && w.Started == false ))
            {
                t.Execute();
            }
        }

        void InitializeTasks()
        {
            lock (_lock)
            {
                for (int i = 0; i < _taskList.Count(); i++)
                {
                    if (_taskList[i].IsInitialised == false
                        && _taskList[i].Started == false)
                    {
                        _taskList[i].Initialise();
                    }
                }
            }           
        }

        public  void AddTask(Task newTask)
        {
            lock (_lock)
            {
                newTask.TaskID = taskCounter++;
                Debug.Log("Task added:" + newTask.GetType() + " id:"  + newTask.TaskID);
            
                _taskList.Add(newTask);
            }
        }
    }
}
