using Moq;
using Xunit;
using Bogus;
using Circle.Core.DataTransformer.Transformer.Transformations;

namespace Circle.Core.Tests.DataTransformer.Transformer.Transformations
{
    public class TransformerNullToEmptyTest
    {
        private object _value;

        public TransformerNullToEmptyTest()
        {
            var faker = new Faker();
            _value = faker.Company.CompanyName();
        }

        [Fact]
        public void NullToEmpty()
        {
            var t = new TransformerNullToEmpty();
            Assert.Equal(string.Empty, t.Transform(null, 0, 0));
        }

        [Fact]
        public void NotNull()
        {
            var t = new TransformerNullToEmpty();
            Assert.Equal(_value, t.Transform(_value, 0, 0));
        }
    }
}
