using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator.Migrations;

[Migration(2025010507)]
public class CreatePrioritiesTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE IF NOT EXISTS "Priorities" ("SexId" int PRIMARY KEY,
                    "SexName" varchar(64) not null);
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"Priorities\";");
    }
}