using SIS.WebServer.MyViewEngine;
using System;
using Xunit;

namespace SIS.WebServer.Tests
{
    public class ViewEngineTests
    {
        private const string FolderPath = "./ViewEngineTests/";

        [Theory]
        [InlineData("Test1/")]
        [InlineData("Test2/")]
        [InlineData("Test3/")]
        [InlineData("Test4/")]
        public void TestWithoutViewModel(string folderName)
        {
            string path = FolderPath + folderName;

            string input = FileReader.ReadAllText(path + "ImputData.html");
            string expectedResult = FileReader.ReadAllText(path + "ResultData.html");
            
            IViewEngine viewEngine = new ViewEngine();

            string actualResult = viewEngine.GetHtml(input, null, null);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void TestWithViewModel()
        {
            string path = FolderPath + "Test5/";

            string input = FileReader.ReadAllText(path + "ImputData.html");
            string expectedResult = FileReader.ReadAllText(path + "ResultData.html");

            IViewEngine viewEngine = new ViewEngine();

            var viewModel = new MockClass1("gosho20@gmail.com", "Gosho", 20);

            string actualResult = viewEngine.GetHtml(input, viewModel, null);

            Assert.Equal(expectedResult, actualResult);
        }       
    }

    public class MockClass1
    {
        public MockClass1(string email, string name, int age)
        {
            Email = email;
            Name = name;
            Age = age;
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
