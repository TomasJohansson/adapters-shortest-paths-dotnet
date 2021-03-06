using Programmerare.ShortestPaths.Adapter.Bsmock.Generics;
using Programmerare.ShortestPaths.Adapter.YanQi.Generics;
using Programmerare.ShortestPaths.Core.Api.Generics;
using Programmerare.ShortestPaths.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programmerare.ShortestPaths.Example.Roadrouting {
    /**
     * @author Tomas Johansson
     */
    public class RoadRoutingMain {
	    public static void MainMethod(bool useDatabase) {
            Console.WriteLine("useDatabase : " + useDatabase);
		    CityRoadService cityRoadService = CityRoadServiceFactory.CreateCityRoadService(useDatabase);
		    try {
			    IList<Road> roads = cityRoadService.GetAllRoads();
			    City startCity = cityRoadService.GetStartCity();
			    City endCity = cityRoadService.GetEndCity();

			    var pathFinderFactories = new List<PathFinderFactoryGenerics< PathFinderGenerics<  PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality>   , Road ,  City , WeightDeterminedByRoadLengthAndQuality > , PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality> ,  Road ,  City , WeightDeterminedByRoadLengthAndQuality>>();
			    pathFinderFactories.Add(new PathFinderFactoryYanQiGenerics<PathFinderGenerics<  PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality>   , Road ,  City , WeightDeterminedByRoadLengthAndQuality > , PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality> ,  Road ,  City , WeightDeterminedByRoadLengthAndQuality>());
			    pathFinderFactories.Add(new PathFinderFactoryBsmockGenerics<PathFinderGenerics<  PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality>   , Road ,  City , WeightDeterminedByRoadLengthAndQuality > , PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality> ,  Road ,  City , WeightDeterminedByRoadLengthAndQuality>());
			    PerformRoadRoutingForTheImplementations(
				    roads, 
				    startCity, 
				    endCity, 
				    pathFinderFactories
			    );			
		    }
		    finally {
			    cityRoadService.ReleaseResourcesIfAny();	
		    }
	    }
	
	    private static void PerformRoadRoutingForTheImplementations(
		    IList<Road> roads, 
		    City startCity, 
		    City endCity, 
		    IList<PathFinderFactoryGenerics<PathFinderGenerics<  PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality>   , Road ,  City , WeightDeterminedByRoadLengthAndQuality > , PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality> ,  Road ,  City , WeightDeterminedByRoadLengthAndQuality>> pathFinderFactories
	    ) {
		    // the parameter GraphEdgesValidationDesired.NO will be used so therefore do the validation once externally here first
            GraphEdgesValidator<PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality>, Road ,  City , WeightDeterminedByRoadLengthAndQuality>.ValidateEdgesForGraphCreation<PathGenerics<Road ,  City , WeightDeterminedByRoadLengthAndQuality>, Road ,  City , WeightDeterminedByRoadLengthAndQuality>(roads);
		    foreach (PathFinderFactoryGenerics<PathFinderGenerics<PathGenerics<Road, City, WeightDeterminedByRoadLengthAndQuality>, Road, City, WeightDeterminedByRoadLengthAndQuality>, PathGenerics<Road, City, WeightDeterminedByRoadLengthAndQuality>, Road, City, WeightDeterminedByRoadLengthAndQuality> pathFinderFactory in pathFinderFactories) {
			    PerformRoadRouting(roads, startCity, endCity, pathFinderFactory);
		    }
	    }

	    private static void PerformRoadRouting(
		    IList<Road> roads, 
		    City startCity, 
		    City endCity, 
		    PathFinderFactoryGenerics<PathFinderGenerics<PathGenerics<Road, City, WeightDeterminedByRoadLengthAndQuality>, Road, City, WeightDeterminedByRoadLengthAndQuality>, PathGenerics<Road, City, WeightDeterminedByRoadLengthAndQuality>, Road, City, WeightDeterminedByRoadLengthAndQuality> pathFinderFactory
	    ) {
		    Console.WriteLine("--------------------------------");
		    Console.WriteLine("Implementation starts for " + pathFinderFactory.GetType().Name);
		
		    // Note that the datatype below is List<Road>  where "Road" is an EXAMPLE 
		    // of domain object you can create yourself.
		    // Note that such an object must implement the interface "Edge" and in particular must pay attention to 
		    // how the method "getEdgeId()" must be implemented as documented in the Edge interface.
		    PathFinderGenerics<PathGenerics<Road, City, WeightDeterminedByRoadLengthAndQuality>, Road, City, WeightDeterminedByRoadLengthAndQuality> pathFinder = pathFinderFactory.CreatePathFinder(
			    roads, 
			    GraphEdgesValidationDesired.NO // do the validation one time instead of doing it for each pathFinderFactory
		    ); 
		
		    IList<PathGenerics<Road , City , WeightDeterminedByRoadLengthAndQuality>> paths = pathFinder.FindShortestPaths(startCity, endCity, 10);
		    // Now also note that you can retrieve your own domain object (for example "Road" above) 
		    // through the returned paths when iterating the path edges, i.e. instead of a list typed as "Edge"
		    // you now have a list with "Road" and thus can use methods of that class, e.g. "getRoadName()" as below
		
		    Console.WriteLine("Paths size "+ paths.Count);
		    foreach (PathGenerics<Road , City , WeightDeterminedByRoadLengthAndQuality> path in paths) {
			    Console.WriteLine(GetPrettyPrintedPath(path));
		    }
		    Console.WriteLine("--------------------------------");
	    }
	
	    private static String GetPrettyPrintedPath(PathGenerics<Road , City , WeightDeterminedByRoadLengthAndQuality> path) {
		    // Note that the code you now are looking at corresponds to client code, i.e. code you could have written yourself.
		    // The Path interface is included in the library, but not WeightDeterminedByRoadLengthAndQuality
		    // which should be thought of as your own defined type (subtype of Weight in the library).
		    // Still the path will return it strongly typed below thanks to usage of Generics.
		    // It should also be noted that reflection is NOT used for instantiating the object with the total weight
		    // but instead the create method of the Weight interface will be used for creating the instance 
		    // from within the class PathFinderBase.
		    WeightDeterminedByRoadLengthAndQuality totalWeightForPath = path.TotalWeightForPath;
		    Console.WriteLine("totalWeightForPath " + totalWeightForPath);
		    Console.WriteLine("totalWeightForPath.getLengthInMeters() " + totalWeightForPath.GetLengthInMeters());
		    Console.WriteLine("totalWeightForPath.getLengthInKiloMeters() " + totalWeightForPath.GetLengthInKiloMeters());
		    // The reason that the above code works is that a prototypical instance of WeightDeterminedByRoadLengthAndQuality
		    // will be used for creating an instance of itself. 
		
		    IList<Road> roadsForPath = path.EdgesForPath;
		    StringBuilder sb = new StringBuilder();
		    sb.Append("Total weight: " + path.TotalWeightForPath.WeightValue + " | ");
		    for (int i = 0; i < roadsForPath.Count; i++) {
			    Road road = roadsForPath[i];
			    City cityFrom = road.GetCityFrom();
			    City cityFromVertex = road.StartVertex;
			    Console.WriteLine("cityFrom getCityName : " + cityFrom.CityName);
			    if(i > 0) {
				    sb.Append(" ---> ");
			    }
			    sb.Append("[" + road.RoadName + "](" + road.GetEdgeWeight().WeightValue + ")");
		    }
		    return sb.ToString();
	    }

    }
}