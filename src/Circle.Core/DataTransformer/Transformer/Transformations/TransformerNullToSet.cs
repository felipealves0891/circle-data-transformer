using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Transformer.Transformations
{
    public class TransformerNullToSet : ITransformer
    {
        private object _value;

        public TransformerNullToSet(object value)
        {
            _value = value;
        }

        public object Transform(object inputData, int inputIndex, int lineIndex)
        {
            return string.IsNullOrEmpty(inputData.ToString()) ? _value : inputData;
        }
    }
}
