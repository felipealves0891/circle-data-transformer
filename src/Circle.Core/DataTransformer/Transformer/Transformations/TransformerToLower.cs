using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Transformer.Transformations
{
    public class TransformerToLower : ITransformer
    {
        public object Transform(object inputData, int inputIndex, int lineIndex)
        {
            return inputData.ToString().ToLower();
        }
    }
}
