using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SIS.WebServer.MyViewEngine
{
    public class ViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            string csharpCode = GenerateCSharpCode(templateCode, viewModel?.GetType());
            IView executableObject = GenerateExecutableCоde(csharpCode, viewModel);

            string html;
            try
            {
                html = executableObject.ExecuteTemplate(viewModel);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return html;
        }

        private string GenerateCSharpCode(string templateCode, Type viewModel)
        {
            string modelTypeName = "object";
            if (viewModel != null)
            {
                if (viewModel.IsGenericType)
                {
                    var modelName = viewModel.FullName;
                    var genericArguments = viewModel.GenericTypeArguments;
                    modelTypeName = modelName.Substring(0, modelName.IndexOf('`'))
                        + "<" + string.Join(",", genericArguments.Select(x => x.FullName)) + ">";
                }
                else
                {
                    modelTypeName = viewModel.FullName;
                }
            }

            string methodBodyCode = GetMethodBodyCode(templateCode);

            return GetCSharpCodeCombined(modelTypeName, methodBodyCode);
        }

        private string GetMethodBodyCode(string templateCode)
        {
            Regex csharpCodeRegex = new Regex(@"[^\""\s&\'\<]+");
            var supportedOperators = new List<string> { "for", "while", "if", "else", "foreach" };
            StringBuilder csharpCode = new StringBuilder();
            StringReader sr = new StringReader(templateCode);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (supportedOperators.Any(x => line.TrimStart().StartsWith("@" + x)))
                {
                    var atSignLocation = line.IndexOf("@");
                    line = line.Remove(atSignLocation, 1);
                    csharpCode.AppendLine(line);
                }
                else if (line.TrimStart().StartsWith("{") ||
                    line.TrimStart().StartsWith("}"))
                {
                    csharpCode.AppendLine(line);
                }
                else
                {
                    csharpCode.Append($"html.AppendLine(@\"");

                    while (line.Contains("@"))
                    {
                        var atSignLocation = line.IndexOf("@");
                        var htmlBeforeAtSign = line.Substring(0, atSignLocation);
                        csharpCode.Append(htmlBeforeAtSign.Replace("\"", "\"\"") + "\" + ");
                        var lineAfterAtSign = line[(atSignLocation + 1)..];
                        var code = csharpCodeRegex.Match(lineAfterAtSign).Value;
                        csharpCode.Append(code + " + @\"");
                        line = lineAfterAtSign[code.Length..];
                    }

                    csharpCode.AppendLine(line.Replace("\"", "\"\"") + "\");");
                }
            }

            return csharpCode.ToString();
        }

        private string GetCSharpCodeCombined(string modelTypeName, string methodBodyCode)
        {
            return @"using System;
                    using System.Text;
                    using System.Linq;
                    using System.Collections.Generic;
                    using SIS.WebServer.MyViewEngine;
                    
                    namespace ViewNamespace
                    {
                        public class ViewClass : IView
                        {
                            public string ExecuteTemplate(object viewModel)
                            {
                                var Model = viewModel as " + modelTypeName + @";
                                var html = new StringBuilder();
                                " + methodBodyCode + @" 
                                return html.ToString();
                            }
                        }
                    }";
        }

        private IView GenerateExecutableCоde(string csharpCode, object viewModel)
        {
            var compileResult = CSharpCompilation
                .Create("ViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

            var libraries = Assembly.Load(
                new AssemblyName("netstandard")).GetReferencedAssemblies();

            foreach (var library in libraries)
            {
                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(
                        Assembly.Load(library).Location));
            }

            if (viewModel != null)
            {
                if (viewModel.GetType().IsGenericType)
                {
                    var genericArguments = viewModel.GetType().GenericTypeArguments;
                    foreach (var genericArgument in genericArguments)
                    {
                        compileResult = compileResult
                            .AddReferences(MetadataReference.CreateFromFile(genericArgument.Assembly.Location));
                    }
                }

                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }

            compileResult = compileResult.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(csharpCode));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                EmitResult result = compileResult.Emit(memoryStream);

                if (!result.Success)
                {
                    return
                        new ErrorView(
                            string.Join(Environment.NewLine,
                            result
                                .Diagnostics
                                .Where(x => x.Severity == DiagnosticSeverity.Error)));
                }

                try
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var byteAssembly = memoryStream.ToArray();
                    var assembly = Assembly.Load(byteAssembly);
                    var viewType = assembly.GetType("ViewNamespace.ViewClass");
                    var instance = Activator.CreateInstance(viewType);
                    return instance as IView ?? new ErrorView("Instance is null!");
                }
                catch (Exception e)
                {
                    return new ErrorView(e.Message);
                }
            }
        }
    }
}
