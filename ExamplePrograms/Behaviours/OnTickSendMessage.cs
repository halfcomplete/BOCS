using System;
using BOCS._Core;
using BOCS._Core.Actions;

namespace ExamplePrograms.Behaviours
{
    public class OnTickSendMessage : IActOnTick, ISendMessage, IBehaviour
    {
        public string Message { get; set; };

        public OnTickSendMessage(string message)
        {
            Message = message;
        }

        public void OnTick()
        {
            Console.WriteLine(Message);
        }
    }
}
