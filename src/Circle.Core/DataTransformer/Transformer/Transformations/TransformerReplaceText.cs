using System;
using System.Collections.Generic;
using System.Text;

namespace Circle.Core.DataTransformer.Transformer.Transformations
{

    public class TransformerReplaceText : ITransformer
    {
        private StringBuilder stringBuilder;
        private readonly string search;
        private readonly string replacement;

        public TransformerReplaceText(string search, string replacement)
        {
            this.search = search;
            this.replacement = replacement;
            this.stringBuilder = new StringBuilder();
        }

        public object Transform(object inputData, int inputIndex, int lineIndex)
        {
            stringBuilder = stringBuilder.Clear();
            stringBuilder = stringBuilder.Append(inputData);
            stringBuilder = stringBuilder.Replace(search, replacement);
            return stringBuilder.ToString();
        }

    }
}
