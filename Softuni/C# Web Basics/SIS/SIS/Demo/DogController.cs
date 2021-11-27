using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Demo
{
    public class DogsController
    {
        private List<string> dogs;

        public DogsController()
        {
            dogs = new List<string>();
        }

        private const string ParameterDogName = "DogName";

        public IHttpResponse AllDogs(IHttpRequest request)
        {
            string content = string.Join(GlobalConstants.HttpNewLine, dogs.Select(x => $"<p>{x}<p>"));

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }

        public IHttpResponse AddDogForm(IHttpRequest request)
        {
            string content = File.ReadAllText("./Resourses/SearchDogForm.html");

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }

        public IHttpResponse AddDog(IHttpRequest request)
        {
            string dogName = request.FormData[ParameterDogName] as string;

            dogs.Add(dogName);

            return AllDogs(null);
        }
    }
}
