using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Base
{
    public enum OrderStatus : byte
    {
        Pending = 0, // just created
        Placed, // balance updated
        Compeleted, // order compeleted
        Canceled, // order canceled
        Expired // order expired
    }
}
