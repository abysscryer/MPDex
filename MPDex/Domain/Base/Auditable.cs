﻿namespace MPDex.Domain.Base
{
    /// <summary>
    /// auditable model abstract class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Auditable<T> : Editable<T>, IAuditable
    {
        public string IPAddress { get; set; }
        public byte[] Version { get; set; }
    }
}