using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Transformer
{
    public interface ITransformer
    {
        object Transform(object inputData, int inputIndex, int lineIndex);
    }
}
