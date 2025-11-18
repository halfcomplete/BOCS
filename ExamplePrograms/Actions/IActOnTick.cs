using System;
using BOCS._Core;

namespace BOCSExamplePrograms._Core.Actions
{
    /// <summary>
    /// An Action that is triggered on each tick of the system.
    /// </summary>
    public interface IActOnTick : IAction
    {
        void OnTick();
    }
}
