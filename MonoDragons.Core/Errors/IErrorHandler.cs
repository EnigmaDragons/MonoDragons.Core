using System;

namespace MonoDragons.Core.Errors
{
    public interface IErrorHandler
    {
        void Handle(Exception ex);
    }
}
