using FluentMigrator;

namespace KpoApi.Infrastructure.PostgresMigrator.Migrations;

[Migration(2025010508)]
public class CreateUsersCardiographsTable : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    CREATE TABLE IF NOT EXISTS "UsersCardiographs" ("UsersCardiographsId" SERIAL PRIMARY KEY,
                    "UserUuid" UUID not null,
                    "CardiographId" varchar(128) not null,
                    constraint fk_UsersCardiographs_users 
                    FOREIGN KEY ("UserUuid") 
                        REFERENCES "Users" ("UserUuid"),
                    constraint fk_UsersCardiographs_cardiographs 
                    FOREIGN KEY ("CardiographId") 
                        REFERENCES "Cardiographs" ("SerialNumber"));
                    """);
    }

    public override void Down()
    {
        Execute.Sql("drop table if exists \"UsersCardiographs\";");
    }
}