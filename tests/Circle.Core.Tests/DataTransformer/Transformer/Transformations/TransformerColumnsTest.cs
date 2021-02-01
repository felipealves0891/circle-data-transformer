using Xunit;
using Bogus;
using Circle.Core.DataTransformer.Transformer.Transformations;
using System;

namespace Circle.Core.Tests.DataTransformer.Transformer.Transformations
{
    public class TransformerColumnsTest
    {
        private string[] _types;

        private object[] _values;

        public TransformerColumnsTest()
        {
            _types = new string[3] { "System.Int32", "System.String", "System.DateTime" };
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
            var t = new TransformerColumns(_types, true);
            var actual = new object[4] 
            {
                new object[3],
                new object[3],
                new object[3],
                new object[3]
            };

            var expected = new object[4]
            {
                new object[3] { "ID", "NOME", "DATA DE NASCIMENTO"},
                new object[3] { 1, "Felipe", new DateTime(1991, 08, 21)},
                new object[3] { 2, "Edna", new DateTime(1989, 01, 26)},
                new object[3] { 3, "Catia", new DateTime(1973, 02, 20) }
            };

            for (int i = 0; i < _values.Length; i++)
                for (int j = 0; j < ((object[])_values[i]).Length; j++)
                    ((object[])actual[i])[j] = t.Transform(((object[])_values[i])[j], j, i);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void TestColumnsWithoutHeader()
        {
            var t = new TransformerColumns(_types, false);
            var actual = new object[3]
            {
                new object[3],
                new object[3],
                new object[3]
            };

            var expected = new object[3]
            {
                new object[3] { 1, "Felipe", new DateTime(1991, 08, 21)},
                new object[3] { 2, "Edna", new DateTime(1989, 01, 26)},
                new object[3] { 3, "Catia", new DateTime(1973, 02, 20) }
            };

            for (int i = 1; i < _values.Length; i++)
                for (int j = 0; j < ((object[])_values[i]).Length; j++)
                    ((object[])actual[i-1])[j] = t.Transform(((object[])_values[i])[j], j, i-1);

            Assert.Equal(expected, actual);

        }
    }
}
