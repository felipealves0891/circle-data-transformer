using Moq;
using Xunit;
using Bogus;
using Circle.Core.DataTransformer.Transformer.Transformations;

namespace Circle.Core.Tests.DataTransformer.Transformer.Transformations
{
    public class TransformerToLowerTest
    {
        [Theory]
        [InlineData("nome", "NOME")]
        [InlineData("sobrenome", "SOBRENOME")]
        public void ToLower(string expected, string actual)
        {
            var t = new TransformerToLower();
            Assert.Equal(expected, t.Transform(actual, 0, 0));
        }

    }
}
