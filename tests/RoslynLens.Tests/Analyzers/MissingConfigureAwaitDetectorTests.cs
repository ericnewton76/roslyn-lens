using RoslynLens.Analyzers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;

namespace RoslynLens.Tests.Analyzers;

public class MissingConfigureAwaitDetectorTests
{
    private readonly MissingConfigureAwaitDetector _detector = new();

    [Fact]
    public void RequiresSemanticModel_IsTrue()
    {
        _detector.RequiresSemanticModel.ShouldBeTrue();
    }

    [Fact]
    public void Returns_Empty_When_SemanticModel_Is_Null()
    {
        const string source = """
            using System.Threading.Tasks;
            public class Foo
            {
                async Task M() { await Task.Delay(1); }
            }
            """;

        var tree = CSharpSyntaxTree.ParseText(source);
        var violations = _detector.Detect(tree, null, TestContext.Current.CancellationToken).ToList();
        violations.ShouldBeEmpty();
    }
}
