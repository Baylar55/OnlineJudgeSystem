using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;
using System.Reflection;

namespace AlgoCode.Application.Features.Problem.Commands.CompileProblem
{
    public class CompileProblemCommandHandler : IRequestHandler<CompileProblemCommand, CompileProblemCommandResponse>
    {
        public async Task<CompileProblemCommandResponse> Handle(CompileProblemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var code = request.Code;
                var testCases = request.TestCases;

                var compilationOptions = new CSharpCompilationOptions(OutputKind.ConsoleApplication);

                var references = new[]
                {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location)
                };

                var syntaxTree = SyntaxFactory.ParseSyntaxTree(code);
                var compilation = CSharpCompilation.Create("LibraryAssembly")
                                                   .WithOptions(compilationOptions)
                                                   .AddReferences(references)
                                                   .AddSyntaxTrees(syntaxTree);

                using var ms = new MemoryStream();
                var emitResult = compilation.Emit(ms);

                if (!emitResult.Success)
                {
                    var errors = emitResult.Diagnostics.Select(diagnostic => diagnostic.ToString());
                    return new CompileProblemCommandResponse
                    {
                        Result = "Compilation Error: " + string.Join(", ", errors),
                        MemoryUsage = 0,
                        ExecutionTime = 0,
                        Status = SubmissionStatus.CompilationError
                    };
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);

                    var assembly = Assembly.Load(ms.ToArray());

                    var testResults = ExecuteTestCases(assembly, testCases, request.MethodName);

                    stopwatch.Stop();

                    var elapsedMilliseconds = 0.0;
                    var memoryUsageMB = 0.0;
                    if (!testResults.Contains("error"))
                    {
                        elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                        var memoryUsageBytes = Process.GetCurrentProcess().WorkingSet64;
                        memoryUsageMB = Math.Round(memoryUsageBytes / (1024.0 * 1024.0), 2);
                    }

                    return new CompileProblemCommandResponse
                    {
                        Result = testResults,
                        MemoryUsage = elapsedMilliseconds,
                        ExecutionTime = (long)memoryUsageMB,
                        Status = testResults.Contains("error") ? SubmissionStatus.CompilationError : SubmissionStatus.Accepted
                    };

                }
            }
            catch (Exception ex)
            {
                return new CompileProblemCommandResponse
                {
                    Result = $"Exception caught: {ex.Message}",
                    MemoryUsage = 0,
                    ExecutionTime = 0,
                    Status = SubmissionStatus.CompilationError
                };
            }
        }

        private string ExecuteTestCases(Assembly assembly, List<TestCase> testCases, string methodName)
        {
            var results = new List<string>();

            foreach (var testCase in testCases)
            {
                var parameters = testCase.InputParameter;
                var expectedOutput = testCase.ExpectedOutput;

                Type? solutionType = assembly.GetType("Solution");
                MethodInfo? methodInfo = solutionType.GetMethod(methodName);

                object?[] methodParameters = new object?[parameters.Count];

                for (int i = 0; i < parameters.Count; i++)
                {
                    if (parameters[i] != null)
                        methodParameters[i] = parameters[i];
                }

                try
                {
                    if (solutionType == null)
                    {
                        results.Add($"Test case failed: Solution type not found.");
                        continue;
                    }

                    var parameterInfos = methodInfo.GetParameters();
                    for (int i = 0; i < methodParameters.Count(); i++)
                    {
                        Type parameterType = parameterInfos[i].ParameterType;
                        if (parameterType.IsArray && parameterType.GetElementType() == typeof(int))
                        {
                            if (!(methodParameters[i] is string inputString))
                            {
                                results.Add($"Test case failed: Parameter {i} is not a string.");
                                continue;
                            }
                            inputString = inputString.Trim('[', ']').Trim();
                            string[] stringArray = inputString.Split(',');
                            int[] intArray = new int[stringArray.Length];
                            for (int j = 0; j < stringArray.Length; j++)
                                intArray[j] = int.Parse(stringArray[j].Trim());
                            methodParameters[i] = intArray;
                        }
                        else
                            methodParameters[i] = Convert.ChangeType(parameters[i], parameterType);
                    }

                    object? instance = Activator.CreateInstance(solutionType);
                    object? actualOutput = methodInfo.Invoke(instance, methodParameters);
                    Type returnType = methodInfo.ReturnType;

                    actualOutput = Convert.ChangeType(actualOutput?.ToString(), returnType);
                    var actualOutputType = actualOutput?.GetType();
                    var expectedOutputType = expectedOutput?.GetType();

                    var convertedExpectedOutput = Convert.ChangeType(expectedOutput, returnType);

                    if (!actualOutput.Equals(convertedExpectedOutput))
                        results.Add($"Test case failed: Inputs: {string.Join(", ", testCase.InputParameter)}, Expected: {convertedExpectedOutput}, Actual: {actualOutput}");
                    else
                        results.Add($"Test case passed: Inputs: {string.Join(", ", testCase.InputParameter)}, Expected Output: {convertedExpectedOutput}, Actual Output: {actualOutput}");
                }
                catch (Exception ex)
                {
                    results.Add($"Test case failed with exception: {ex.Message}");
                }
            }
            return results.Any() ? string.Join("\n", results) : "All test cases passed successfully";
        }
    }
}
