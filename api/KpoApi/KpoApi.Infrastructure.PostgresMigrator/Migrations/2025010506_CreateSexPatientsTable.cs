using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator.Migrations;

[Migration(2025010506)]
public class CreateSexPatientsTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE IF NOT EXISTS "SexPatients" ("SexId" int PRIMARY KEY,
                    "SexName" varchar(64) not null);
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"SexPatients\";");
    }
}