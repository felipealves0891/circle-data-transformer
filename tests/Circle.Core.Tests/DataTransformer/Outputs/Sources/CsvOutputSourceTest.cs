using Circle.Core.DataTransformer.Output;
using Circle.Core.DataTransformer.Output.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Circle.Core.Tests.DataTransformer.Outputs.Sources
{
    public class CsvOutputSourceTest
    {
        [Fact]
        public void SetData_WithRecords_Assertive()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                //Arrange
                object[] records = new object[] { "01", $"teste\r\nteste", "teste;teste", "teste\"teste\"teste" };
                string expected = "\"01\";\"teste\r\nteste\";\"teste;teste\";\"teste\\\"teste\\\"teste\"\r\n";

                //Act
                StreamWriter writer = new StreamWriter(stream);
                CsvOutputSource outputSource = new CsvOutputSource(writer);
                outputSource.SetData(records);

                writer.Flush();
                stream.Position = 0;

                //Assert
                using (StreamReader reader = new StreamReader(stream))
                {
                    string current = reader.ReadToEnd();
                    Assert.Equal(expected, current);
                }
            }

        }
    }

    
}
