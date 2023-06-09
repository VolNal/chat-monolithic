using FluentMigrator;

namespace VolNal.Chat.Migrator.Migrations;

[Migration(4)]
public class ChatMembersTable:Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            IF NOT EXISTS (
                SELECT * FROM sys.tables t 
                JOIN sys.schemas s ON (t.schema_id = s.schema_id) 
                WHERE s.name = 'ChatMembers') 	
                CREATE TABLE ChatMembers(
                    Id INT PRIMARY KEY IDENTITY,
                    User_id INT,
                    ChatId INT
                    );");
    }


    public override void Down()
    {
        Execute.Sql("IF EXISTS (" +
                    "DROP TABLE dbo.ChatMembers" +
                    ");");
    }
}