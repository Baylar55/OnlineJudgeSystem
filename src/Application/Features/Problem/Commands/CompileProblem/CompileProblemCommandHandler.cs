using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;
using System.Reflection;

namespace AlgoCode.Application.Features.Problem.Commands.CompileProblem
{
    public class CompileProblemCommandHandler : IRequestHandler<CompileProblemCommand, string>
    {
        public async Task<string> Handle(CompileProblemCommand request, CancellationToken cancellationToken)
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
                    return "Compilation Error: " + string.Join(", ", errors);
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);

                    var assembly = Assembly.Load(ms.ToArray());

                    var testResults = ExecuteTestCases(assembly, testCases);

                    stopwatch.Stop();
                    var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                    var memoryUsageBytes = Process.GetCurrentProcess().WorkingSet64;
                    var memoryUsageMB = Math.Round(memoryUsageBytes / (1024.0 * 1024.0), 2);

                    return $"Test Results:\n{testResults}\nElapsed Time: {elapsedMilliseconds}ms\nMemory Usage: {memoryUsageMB} MB";
                }
            }
            catch (Exception ex)
            {
                return string.Format("Exception catched");
            }
        }

        private string ExecuteTestCases(Assembly assembly, List<TestCase> testCases)
        {
            var results = new List<string>();

            foreach (var testCase in testCases)
            {
                var parameters = testCase.Inputs.Select(str => (object)str).ToArray();
                var expectedOutput = (object)testCase.ExpectedOutput; // Convert.ChangeType(testCase.ExpectedOutput, typeof(object));

                Type? solutionType = assembly.GetType("Solution");
                MethodInfo? methodInfo = solutionType.GetMethod("IsPalindrome");
                for (int i = 0; i < parameters.Length; i++)
                {
                    ParameterInfo paramInfo = methodInfo.GetParameters()[i];
                    if (parameters[i] != null)
                    {
                        parameters[i] = Convert.ChangeType(parameters[i].ToString(), paramInfo.ParameterType);
                    }
                }
                try
                {
                    if (solutionType == null)
                    {
                        results.Add($"Test case failed: Solution type not found.");
                        continue;
                    }
                    object? instance = Activator.CreateInstance(solutionType);
                    object? actualOutput = methodInfo.Invoke(instance, parameters);
                    Type returnType = methodInfo.ReturnType;

                    actualOutput = Convert.ChangeType(actualOutput?.ToString(), returnType);
                    expectedOutput = Convert.ChangeType(expectedOutput.ToString(), returnType);

                    if (!actualOutput.Equals(expectedOutput))
                        results.Add($"Test case failed: Inputs: {string.Join(", ", testCase.Inputs)}, Expected: {expectedOutput}, Actual: {actualOutput}");
                    else
                        results.Add($"Test case passed: Inputs: {string.Join(", ", testCase.Inputs)}, Expected Output: {expectedOutput}, Actual Output: {actualOutput}");
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
