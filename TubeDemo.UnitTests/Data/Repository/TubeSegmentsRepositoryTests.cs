using System.Text;
using System.IO.Abstractions.TestingHelpers;
using TubeDemo.Data.Repository;
using TubeDemo.Data.Contexts;

namespace TubeDemo.UnitTests.Data.Repository
{
    public class TubeSegmentsRepositoryTests
    {

        static MockFileSystem GetMockFileSystem(string mockFilePath, StringBuilder mockData)
        {
            return new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { mockFilePath, new MockFileData(mockData.ToString()) },
            });
        }

        [Fact]
        public void TubeSegmentsRepository_FromCSVSource_ParsedToObject()
        {
            //Arrange
            string errMessage;
            string fName = @"c:\Test\Data\file.csv";
            
            StringBuilder data = new StringBuilder();
            data.AppendLine("Name;Distance;Angle;Width;Hegth;IsDefect");
            data.AppendLine("Коррозия;2;7;3;2;yes");

            var mockFS = GetMockFileSystem(fName, data);
            var mockStream = new MockFileStream(mockFS, fName, FileMode.Open);

            //Act
            var repository = new TubeSegmentsRepository(new CsvContext(mockStream));
            var records = repository.GetAll(out errMessage);

            //Assert
            Assert.NotEmpty(records);
        }
    }
}
