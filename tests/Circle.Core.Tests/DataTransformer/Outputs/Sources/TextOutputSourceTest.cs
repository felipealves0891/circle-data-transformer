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
            using (MemoryStream stream = new MemoryStream()) 
            { 
                //Arrange
                string[] records = new string[] { "Column 1", "Column 2", "Column 3" };
                string expected = "Column 1;Column 2;Column 3";

                //Act
                StreamWriter writer = new StreamWriter(stream);
                TextOutputSource outputSource = new TextOutputSource(writer);
                outputSource.SetData(records);
                
                writer.Flush();
                stream.Position = 0;

                //Assert
                using (StreamReader reader = new StreamReader(stream))
                {
                    string current = reader.ReadLine();
                    Assert.Equal(expected, current);
                }
            }
        }
    }
}