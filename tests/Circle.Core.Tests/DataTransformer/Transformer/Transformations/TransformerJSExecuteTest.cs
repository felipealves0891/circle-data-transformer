using Moq;
using Xunit;
using Bogus;
using Circle.Core.DataTransformer.Transformer.Transformations;

namespace Circle.Core.Tests.DataTransformer.Transformer.Transformations
{
    public class TransformerJSExecuteTest
    {
        [Theory]
        [InlineData("Nome_transformed", "Nome")]
        [InlineData("Sobrenome_transformed", "Sobrenome")]
        public void Transform_ExecuteScript_Asserting(string expected, string test)
        {
            //Arrange
            string script = "function transformer(data){ return data + '_transformed';}";

            //Act
            var t = new TransformerJSExecute(script);
            var actual = t.Transform(test, 0, 0);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
