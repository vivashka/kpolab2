using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator.Migrations;

[Migration(2025010503)]
public class CreateResultsCardiogramsTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE IF NOT EXISTS "ResultsCardiograms" ("ResultCardiogramUuid" UUID PRIMARY KEY,
                    "Description" varchar(1024),
                    "DiagnosisMain" varchar(256));
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"ResultsCardiograms\";");
    }
}