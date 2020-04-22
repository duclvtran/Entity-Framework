namespace P04_Relationship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Email3", "RefToPerson", "dbo.Person3");
            DropForeignKey("dbo.Email4", "Owner_Person4Id", "dbo.Person4");
            DropIndex("dbo.Email3", new[] { "RefToPerson" });
            DropIndex("dbo.Email4", new[] { "Owner_Person4Id" });
            DropColumn("dbo.Email4", "RefToPerson");
            RenameColumn(table: "dbo.Email4", name: "Owner_Person4Id", newName: "RefToPerson");
            AddColumn("dbo.Email3", "Owner_Person3Id", c => c.Int());
            AlterColumn("dbo.Email4", "RefToPerson", c => c.Int(nullable: false));
            CreateIndex("dbo.Email3", "Owner_Person3Id");
            CreateIndex("dbo.Email4", "RefToPerson");
            AddForeignKey("dbo.Email3", "Owner_Person3Id", "dbo.Person3", "Person3Id");
            AddForeignKey("dbo.Email4", "RefToPerson", "dbo.Person4", "Person4Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Email4", "RefToPerson", "dbo.Person4");
            DropForeignKey("dbo.Email3", "Owner_Person3Id", "dbo.Person3");
            DropIndex("dbo.Email4", new[] { "RefToPerson" });
            DropIndex("dbo.Email3", new[] { "Owner_Person3Id" });
            AlterColumn("dbo.Email4", "RefToPerson", c => c.Int());
            DropColumn("dbo.Email3", "Owner_Person3Id");
            RenameColumn(table: "dbo.Email4", name: "RefToPerson", newName: "Owner_Person4Id");
            AddColumn("dbo.Email4", "RefToPerson", c => c.Int(nullable: false));
            CreateIndex("dbo.Email4", "Owner_Person4Id");
            CreateIndex("dbo.Email3", "RefToPerson");
            AddForeignKey("dbo.Email4", "Owner_Person4Id", "dbo.Person4", "Person4Id");
            AddForeignKey("dbo.Email3", "RefToPerson", "dbo.Person3", "Person3Id", cascadeDelete: true);
        }
    }
}
