using System;
using BOCS._Core;

namespace ExamplePrograms.Actions
{
    /// <summary>
    /// An Action that allows sending messages.
    /// </summary>
    public interface ISendMessage : IAction
    {
        string Message { get; set; }
    }
}
