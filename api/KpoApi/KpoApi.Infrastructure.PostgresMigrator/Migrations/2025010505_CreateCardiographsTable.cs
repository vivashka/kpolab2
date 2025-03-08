using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator.Migrations;

[Migration(2025010505)]
public class CreateCardiographsTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE IF NOT EXISTS "Cardiographs" ("SerialNumber" varchar(128) PRIMARY KEY,
                    "CardiographName" varchar(128) not null,
                    "ManufacturerName" varchar(256));
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"Cardiographs\";");
    }
}