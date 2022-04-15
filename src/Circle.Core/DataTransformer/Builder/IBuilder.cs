using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Builder
{
    public interface IBuilder
    {
        Transformation Build();
    }
}
