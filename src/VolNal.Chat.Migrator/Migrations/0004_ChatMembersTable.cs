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
                WHERE s.name = 'chat_members') 	
                CREATE TABLE messages(
                    id INT PRIMARY KEY IDENTITY,
                    user_id INT,
                    chat_id INT
                    );");
    }


    public override void Down()
    {
        Execute.Sql("IF EXISTS (" +
                    "DROP TABLE dbo.chat_members" +
                    ");");
    }
}