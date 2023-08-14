﻿using System;
using Scripts.Infrastructure;
using Scripts.Infrastructure.Services;
using UnityEngine;

namespace Scripts.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;
        public BoxCollider Collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Save");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!Collider) return;
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawWireCube(transform.position + Collider.center, Collider.size);
        }
    }
}