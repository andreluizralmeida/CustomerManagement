namespace CustomerManagement.ArchitectureTest;

using CustomerManagement.Domain.Entities;
using CustomerManagement.Infrastructure.Data.Repositories;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using Xunit;

[ExcludeFromCodeCoverage]
public class InheritTest : BaseTest
{
	[Fact]
	public void Models_Entities_Shoud_Inherit_From_Entity()
	{
		// Arrange
		// Act
		var result = types.That().ResideInNamespace("CustomerManagement.Entities")
			.Should()
			.Inherit(typeof(Entity))
			.GetResult();

		// Assert
		result.IsSuccessful.Should().BeTrue(GetFailingTypes(result));
	}

	[Fact]
	public void Repositories_Shoud_Inherit_From_BaseRepository()
	{
		// Arrange
		// Act
		var result = types.That().ResideInNamespace("CustomerManagement.Infrastructure.Data.Repositories").And()
			.DoNotHaveName(typeof(BaseRepository<>).Name)			
			.Should()
			.Inherit(typeof(BaseRepository<>))
			.GetResult();

		// Assert
		result.IsSuccessful.Should().BeTrue(GetFailingTypes(result));
	}
}