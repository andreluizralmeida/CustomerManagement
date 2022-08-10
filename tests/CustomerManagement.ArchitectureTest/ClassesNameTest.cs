namespace CustomerManagement.ArchitectureTest;

using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using Xunit;

[ExcludeFromCodeCoverage]
public class ClassesNameTest : BaseTest
{
	[Fact]
	public void Controllers_Classes_Should_Have_NameEnding_With_Controller()
	{
		// Arrange
		// Act
		var result = types.That().ResideInNamespaceStartingWith("CustomerManagement.Api.Controllers")
			.Should().HaveNameEndingWith("Controller").GetResult();

		// Assert
		result.IsSuccessful.Should().BeTrue(GetFailingTypes(result));
	}

	[Fact]
	public void Interfaces_Should_Have_NameStarting_With_I()
	{
		// Arrange
		// Act
		var result = types.That().ResideInNamespaceStartingWith("CustomerManagement").And().AreInterfaces()
			.Should().HaveNameStartingWith("I").GetResult();

		// Assert
		result.IsSuccessful.Should().BeTrue(GetFailingTypes(result));
	}
}