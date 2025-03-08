using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator;

[Migration(2025010501)]
public class CreateOrganizationTable : Migration 
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE IF NOT EXISTS "Organizations" (
                        "OrganizationUuid" UUID PRIMARY KEY,
                    "Name" varchar(256),
                    "SsmpNumber" int not null,
                    "SsmpAdress" varchar(1024) not null,
                    "PhoneContactName" varchar(256),
                    "PhoneNumber" int);
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"Organizations\";");
    }
}