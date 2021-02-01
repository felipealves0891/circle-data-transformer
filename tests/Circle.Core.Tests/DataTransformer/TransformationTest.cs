using Bogus;
using Circle.Core.DataTransformer;
using Circle.Core.DataTransformer.Input;
using Circle.Core.DataTransformer.Output;
using Circle.Core.DataTransformer.Transformer;
using Moq;
using Xunit;

namespace Circle.Core.Tests.DataTransformer
{
    public class TransformationTest
    {
        private object[] _data;

        public TransformationTest()
        {
            var faker = new Faker();
            _data = new object[3]
            {
                faker.Company.CompanyName(),
                faker.Company.CompanyName(),
                faker.Company.CompanyName()
            };
        }

        [Fact]
        public void TestExcecution()
        {
            var t = new Transformation(MockInput(), MockOutput());
            t.AddRowTransformer(MockRowTransformer());
            t.AddTransformer(MockTransformer());
            t.Execute();
            t.Dispose();

            Assert.Equal(1, t.Counter);
        }

        private IInputSource MockInput()
        {
            var mock = new Mock<IInputSource>();
            mock.SetupSequence(i => i.Next())
                .Returns(true)
                .Returns(false);

            mock.SetupSequence(i => i.GetData())
                .Returns(_data);

            mock.Setup(i => i.Close());

            return mock.Object;
        }

        private IOutputSource MockOutput()
        {
            var mock = new Mock<IOutputSource>();
            mock.SetupSequence(i => i.SetData(_data));
            mock.Setup(i => i.Close());

            return mock.Object;
        }

        private IRowTransformer MockRowTransformer()
        {
            var mock = new Mock<IRowTransformer>();
            mock.Setup(i => i.Transform(_data, 0))
                .Returns(_data);

            return mock.Object;
        }

        private ITransformer MockTransformer()
        {
            var mock = new Mock<ITransformer>();
            mock.Setup(i => i.Transform(_data[0], 0, 0)).Returns(_data[0]);
            mock.Setup(i => i.Transform(_data[1], 1, 0)).Returns(_data[1]);
            mock.Setup(i => i.Transform(_data[2], 2, 0)).Returns(_data[2]);

            return mock.Object;
        }
    }
}
