using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Transformer.Transformations
{
    public class TransformerNullToEmpty : ITransformer
    {
        public object Transform(object inputData, int inputIndex, int lineIndex)
        {
            return inputData == null ? string.Empty : inputData;
        }
    }
}
