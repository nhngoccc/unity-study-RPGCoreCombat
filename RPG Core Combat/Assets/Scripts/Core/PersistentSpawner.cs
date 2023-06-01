using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{
    public class PersistentSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentPrefabs;
        bool hasSpawner = false;
        private void Awake()
        {
            if (hasSpawner) return;
            SpawnerPersistent();
            hasSpawner = true;

        }

        private void SpawnerPersistent()
        {
            GameObject fader = Instantiate(persistentPrefabs);
            DontDestroyOnLoad(fader);
        }
    }
}


