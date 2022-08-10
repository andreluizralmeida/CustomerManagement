namespace CustomerManagement.ArchitectureTest;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class DependencyTest : BaseTest
{
    [Fact]
    public void Controllers_Should_Not_Have_Dependency_From_Infrastructure()
    {
        // Arrange
        // Act
        var result = types.That().ResideInNamespace("Farfetch.LookupService.Api.Controllers")
            .Should().NotHaveDependencyOn("CustomerManagement.Infrastructure").GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(GetFailingTypes(result));
    }
}