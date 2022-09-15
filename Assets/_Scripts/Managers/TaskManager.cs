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
                InitializeTasks();

                ExecuteTasks();

                RemoveFinishedTasks();
            }
        }

        private void RemoveFinishedTasks()
        {
            _taskList.RemoveAll(w => w.IsFinished() == true);
        }

        private void ExecuteTasks()
        {
            foreach (var t in _taskList.Where(w => w.IsInitialised == true
                                                && w.Started == false 
                                                && w.IsFinished() == false))
            {
                t.Execute();
            }
        }

        void InitializeTasks()
        {
            foreach (var t in _taskList.Where(w => w.IsInitialised == false))
            {
                t.Initialise();
            }
        }

        public  void AddTask(Task newTask)
        {
            _taskList.Add(newTask);
        }
    }
}
