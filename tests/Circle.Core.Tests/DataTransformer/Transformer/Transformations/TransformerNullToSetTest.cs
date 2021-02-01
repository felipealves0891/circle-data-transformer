using Moq;
using Xunit;
using Bogus;
using Circle.Core.DataTransformer.Transformer.Transformations;

namespace Circle.Core.Tests.DataTransformer.Transformer.Transformations
{
    public class TransformerNullToSetTest
    {
        private object _value;

        public TransformerNullToSetTest()
        {
            var faker = new Faker();
            _value = faker.Company.CompanyName();
        }

        [Fact]
        public void NullToSet()
        {
            var t = new TransformerNullToSet(_value);
            Assert.Equal(_value, t.Transform(null, 0, 0));
        }

        [Fact]
        public void NotNull()
        {
            var t = new TransformerNullToSet(_value);
            Assert.Equal("NotNull", t.Transform("NotNull", 0, 0));
        }

    }
}
