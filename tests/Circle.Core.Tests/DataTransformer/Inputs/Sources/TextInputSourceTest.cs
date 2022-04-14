using Circle.Core.DataTransformer.Input.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Circle.Core.Tests.DataTransformer.Inputs.Sources
{
    public class TextInputSourceTest
    {
        [Fact]
        public void GetData_WithData_Assertive() 
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //Arrange
                object[] expected = new object[] { "Column 1", "Column 2", "Column 3" };
                string records = "Column 1;Column 2;Column 3";

                StreamWriter sw = new StreamWriter(ms);
                sw.WriteLine(records);
                sw.Flush();
                ms.Position = 0;

                //Act
                StreamReader sr = new StreamReader(ms);
                TextInputSource source = new TextInputSource(sr);

                //Assert
                Assert.True(source.Next());
                Assert.Equal(expected, source.GetData());
            }
        }
    }
}
