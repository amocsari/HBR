using System;

namespace BLL.Services.Implementation
{
    public interface ITimeService
    {
        DateTime UtcNow { get; }
    }
}
