namespace CustomerManagement.ArchitectureTest;

using System.Diagnostics.CodeAnalysis;
using CustomerManagement.Domain.Interfaces.Repository;
using FluentAssertions;
using Xunit;

[ExcludeFromCodeCoverage]
public class ImplementsTest : BaseTest
{
	[Fact]
	public void Repository_Shoud_Implements_IRepository()
	{
		// Arrange
		// Act
		var result = types.That().ResideInNamespace("CustomerManagement.Data.Repositories")
			.Should().ImplementInterface(typeof(IBaseRepository<>)).GetResult();

		// Assert
		result.IsSuccessful.Should().BeTrue(GetFailingTypes(result));
	}
}