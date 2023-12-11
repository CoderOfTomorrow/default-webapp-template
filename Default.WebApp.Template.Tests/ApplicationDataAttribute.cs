using EntityFrameworkCore.AutoFixture.Core;
using EntityFrameworkCore.AutoFixture.Sqlite;

namespace Default.WebApp.Template.Tests
{
    public class ApplicationDataAttribute(params object[] arguments) : InlineAutoDataAttribute(() => new Fixture()
                .Customize(new CompositeCustomization(
                    new AutoNSubstituteCustomization(),
                    new SqliteCustomization
                    {
                        AutoOpenConnection = true,
                        OmitDbSets = true,
                        OnCreate = OnCreateAction.EnsureCreated
                    })),
                arguments)
    {
    }
}