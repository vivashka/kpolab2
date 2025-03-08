using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator.Migrations;

[Migration(2025010504)]
public class CreateCardiogramsStateTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE IF NOT EXISTS "CardiogramsState" ("CardiogramState" SERIAL PRIMARY KEY,
                    "StateName" varchar(64) not null);
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"CardiogramsState\";");
    }
}