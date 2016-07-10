namespace EmailClient.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginEmail = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Domain = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailSetting");
        }
    }
}
