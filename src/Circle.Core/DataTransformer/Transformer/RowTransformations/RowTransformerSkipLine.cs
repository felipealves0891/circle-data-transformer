using Circle.Core.DataTransformer.Transformer;

namespace Circle.Core.DataTransformer.Transformer.RowTransformations
{
    public class RowTransformerSkipLine : IRowTransformer
    {
        private int _lines;

        public RowTransformerSkipLine(int lines)
        {
            _lines = lines;
        }

        public object[] Transform(object[] inputData, int inputIndex)
        {
            if (inputIndex <= _lines)
                return null;

            return inputData;
        }
    }
}
