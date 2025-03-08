using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator.Migrations;

[Migration(2025010509)]
public class CreateCallsTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE IF NOT EXISTS "Calls" ("CallUuid" varchar(36)  PRIMARY KEY,
                    "CallState" int,
                    "PatientName" varchar(256),
                    "PatientSurname" varchar(256),
                    "PatientPatronymic" varchar(256),
                    "PatientAge" int,
                    "PatientId" varchar(36),
                    "PatientSex" int not null,
                    "BrigadeNumber" varchar(36),
                    "BrigadeProfile" varchar(36),
                    "MainChiefNumber" varchar(36),
                    "MainChiefFullName" varchar(36),
                    "YearNumber" int not null,
                    "DayNumber" int not null,
                    "RegionCode" int,
                    "City" varchar(64),
                    "SsmpNumber" int,
                    "SsmpName" varchar(1024),
                    "Street" varchar(256),
                    "StreetNumber" int,
                    "ApartmentNumber" int,
                    "Entrance" int,
                    "Priority" int not null,
                    "HospitalizationAddress" varchar(256),
                    "HospitalizationDistance" float,
                    "CallType" varchar(256),
                    "Reason" varchar(1024),
                    "ReceiveTime" timestamp,
                    "TransferTime" timestamp,
                    "DepartureTime" timestamp,
                    "ArrivalTime" timestamp,
                    "CallDiagnosis" varchar(1024));
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"Calls\";");
    }
}