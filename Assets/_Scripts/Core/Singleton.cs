﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{   
    /// <summary>
    /// A static instance is similar to a singleton, but instead of destroying any new
    /// instances, it overrides the current instance. This is handy for resetting the state
    /// and saves you doing it manually
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        private static readonly object _lock = new object();
        public static T Instance
        {
            get
            {

                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();
                    }

                    return instance;
                }
            }
        }               
    }

    ///// <summary>
    ///// This transforms the static instance into a basic singleton. This will destroy any new
    ///// versions created, leaving the original instance intact
    ///// </summary>
    //public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    //{
    //    protected override void Awake()
    //    {
    //        if (Instance != null) Destroy(gameObject);
    //        base.Awake();
    //    }
    //}

    ///// <summary>
    ///// Finally we have a persistent version of the singleton. This will survive through scene
    ///// loads. Perfect for system classes which require stateful, persistent data. Or audio sources
    ///// where music plays through loading screens, etc
    ///// </summary>
    //public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    //{
    //    protected override void Awake()
    //    {
    //        base.Awake();
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}


}
