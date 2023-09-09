#nullable enable
using System;

namespace HamletTwoSacks.Character
{
    public sealed class ActionReceiver
    {
        public readonly Action Callback;
        public readonly string CallToAction;
        
        public ActionReceiver(Action callback, string callToAction)
        {
            Callback = callback;
            CallToAction = callToAction;
        }
    }
}