﻿namespace Landlords.Notifications
{
    using System;

    public enum MessageType
    {
        Text,
        ClientMethodInvocation,
        ConnectionEvent
    }

    public class Message
    {
        public MessageType MessageType { get; set; }
        public string Data { get; set; }
    }
}