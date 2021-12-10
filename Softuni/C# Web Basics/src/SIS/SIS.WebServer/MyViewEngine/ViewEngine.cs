using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using SIS.WebServer.DataManager;
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
        public string GetHtml(string templateCode, object viewModel, IdentityUser user)
        {
            string csharpCode = GenerateCSharpCode(templateCode, viewModel?.GetType());
            IView executableObject = GenerateExecutableCоde(csharpCode, viewModel);

            string html;
            try
            {
                html = executableObject.ExecuteTemplate(viewModel, user);
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
            var lines = templateCode.Split(new string[] { "\r\n", "\n\r", "\n" }, StringSplitOptions.None);
            var csharpCode = new StringBuilder();
            var supportedOperators = new[] { "for", "if", "else" };
            var csharpCodeRegex = new Regex(@"[^\s<""\&]+", RegexOptions.Compiled);
            var csharpCodeDepth = 0; // If > 0, Inside CSharp Syntax

            foreach (var line in lines)
            {
                string currentLine = line;

                if (currentLine.TrimStart().StartsWith("@{"))
                {
                    csharpCodeDepth++;
                }
                else if (currentLine.TrimStart().StartsWith("{") || currentLine.TrimStart().StartsWith("}"))
                {
                    // { / }
                    if (csharpCodeDepth > 0)
                    {
                        if (currentLine.TrimStart().StartsWith("{"))
                        {
                            csharpCodeDepth++;
                        }
                        else if (currentLine.TrimStart().StartsWith("}"))
                        {
                            if ((--csharpCodeDepth) == 0)
                            {
                                continue;
                            }
                        }
                    }

                    csharpCode.AppendLine(currentLine);
                }
                else if (csharpCodeDepth > 0)
                {
                    csharpCode.AppendLine(currentLine);
                    continue;
                }
                else if (supportedOperators.Any(x => currentLine.TrimStart().StartsWith("@" + x)))
                {
                    // @C#
                    var atSignLocation = currentLine.IndexOf("@");
                    var csharpLine = currentLine.Remove(atSignLocation, 1);
                    csharpCode.AppendLine(csharpLine);
                }
                else
                {
                    var csharpStringToAppend = "html.AppendLine(@\"";
                    var restOfLine = currentLine;
                    while (restOfLine.Contains("@"))
                    {
                        var atSignLocation = restOfLine.IndexOf("@");
                        var plainText = restOfLine.Substring(0, atSignLocation).Replace("\"", "\"\"");
                        var csharpExpression = csharpCodeRegex.Match(restOfLine.Substring(atSignLocation + 1))?.Value;

                        if (csharpExpression.Contains("{") && csharpExpression.Contains("}"))
                        {
                            var csharpInlineExpression =
                                csharpExpression.Substring(1, csharpExpression.IndexOf("}") - 1);

                            csharpStringToAppend += plainText + "\" + " + csharpInlineExpression + " + @\"";

                            csharpExpression = csharpExpression.Substring(0, csharpExpression.IndexOf("}") + 1);
                        }
                        else
                        {
                            csharpStringToAppend += plainText + "\" + " + csharpExpression + " + @\"";
                        }

                        if (restOfLine.Length <= atSignLocation + csharpExpression.Length + 1)
                        {
                            restOfLine = string.Empty;
                        }
                        else
                        {
                            restOfLine = restOfLine.Substring(atSignLocation + csharpExpression.Length + 1);
                        }
                    }

                    csharpStringToAppend += $"{restOfLine.Replace("\"", "\"\"")}\");";
                    csharpCode.AppendLine(csharpStringToAppend);
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
                    using SIS.WebServer.DataManager;
                    
                    namespace ViewNamespace
                    {
                        public class ViewClass : IView
                        {
                            public string ExecuteTemplate(object viewModel, IdentityUser user)
                            {
                                var Model = viewModel as " + modelTypeName + @";
                                var User = user;           
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
