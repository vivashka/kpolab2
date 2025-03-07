using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator.Migrations;

[Migration(2025010510)]
public class CreateCardiogramsTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                     CREATE TABLE "Cardiograms" ("CardiogramUuid" UUID  PRIMARY KEY,
                     "ReceivedTime" timestamp,
                     "MeasurementTime" timestamp,
                     "CardiographUuid" varchar(128) not null,
                     "CallUuid" varchar(36) not null,
                     "CardiogramState" int not null,
                     "ResultCardiogramUuid" UUID,
                     "RawCardiogram" jsonb,
                     constraint fk_Cardiograms_ResultCardiogramUuid
                     FOREIGN KEY ("ResultCardiogramUuid")
                         REFERENCES "ResultsCardiograms" ("ResultCardiogramUuid"));
                     """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"Cardiograms\";");
    }
}