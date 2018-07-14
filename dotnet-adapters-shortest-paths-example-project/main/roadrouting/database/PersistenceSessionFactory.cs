using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;

namespace roadrouting.database {

    public class PersistenceSessionFactory {
        public static ISessionFactory CreateSessionFactory() {
          return Fluently.Configure()
            .Database(
              SQLiteConfiguration.Standard
                .UsingFile(DbFile)
            )
            .Mappings(m =>
              m.FluentMappings.AddFromAssemblyOf<City>()
              //.Conventions.AddFromAssemblyOf<EnumConvention>()
              )
             .ExposeConfiguration(BuildSchema)
            .BuildSessionFactory();
        }

        /// <summary>
        /// SQLite file at path: ...\dotnet-adapters-shortest-paths-example-project\bin\Debug\roadrouting.db
        /// </summary>
        private static string DbFile = "roadrouting.db";

        private static void BuildSchema(Configuration config) {
          //if (File.Exists(DbFile))
          //  File.Delete(DbFile);
          bool databaseFilexists = File.Exists(DbFile);
          var schemaExport = new SchemaExport(config);
          schemaExport.Create(useStdOut: true, execute: !databaseFilexists);
        }
    }
}