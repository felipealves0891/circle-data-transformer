using Circle.Core.DataTransformer.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Builder
{
    public interface IBuilderOutput
    {
        IBuilderTransformation SetOutput(IOutputSource output);
    }
}
