#nullable enable

using System.Collections.Generic;
using dmdspirit.Core.Attributes;
using HamletTwoSacks.Time;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace HamletTwoSacks.AI
{
    public sealed class Brain : MonoBehaviour
    {
        private readonly CompositeDisposable _subs = new();

        [SerializeField, ReadOnly]
        private Task? _activeTask;

        [SerializeField]
        private List<Task> _tasks = null!;

        [SerializeField, Button(nameof(FindTasks))]
        private bool _findTasks;

        [Inject]
        private void Construct(TimeController timeController)
        {
            foreach (Task task in _tasks)
                task.Completed.Subscribe(OnTaskCompleted).AddTo(_subs);
            timeController.Update.Subscribe(OnUpdate).AddTo(_subs);
            timeController.FixedUpdate.Subscribe(OnFixedUpdate).AddTo(_subs);
        }

        private void Start()
        {
            _activeTask = _tasks[0];
            _activeTask.Activate();
        }

        private void OnDestroy()
            => _subs.Dispose();

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