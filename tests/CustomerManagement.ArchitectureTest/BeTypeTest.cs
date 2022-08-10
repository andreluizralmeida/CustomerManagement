namespace CustomerManagement.ArchitectureTest;

using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using Xunit;

[ExcludeFromCodeCoverage]
public class BeTypeTest : BaseTest
{
	[Fact]
	public void Interfaces_Repositories_Should_BeInterfaces()
	{
		// Arrange
		// Act
		var result = types.That().ResideInNamespace("CustomerManagement.Domain.Interfaces")
			.Should().BeInterfaces().GetResult();

		// Assert
		result.IsSuccessful.Should().BeTrue(GetFailingTypes(result));
	}
}