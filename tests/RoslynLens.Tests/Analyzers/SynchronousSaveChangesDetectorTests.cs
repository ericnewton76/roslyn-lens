using RoslynLens.Analyzers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;

namespace RoslynLens.Tests.Analyzers;

public class SynchronousSaveChangesDetectorTests
{
    private readonly SynchronousSaveChangesDetector _detector = new();

    [Fact]
    public void RequiresSemanticModel_IsTrue()
    {
        _detector.RequiresSemanticModel.ShouldBeTrue();
    }

    [Fact]
    public void Returns_Empty_When_SemanticModel_Is_Null()
    {
        const string source = """
            public class Foo
            {
                void M() { _context.SaveChanges(); }
            }
            """;

        var tree = CSharpSyntaxTree.ParseText(source);
        var violations = _detector.Detect(tree, null, TestContext.Current.CancellationToken).ToList();
        violations.ShouldBeEmpty();
    }
}
