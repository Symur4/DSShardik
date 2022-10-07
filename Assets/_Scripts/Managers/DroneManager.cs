﻿using Assets._Scripts.Core.Task;
using Assets._Scripts.Drones;
using Assets._Scripts.Map;
using Assets._Scripts.Models;
using Assets.Scripts.Core;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class DroneManager : Singleton<DroneManager>
    {
        [SerializeField]
        private GameObject DroneContainer;

        private List<Drone> _drones = new List<Drone>();

        private void Awake()
        {
            InputManager.Instance.OnKeyboardAction += OnKeyboardAction;
        }

        private void OnKeyboardAction(KeyCode code)
        {
            if(code == KeyCode.M)
            {
                if (MapManager.Instance.SelectedTile != null)
                {
                    //var drone = _drones.Where(w => w.IsIdle).FirstOrDefault();
                    //drone.Move(MapManager.Instance.SelectedTile);
                    TaskManager.Instance.AddTask(new DroneMoveTask(MapManager.Instance.SelectedTile, null));
                }
            }
        }

        public void AddDrone(MapTile tile)
        {
            AddDrone(new DroneData() { 
                CurrentPosition = new Vector3(tile.transform.position.x, 2, tile.transform.position.z)
            });
        }

        private void AddDrone(DroneData newDrone)
        {
            var droneResource = ResourceCore.Instance
               .DroneList
               .FirstOrDefault();

            var spawned = Instantiate(droneResource.Prefab
               , newDrone.CurrentPosition
               , Quaternion.identity
               , DroneContainer.transform);

            var drone = spawned.GetComponent<Drone>();
            if (drone != null)
            {
                drone.Init(droneResource);
                _drones.Add(drone);
            }
        }

        public Drone GetIdleDrone(bool setBusy = false)
        {
            var drone = _drones.Where(w => w.IsIdle).FirstOrDefault();
            if(drone != null
                && setBusy)
            {
                drone.SetBusy();
            }
            return drone;
        }

        public List<DroneData> GetDronesData()
        {
            return _drones.Select(d => new DroneData() { 
            CurrentPosition = d.transform.position
            ,TargetPosition = d.GetTarget()
            }).ToList();
        }

        public void InitDrones(List<DroneData> drones)
        {
            foreach (var drone in drones)
            {
                AddDrone(drone);
            }

        }
    }
}