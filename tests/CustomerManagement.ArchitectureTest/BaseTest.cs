namespace CustomerManagement.ArchitectureTest;

using System.Diagnostics.CodeAnalysis;
using NetArchTest.Rules;

[ExcludeFromCodeCoverage]
public abstract class BaseTest
{
    protected readonly Types types;

    protected BaseTest()
    {
        this.types = Types.InCurrentDomain();
    }

    protected string GetFailingTypes(TestResult result)
    {
        return result.FailingTypeNames != null ?
            "\n" + string.Join("\n ", result.FailingTypeNames) :
            string.Empty;
    }
}