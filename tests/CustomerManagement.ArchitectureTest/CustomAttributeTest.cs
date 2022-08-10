namespace CustomerManagement.ArchitectureTest;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class CustomAttributeTest : BaseTest
{
    [Fact]
    public void Tests_Unit_Should_HaveAttribute_ExcludeFromCodeCoverageAttribute()
    {
        // Arrange
        // Act
        var result = types.That()
            .ResideInNamespaceStartingWith("CustomerManagement.ArchitectureTest").Or()
            .ResideInNamespaceStartingWith("CustomerManagement.IntegrationTest").Or()
            .ResideInNamespaceStartingWith("CustomerManagement.UnitTest")
            .Should().HaveCustomAttribute(typeof(ExcludeFromCodeCoverageAttribute)).GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(GetFailingTypes(result));
    }
}