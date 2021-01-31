using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Input
{
    public interface IInputSource : ISource
    {
        bool Next();

        object[] GetData();

    }
}
