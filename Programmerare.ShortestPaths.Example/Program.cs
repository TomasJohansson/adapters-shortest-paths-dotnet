using Programmerare.ShortestPaths.Example.Roadrouting;
using System;

namespace dotnet_adapters_shortest_paths_example_project {
    class Program {
	    /**
	    * Use the following commands to run the main method:
	    * 			(but use the last "1" argument only if you want to use SQLite database,
	    * 		 	 so for example use "0" if you want hardcoded 'entities' instead) :
        * 		cd Programmerare.ShortestPaths.Example/bin/Debug
	    * 		Programmerare.ShortestPaths.Example 1
        * 		Programmerare.ShortestPaths.Example 0
	    * @param args "1" if you want to use the database, otherwise hardcoded 'entities' will be used.
	    */
        static void Main(string[] args) {
		    bool useDatabase = ParseArguments(args);
            Console.WriteLine("My Console App");
            RoadRoutingMain.MainMethod(useDatabase);
            Console.ReadLine();
        }

        /**
         * @param args
         * @return true if args[0] == "1" otherwise false
         */
        private static bool ParseArguments(String[] args) {
            if (args == null || args.Length < 1) return false;
            return args[0].Equals("1");
        }
    }
}