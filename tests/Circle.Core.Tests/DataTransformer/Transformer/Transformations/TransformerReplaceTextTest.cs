using Circle.Core.DataTransformer.Transformer;
using Circle.Core.DataTransformer.Transformer.Transformations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Circle.Core.Tests.DataTransformer.Transformer.Transformations
{
    public class TransformerReplaceTextTest
    {
        [Fact]
        public void Transform_Replacement_Assertive()
        {
            //Arrange
            string text = "123|456";
            string search = "|";
            string replacement = ";";
            string expected = "123;456";

            //Act
            TransformerReplaceText transformer = new TransformerReplaceText(search, replacement);

            //Assert
            Assert.Equal(expected, transformer.Transform(text, 0, 0));
        }
    }

}
