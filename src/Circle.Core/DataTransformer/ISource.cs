using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer
{
    public interface ISource
    {
        void Open(string connectionString);

        void Close();
    }
}
