namespace CustomerManagement.UnitTest;

using AutoFixture;
using AutoFixture.AutoNSubstitute;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

[ExcludeFromCodeCoverage]
public abstract class TestsBase
{

    protected IFixture Fixture { get; }

    protected TestsBase()
    {
        this.Fixture = new Fixture();

        this.Fixture.Customize(new AutoNSubstituteCustomization());

        this.Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));

        this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}