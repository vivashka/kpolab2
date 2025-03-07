using FluentMigrator;

namespace KpoApi.Migrator.Migrations;

[Migration(2025010502)]
public class CreateUsersTable : Migration 
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE "Users" ("UserUuid" UUID PRIMARY KEY,
                    "Login" varchar(8) not null,
                    "Password" varchar(64) not null,
                    "PhoneNumber" int,
                    "FullName" varchar(256) not null,
                    "OrganizationUuid" UUID not null,
                    "Appointment" varchar(256) not null,
                    constraint fk_users_organizations
                    FOREIGN KEY ("OrganizationUuid") 
                        REFERENCES "Organizations" ("OrganizationUuid"));
                    
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists users;");
    }
}