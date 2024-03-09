namespace DigitalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_GameNews", "Game_Id", "dbo.tn_Game");
            DropForeignKey("dbo.tn_Game", "GameGenre_Id", "dbo.tb_GameGenre");
            DropForeignKey("dbo.tb_OrderDetail", "Game_Id", "dbo.tn_Game");
            DropForeignKey("dbo.tb_GameNews", "Publisher_Id", "dbo.tb_Publisher");
            DropForeignKey("dbo.tn_Game", "Publisher_Id", "dbo.tb_Publisher");
            DropForeignKey("dbo.tb_OrderDetail", "Order_Id", "dbo.tb_Order");
            DropIndex("dbo.tn_Game", new[] { "Publisher_Id" });
            DropIndex("dbo.tn_Game", new[] { "GameGenre_Id" });
            DropIndex("dbo.tb_GameNews", new[] { "Game_Id" });
            DropIndex("dbo.tb_GameNews", new[] { "Publisher_Id" });
            DropIndex("dbo.tb_OrderDetail", new[] { "Game_Id" });
            DropIndex("dbo.tb_OrderDetail", new[] { "Order_Id" });
            RenameColumn(table: "dbo.tn_Game", name: "GameGenre_Id", newName: "GameGenreId");
            RenameColumn(table: "dbo.tb_OrderDetail", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.tb_GameNews", name: "Publisher_Id", newName: "PublishersID");
            RenameColumn(table: "dbo.tn_Game", name: "Publisher_Id", newName: "PublisherId");
            RenameColumn(table: "dbo.tb_OrderDetail", name: "Order_Id", newName: "OrderId");
            CreateTable(
                "dbo.tb_ContractCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Contract",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractCategoryId = c.Int(nullable: false),
                        Contract_Code = c.String(),
                        Name = c.String(),
                        NameSideA = c.String(),
                        NameSideB = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActiveDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        Publisher_Id = c.Int(),
                        MarketingPartner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_ContractCategory", t => t.ContractCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.tb_Publisher", t => t.Publisher_Id)
                .ForeignKey("dbo.tb_MarketingPartner", t => t.MarketingPartner_Id)
                .Index(t => t.ContractCategoryId)
                .Index(t => t.Publisher_Id)
                .Index(t => t.MarketingPartner_Id);
            
            CreateTable(
                "dbo.tb_Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        UserName = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        Content = c.String(),
                        Rate = c.Int(nullable: false),
                        CreateDate = c.DateTime(),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tn_Game", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.tb_Wishlist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        UserName = c.String(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tn_Game", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.tb_MarketingPartner",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_VoucherCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_Voucher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherType = c.Int(nullable: false),
                        VoucherCode = c.String(),
                        DiscountPrice = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_VoucherCategory", t => t.VoucherType, cascadeDelete: true)
                .Index(t => t.VoucherType);
            
            AddColumn("dbo.tn_Game", "PublisherName", c => c.String());
            AddColumn("dbo.tn_Game", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tn_Game", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_GameNews", "CategoryID", c => c.Int(nullable: false));
            AddColumn("dbo.tb_OrderDetail", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Order", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Order", "TypePayment", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Order", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Order", "CustomerId", c => c.String());
            AddColumn("dbo.tb_Order", "VoucherCode", c => c.String());
            AddColumn("dbo.tb_Order", "DiscountPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tb_Order", "Voucher_Id", c => c.Int());
            AddColumn("dbo.tb_Menu", "Alias", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AlterColumn("dbo.tn_Game", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.tn_Game", "PublisherId", c => c.Int(nullable: false));
            AlterColumn("dbo.tn_Game", "GameGenreId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_GameNews", "PublishersID", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_OrderDetail", "GameId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_OrderDetail", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_Order", "Order_Date", c => c.DateTime());
            CreateIndex("dbo.tn_Game", "GameGenreId");
            CreateIndex("dbo.tn_Game", "PublisherId");
            CreateIndex("dbo.tb_OrderDetail", "OrderId");
            CreateIndex("dbo.tb_OrderDetail", "GameId");
            CreateIndex("dbo.tb_Order", "Voucher_Id");
            CreateIndex("dbo.tb_GameNews", "PublishersID");
            AddForeignKey("dbo.tb_Order", "Voucher_Id", "dbo.tb_Voucher", "Id");
            AddForeignKey("dbo.tn_Game", "GameGenreId", "dbo.tb_GameGenre", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_OrderDetail", "GameId", "dbo.tn_Game", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_GameNews", "PublishersID", "dbo.tb_Publisher", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tn_Game", "PublisherId", "dbo.tb_Publisher", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_OrderDetail", "OrderId", "dbo.tb_Order", "Id", cascadeDelete: true);
            DropColumn("dbo.tn_Game", "IsHot");
            DropColumn("dbo.tb_GameNews", "Game_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_GameNews", "Game_Id", c => c.Int());
            AddColumn("dbo.tn_Game", "IsHot", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.tb_OrderDetail", "OrderId", "dbo.tb_Order");
            DropForeignKey("dbo.tn_Game", "PublisherId", "dbo.tb_Publisher");
            DropForeignKey("dbo.tb_GameNews", "PublishersID", "dbo.tb_Publisher");
            DropForeignKey("dbo.tb_OrderDetail", "GameId", "dbo.tn_Game");
            DropForeignKey("dbo.tn_Game", "GameGenreId", "dbo.tb_GameGenre");
            DropForeignKey("dbo.tb_Voucher", "VoucherType", "dbo.tb_VoucherCategory");
            DropForeignKey("dbo.tb_Order", "Voucher_Id", "dbo.tb_Voucher");
            DropForeignKey("dbo.tb_Contract", "MarketingPartner_Id", "dbo.tb_MarketingPartner");
            DropForeignKey("dbo.tb_Wishlist", "GameId", "dbo.tn_Game");
            DropForeignKey("dbo.tb_Review", "GameId", "dbo.tn_Game");
            DropForeignKey("dbo.tb_Contract", "Publisher_Id", "dbo.tb_Publisher");
            DropForeignKey("dbo.tb_Contract", "ContractCategoryId", "dbo.tb_ContractCategory");
            DropIndex("dbo.tb_Voucher", new[] { "VoucherType" });
            DropIndex("dbo.tb_Wishlist", new[] { "GameId" });
            DropIndex("dbo.tb_Review", new[] { "GameId" });
            DropIndex("dbo.tb_GameNews", new[] { "PublishersID" });
            DropIndex("dbo.tb_Order", new[] { "Voucher_Id" });
            DropIndex("dbo.tb_OrderDetail", new[] { "GameId" });
            DropIndex("dbo.tb_OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.tn_Game", new[] { "PublisherId" });
            DropIndex("dbo.tn_Game", new[] { "GameGenreId" });
            DropIndex("dbo.tb_Contract", new[] { "MarketingPartner_Id" });
            DropIndex("dbo.tb_Contract", new[] { "Publisher_Id" });
            DropIndex("dbo.tb_Contract", new[] { "ContractCategoryId" });
            AlterColumn("dbo.tb_Order", "Order_Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_OrderDetail", "OrderId", c => c.Int());
            AlterColumn("dbo.tb_OrderDetail", "GameId", c => c.Int());
            AlterColumn("dbo.tb_GameNews", "PublishersID", c => c.Int());
            AlterColumn("dbo.tn_Game", "GameGenreId", c => c.Int());
            AlterColumn("dbo.tn_Game", "PublisherId", c => c.Int());
            AlterColumn("dbo.tn_Game", "Price", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.tb_Menu", "Alias");
            DropColumn("dbo.tb_Order", "Voucher_Id");
            DropColumn("dbo.tb_Order", "DiscountPrice");
            DropColumn("dbo.tb_Order", "VoucherCode");
            DropColumn("dbo.tb_Order", "CustomerId");
            DropColumn("dbo.tb_Order", "Status");
            DropColumn("dbo.tb_Order", "TypePayment");
            DropColumn("dbo.tb_Order", "Quantity");
            DropColumn("dbo.tb_OrderDetail", "Quantity");
            DropColumn("dbo.tb_GameNews", "CategoryID");
            DropColumn("dbo.tn_Game", "IsNew");
            DropColumn("dbo.tn_Game", "OriginalPrice");
            DropColumn("dbo.tn_Game", "PublisherName");
            DropTable("dbo.tb_Voucher");
            DropTable("dbo.tb_VoucherCategory");
            DropTable("dbo.tb_MarketingPartner");
            DropTable("dbo.tb_Wishlist");
            DropTable("dbo.tb_Review");
            DropTable("dbo.tb_Contract");
            DropTable("dbo.tb_ContractCategory");
            RenameColumn(table: "dbo.tb_OrderDetail", name: "OrderId", newName: "Order_Id");
            RenameColumn(table: "dbo.tn_Game", name: "PublisherId", newName: "Publisher_Id");
            RenameColumn(table: "dbo.tb_GameNews", name: "PublishersID", newName: "Publisher_Id");
            RenameColumn(table: "dbo.tb_OrderDetail", name: "GameId", newName: "Game_Id");
            RenameColumn(table: "dbo.tn_Game", name: "GameGenreId", newName: "GameGenre_Id");
            CreateIndex("dbo.tb_OrderDetail", "Order_Id");
            CreateIndex("dbo.tb_OrderDetail", "Game_Id");
            CreateIndex("dbo.tb_GameNews", "Publisher_Id");
            CreateIndex("dbo.tb_GameNews", "Game_Id");
            CreateIndex("dbo.tn_Game", "GameGenre_Id");
            CreateIndex("dbo.tn_Game", "Publisher_Id");
            AddForeignKey("dbo.tb_OrderDetail", "Order_Id", "dbo.tb_Order", "Id");
            AddForeignKey("dbo.tn_Game", "Publisher_Id", "dbo.tb_Publisher", "Id");
            AddForeignKey("dbo.tb_GameNews", "Publisher_Id", "dbo.tb_Publisher", "Id");
            AddForeignKey("dbo.tb_OrderDetail", "Game_Id", "dbo.tn_Game", "Id");
            AddForeignKey("dbo.tn_Game", "GameGenre_Id", "dbo.tb_GameGenre", "Id");
            AddForeignKey("dbo.tb_GameNews", "Game_Id", "dbo.tn_Game", "Id");
        }
    }
}
