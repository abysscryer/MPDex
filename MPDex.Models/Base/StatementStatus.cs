using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Base
{
    public enum StatementStatus : byte
    {
        Pending = 0, // just created
        Placed, // balance amount updated
        Completed, // current amount updated
        Canceled, // balance update canceled
        Expired // balance update expired
    }
}
