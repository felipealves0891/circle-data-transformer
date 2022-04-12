using System.IO;
using Circle.Core.DataTransformer.Output.Sources;
using Xunit;

namespace Circle.Core.Tests.DataTransformer.Outputs.Sources
{
    public class TextOutputSourceTest
    {
        [Fact]
        public void SetData_WithRecords_Assertive()
        {
            //Arrange
            string[] records = new string[] { "Column 1", "Column 2", "Column 3"};
            string expected = "Column 1;Column 2;Column 3";
            string filename = "./TextOutputSourceTest_SetData_WithRecords_Assertive";

            //Act
            TextOutputSource outputSource = new TextOutputSource();
            outputSource.Open(filename);
            outputSource.SetData(records);
            outputSource.Close();
            
            StreamReader reader = new StreamReader(filename);
            string current = reader.ReadLine();
            reader.Close();
            
            //Assert
            Assert.Equal(expected, current);
        }
    }
}