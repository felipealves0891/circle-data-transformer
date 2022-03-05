using Bogus;
using Circle.Core.DataTransformer;
using Circle.Core.DataTransformer.Input;
using Circle.Core.DataTransformer.Output;
using Circle.Core.DataTransformer.Transformer;
using Moq;
using System;
using Xunit;

namespace Circle.Core.Tests.DataTransformer
{
    public class TransformationTest
    {
        private object[] _data;

        private readonly Mock<IInputSource> _mockInput;

        private readonly Mock<IOutputSource> _mockOutput;

        private readonly Mock<IRowTransformer> _mockRowTransformer;

        private readonly Mock<ITransformer> _mockTransformer;

        public TransformationTest()
        {
            var faker = new Faker();
            _data = new object[3]
            {
                faker.Company.CompanyName(),
                faker.Company.CompanyName(),
                faker.Company.CompanyName()
            };

            _mockInput = new Mock<IInputSource>();
            _mockOutput = new Mock<IOutputSource>();
            _mockRowTransformer = new Mock<IRowTransformer>();
            _mockTransformer = new Mock<ITransformer>();
        }

        [Fact]
        public void Execute_ExecutionOfETL_Asserting()
        {
            //Arrange
            IInputSource input = MockInput();
            IOutputSource output = MockOutput();
            IRowTransformer rowTransformer = MockRowTransformer();
            ITransformer transformer = MockTransformer();

            //Act
            var t = new Transformation(input, output);
            t.AddRowTransformer(rowTransformer);
            t.AddTransformer(transformer);
            t.Execute();
            t.Dispose();

            //Assert
            Assert.Equal(1, t.Counter);
            _mockInput.VerifyAll();
            _mockOutput.VerifyAll();
            _mockRowTransformer.VerifyAll();
            _mockTransformer.VerifyAll();
        }

        private IInputSource MockInput()
        {
            _mockInput.SetupSequence(i => i.Next())
                .Returns(true)
                .Returns(false)
                .Throws<InvalidOperationException>();

            _mockInput.Setup(i => i.GetData())
                .Returns(_data)
                .Verifiable();

            _mockInput.Setup(i => i.Close())
                      .Verifiable();

            return _mockInput.Object;
        }

        private IOutputSource MockOutput()
        {
            _mockOutput.Setup(i => i.SetData(_data))
                       .Verifiable();

            _mockOutput.Setup(i => i.Close())
                       .Verifiable();;

            return _mockOutput.Object;
        }

        private IRowTransformer MockRowTransformer()
        {
            _mockRowTransformer.Setup(i => i.Transform(_data, 0))
                               .Returns(_data)
                               .Verifiable();

            return _mockRowTransformer.Object;
        }

        private ITransformer MockTransformer()
        {
            _mockTransformer.Setup(i => i.Transform(_data[0], 0, 0))
                            .Returns(_data[0])
                            .Verifiable();

            _mockTransformer.Setup(i => i.Transform(_data[1], 1, 0))
                            .Returns(_data[1])
                            .Verifiable();

            _mockTransformer.Setup(i => i.Transform(_data[2], 2, 0))
                            .Returns(_data[2])
                            .Verifiable();

            return _mockTransformer.Object;
        }
    }
}
