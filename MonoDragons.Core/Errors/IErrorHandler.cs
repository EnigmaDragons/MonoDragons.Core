using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Errors
{
    public interface IErrorHandler
    {
        void Handle(Exception ex);
    }
}
