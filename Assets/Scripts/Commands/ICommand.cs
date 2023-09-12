#nullable enable

using System;

namespace HamletTwoSacks.Commands
{
    public interface ICommand
    {
        IObservable<ICommand> OnCompleted { get; }
        void Start();
        void Interrupt();
    }
}