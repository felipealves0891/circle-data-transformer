using Circle.Core.DataTransformer.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Builder
{
    public interface IBuilderInput
    {
        IBuilderOutput SetInput(IInputSource input);
    }
}
