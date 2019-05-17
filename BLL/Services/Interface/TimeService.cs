using BLL.Services.Implementation;
using System;

namespace BLL.Services.Implementation
{
    public class TimeService : ITimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
