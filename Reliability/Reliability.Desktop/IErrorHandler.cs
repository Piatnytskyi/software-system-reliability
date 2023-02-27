using System;

namespace Reliability.Desktop
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
