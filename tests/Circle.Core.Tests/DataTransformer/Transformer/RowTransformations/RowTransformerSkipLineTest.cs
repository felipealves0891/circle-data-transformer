using Xunit;
using Bogus;
using Circle.Core.DataTransformer.Transformer.RowTransformations;

namespace Circle.Core.Tests.DataTransformer.Transformer.RowTransformations
{
    public class RowTransformerSkipLineTest
    {
        private object[] _values;

        public RowTransformerSkipLineTest()
        {
            _values = new object[4]
            {
                new object[3] { "ID", "NOME", "DATA DE NASCIMENTO"},
                new object[3] { "1", "Felipe", "1991-08-21"},
                new object[3] { "2", "Edna", "1989-01-26"},
                new object[3] { "3", "Catia", "1973-02-20"}
            };
        }


        [Fact]
        public void TestColumnsWithHeader()
        {
            var t = new RowTransformerSkipLine(0);
            var actual = new object[4];
            var expected = new object[4]
            {
                null,
                new object[3] { "1", "Felipe", "1991-08-21"},
                new object[3] { "2", "Edna", "1989-01-26"},
                new object[3] { "3", "Catia", "1973-02-20"}
            };

            for (int i = 0; i < _values.Length; i++)
                actual[i] = t.Transform((object[])_values[i], i);

            Assert.Equal(expected, actual);

        }

    }
}
