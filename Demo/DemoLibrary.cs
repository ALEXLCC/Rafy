﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OEA;
using OEA.Library.ORM.DbMigration;
using OEA.MetaModel.View;
using OEA.MetaModel;

namespace Demo
{
    class DemoLibrary : ILibrary
    {
        public ReuseLevel ReuseLevel
        {
            get { return ReuseLevel.Main; }
        }

        public void Initialize(IApp app)
        {
            app.ModuleOperations += (o, e) =>
            {
                UIModel.AggtBlocks.DefineBlocks("书籍查询界面", new AggtBlocks
                {
                    MainBlock = new Block(typeof(Book))
                    {
                        ExtendView = "书籍查询界面_Book"
                    },
                    Surrounders = 
                    {
                        new SurrounderBlock(typeof(BookQueryCriteria))
                        {
                            BlockType = BlockType.Detail,
                            SurrounderType = SurrounderType.Condition,
                        }
                    }
                });

                var moduleBookImport = CommonModel.Modules.AddRoot(new ModuleMeta
                {
                    Label = "书籍录入",
                    Children =
                    {
                        new ModuleMeta{ Label = "书籍类别", EntityType = typeof(BookCategory)},
                        new ModuleMeta{ Label = "书籍管理", EntityType = typeof(Book)},
                    }
                });

                var moduleQuery = CommonModel.Modules.AddRoot(new ModuleMeta
                {
                    Label = "书籍查询模块",
                    Children =
                    {
                        new ModuleMeta
                        {
                            Label = "书籍查询", EntityType = typeof(Book), AggtBlocksName= "书籍查询界面",
                        }
                    }
                });

                if (OEAEnvironment.IsWeb)
                {
                    moduleBookImport.Children.Add(new ModuleMeta { Label = "163", CustomUI = "http://www.163.com" });

                    moduleQuery.Children[0].Children.Add(new ModuleMeta
                    {
                        Label = "书籍查询(Url访问)",
                        CustomUI = "EntityModule?isAggt=1&type=Demo.Book&viewName=书籍查询界面"
                    });
                }
            };

            app.DbMigratingOperations += (o, e) =>
            {
                using (var c = new OEADbMigrationContext(DemoEntity.ConnectionString))
                {
                    //c.RollbackToHistory(DateTime.Parse("2012-01-07 21:27:00.000"), RollbackAction.DeleteHistory);
                    c.AutoMigrate();
                };
            };
        }
    }
}