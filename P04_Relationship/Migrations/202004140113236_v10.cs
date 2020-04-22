namespace P04_Relationship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompanyPersons",
                c => new
                    {
                        Company_Id = c.Int(nullable: false),
                        Person_PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Company_Id, t.Person_PersonId })
                .ForeignKey("dbo.Companies", t => t.Company_Id, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_PersonId, cascadeDelete: true)
                .Index(t => t.Company_Id)
                .Index(t => t.Person_PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyPersons", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.CompanyPersons", "Company_Id", "dbo.Companies");
            DropIndex("dbo.CompanyPersons", new[] { "Person_PersonId" });
            DropIndex("dbo.CompanyPersons", new[] { "Company_Id" });
            DropTable("dbo.CompanyPersons");
            DropTable("dbo.Companies");
        }
    }
}
