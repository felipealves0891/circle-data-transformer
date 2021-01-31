using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Core.DataTransformer.Transformer.Transformations
{
    public class TransformerColumns : ITransformer
    {
        public string[] Types { get; private set; }

        public bool ContainsHeader { get; private set; }

        public TransformerColumns(string[] types, bool containsHeader)
        {
            Types = types;
            ContainsHeader = containsHeader;
        }

        public object Transform(object inputData, int inputIndex, int lineIndex)
        {
            if (ContainsHeader && lineIndex == 0)
                return inputData;

            if (inputIndex >= Types.Length)
                return inputData;

            Type type = Type.GetType(Types[inputIndex]);
            return Converter(inputData, type);
        }

        private object Converter(object value, Type type)
        {
            try
            {
                return Convert.ChangeType(value, type);
            }
            catch
            {
                return null;
            }
        }
    }
}
