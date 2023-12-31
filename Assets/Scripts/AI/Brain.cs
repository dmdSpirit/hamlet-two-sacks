﻿#nullable enable

using System.Collections.Generic;
using Aether.Attributes;
using HamletTwoSacks.Infrastructure;
using HamletTwoSacks.Time;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace HamletTwoSacks.AI
{
    public sealed class Brain : MonoBehaviour
    {
        private TimeController _timeController = null!;

        private readonly CompositeDisposable _tasksSubs = new();
        private readonly CompositeDisposable _timeSubs = new();

        [SerializeField, ReadOnly]
        private Task? _activeTask;

        [SerializeField]
        private List<Task> _tasks = null!;

        [SerializeField, Button(nameof(FindTasks))]
        private bool _findTasks;

        public bool IsActive { get; private set; }

        [Inject]
        private void Construct(TimeController timeController)
        {
            _timeController = timeController;
            foreach (Task task in _tasks)
                task.Completed.Subscribe(OnTaskCompleted).AddTo(_tasksSubs);
        }

        private void OnDestroy()
        {
            _timeSubs.Dispose();
            _tasksSubs.Dispose();
        }

        public void Initialize(SystemReferences systemReferences)
        {
            foreach (Task task in _tasks)
                task.Initialize(systemReferences);
        } 

        public void Activate()
        {
            if (IsActive)
                return;
            IsActive = true;
            if (_activeTask == null)
                _activeTask = _tasks[0];

            _timeController.Update.Subscribe(OnUpdate).AddTo(_timeSubs);
            _timeController.FixedUpdate.Subscribe(OnFixedUpdate).AddTo(_timeSubs);

            _activeTask.Activate();
        }

        public void Deactivate()
        {
            IsActive = false;
            _timeSubs.Clear();
            if(_activeTask!=null)
                _activeTask.Deactivate();
        }

        private void OnTaskCompleted(Task completedTask)
        {
            Assert.IsTrue(completedTask == _activeTask);
            int taskIndex = _tasks.IndexOf(completedTask);
            taskIndex = (taskIndex + 1) % _tasks.Count;
            _activeTask = _tasks[taskIndex];

            // TODO (Stas): Handle tasks that can not be started.
            // - Stas 13 September 2023
            _activeTask.Activate();
        }

        private void OnUpdate(float time)
        {
            if (_activeTask != null)
                _activeTask.OnUpdate(time);
        }

        private void OnFixedUpdate(float time)
        {
            if (_activeTask != null)
                _activeTask.OnFixedUpdate(time);
        }

        private void FindTasks()
        {
            _tasks.Clear();
            _tasks.AddRange(GetComponents<Task>());
        }
    }
}