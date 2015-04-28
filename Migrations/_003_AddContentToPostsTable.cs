using FluentMigrator;

namespace SimpleBlog.Migrations
{
    [Migration(3)]
    // ReSharper disable once InconsistentNaming
    public class _003_AddContentToPostsTable : Migration
    {
        public override void Up()
        {
            if (Create == null) return;

            Create.Column("content").OnTable("posts").AsCustom("TEXT");
        }

        public override void Down()
        {
            Delete.Column("content").FromTable("posts");
        }
    }
}