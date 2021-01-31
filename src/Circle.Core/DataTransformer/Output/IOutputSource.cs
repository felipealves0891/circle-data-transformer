using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Output
{
    public interface IOutputSource : ISource
    {
        void SetData(object[] data);
    }
}
