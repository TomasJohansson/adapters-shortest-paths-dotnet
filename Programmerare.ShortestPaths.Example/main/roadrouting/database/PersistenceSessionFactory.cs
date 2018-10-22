using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;

namespace Programmerare.ShortestPaths.Example.Roadrouting.Database {

    public class PersistenceSessionFactory {
        public static ISessionFactory CreateSessionFactory() {
          return Fluently.Configure()
            .Database(
              SQLiteConfiguration.Standard
                .UsingFile(GetFullPathToSQLiteFile())
            )
            .Mappings(m =>
              m.FluentMappings.AddFromAssemblyOf<City>()
              //.Conventions.AddFromAssemblyOf<EnumConvention>()
              )
             .ExposeConfiguration(BuildSchema)
            .BuildSessionFactory();
        }

        /// <summary>
        /// SQLite file at path: ...\Programmerare.ShortestPaths.Example\sqlite_directory\roadrouting.db
        /// </summary>
        private static string NameOfSQLiteFileWithoutPath = "roadrouting.sqlite";
        private static string NameOfSQLiteDirectory = "sqlite_directory";
        // SQLite commands to look in the database:
        // cd [ the path to the directory ...\Programmerare.ShortestPaths.Example\sqlite_directory ]
        // sqlite3 roadrouting.sqlite
        // .tables (displays the existing tables, should be City and Road)
        // SELECT * FROM Road;
        // SELECT * FROM City;
        // .quit

        private static void BuildSchema(Configuration config) {
          //if (File.Exists(DbFile))
          //  File.Delete(DbFile);
          bool databaseFilexists = File.Exists(GetFullPathToSQLiteFile());
          var schemaExport = new SchemaExport(config);
          //schemaExport.Create(useStdOut: true, execute: !databaseFilexists);
          schemaExport.Create(useStdOut: true, execute: true);
        }

        private static string GetFullPathToSQLiteFile() {
            string fullPathToSQLiteFile = GetFullPath(NameOfSQLiteFileWithoutPath);
            //Console.WriteLine("GetFullPathToSQLiteFile() : " + fullPathToSQLiteFile);
            return fullPathToSQLiteFile;
        }
        
        private static string GetFullPath(string fileNameWithoutDirectoryPath) {
            string basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var directory = new DirectoryInfo(basePath);
            directory = TryGetSQLiteSubDirectoryOfProjectRoot(directory);
            string fullPath = Path.Combine(directory.FullName, fileNameWithoutDirectoryPath);
            return fullPath;
        }

        private static DirectoryInfo TryGetSQLiteSubDirectoryOfProjectRoot(DirectoryInfo directory) {
            //Console.WriteLine("Name of ExecutingAssembly: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            string projectName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; // "Programmerare.ShortestPaths.Example"
            int maxNumberOfBaseDirectoriesToNavigate = 3;  // tyipcally only two: debug and bin (bin/debug)
            int count = 0;
            var baseDirectory = directory;
            while(count < maxNumberOfBaseDirectoriesToNavigate) {
                baseDirectory = baseDirectory.Parent;
                if(baseDirectory.Name.Equals(projectName)) {
                    var sqlite_directory = new DirectoryInfo(Path.Combine(baseDirectory.FullName, NameOfSQLiteDirectory));
                    if(!sqlite_directory.Exists) {
                        sqlite_directory.Create();
                    }
                    return sqlite_directory;
                }
            }
            return directory;
        }
    }
}