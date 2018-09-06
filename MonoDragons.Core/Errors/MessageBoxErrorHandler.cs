using System;
using System.Text;
using System.Windows.Forms;

namespace MonoDragons.Core.Errors
{
    public sealed class MessageBoxErrorHandler : IErrorHandler
    {
        public void Handle(Exception ex)
        {
            var msg = new StringBuilder();
            var depth = 0;
            var inner = ex;
            while (inner.InnerException != null)
            {
                depth += 1;
                inner = inner.InnerException;
                for (var i = 0; i < depth; i++)
                    msg.Append("  ");
                msg.Append(inner.Message).AppendLine();
            }

            MessageBox.Show(msg.Append(inner.Message).Append(Environment.NewLine).Append(inner.StackTrace).ToString());
            Environment.Exit(1);
        }
    }
}
