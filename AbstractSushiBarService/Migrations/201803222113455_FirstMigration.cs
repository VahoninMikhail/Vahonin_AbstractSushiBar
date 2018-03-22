namespace AbstractSushiBarService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CookFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zakazs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitorId = c.Int(nullable: false),
                        SushiId = c.Int(nullable: false),
                        CookId = c.Int(),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cooks", t => t.CookId)
                .ForeignKey("dbo.Sushis", t => t.SushiId, cascadeDelete: true)
                .ForeignKey("dbo.Visitors", t => t.VisitorId, cascadeDelete: true)
                .Index(t => t.VisitorId)
                .Index(t => t.SushiId)
                .Index(t => t.CookId);
            
            CreateTable(
                "dbo.Sushis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SushiName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SushiIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SushiId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Sushis", t => t.SushiId, cascadeDelete: true)
                .Index(t => t.SushiId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.StorageId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visitors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitorFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zakazs", "VisitorId", "dbo.Visitors");
            DropForeignKey("dbo.Zakazs", "SushiId", "dbo.Sushis");
            DropForeignKey("dbo.SushiIngredients", "SushiId", "dbo.Sushis");
            DropForeignKey("dbo.SushiIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.StorageIngredients", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.StorageIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.Zakazs", "CookId", "dbo.Cooks");
            DropIndex("dbo.StorageIngredients", new[] { "IngredientId" });
            DropIndex("dbo.StorageIngredients", new[] { "StorageId" });
            DropIndex("dbo.SushiIngredients", new[] { "IngredientId" });
            DropIndex("dbo.SushiIngredients", new[] { "SushiId" });
            DropIndex("dbo.Zakazs", new[] { "CookId" });
            DropIndex("dbo.Zakazs", new[] { "SushiId" });
            DropIndex("dbo.Zakazs", new[] { "VisitorId" });
            DropTable("dbo.Visitors");
            DropTable("dbo.Storages");
            DropTable("dbo.StorageIngredients");
            DropTable("dbo.Ingredients");
            DropTable("dbo.SushiIngredients");
            DropTable("dbo.Sushis");
            DropTable("dbo.Zakazs");
            DropTable("dbo.Cooks");
        }
    }
}
