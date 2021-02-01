using Moq;
using Xunit;
using Bogus;
using Circle.Core.DataTransformer.Transformer.Transformations;

namespace Circle.Core.Tests.DataTransformer.Transformer.Transformations
{
    public class TransformerJSExecuteTest
    {
        private string _script = "function transformer(data){ return data + '_transformed';}";

        [Theory]
        [InlineData("Nome_transformed", "Nome")]
        [InlineData("Sobrenome_transformed", "Sobrenome")]
        public void JSExecute(string expected, string actual)
        {
            var t = new TransformerJSExecute(_script);
            Assert.Equal(expected, t.Transform(actual, 0, 0));
        }
    }
}
