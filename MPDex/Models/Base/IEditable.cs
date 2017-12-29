﻿using System;

namespace MPDex.Models.Base
{
    /// <summary>
    /// editable model interface
    /// </summary>
    public interface IEditable
    {
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
    }
}