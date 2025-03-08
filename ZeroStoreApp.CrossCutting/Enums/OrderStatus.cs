using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStoreApp.CrossCutting.Enums;

public enum OrderStatus
{
    Canceled = -1,
    Created = 0,
    Payed = 1,
    Delivered = 2,
    Completed = 3,
}
