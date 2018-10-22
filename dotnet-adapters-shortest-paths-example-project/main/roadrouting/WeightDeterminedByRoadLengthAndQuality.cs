using Programmerare.ShortestPaths.Core.Api;
namespace Programmerare.ShortestPaths.Example.Roadrouting {
    /**
     * @author Tomas Johansson
     */
    public sealed class WeightDeterminedByRoadLengthAndQuality : Weight {  

	    private readonly double weightValue;

        private WeightDeterminedByRoadLengthAndQuality(double weightValue) {
		    this.weightValue = weightValue;
	    }
	
	    public WeightDeterminedByRoadLengthAndQuality(int roadLength, RoadQuality roadQuality) {
		    double multiplierForRoadQuality = roadQuality == RoadQuality.BAD ? 1.5 : 1;
		    weightValue = roadLength * multiplierForRoadQuality;
	    }

        public double WeightValue => weightValue;

	    public string RenderToString() {
		    return ToString();
	    }

	    public override string ToString() {
		    return "WeightDeterminedByRoadLengthAndQuality [weightValue=" + weightValue + "]";
	    }


	    // the purpose of these two methods below is to see that they can be used even in the instance created by the algorithms as the total weight
	    public double GetLengthInKiloMeters() {
		    return WeightValue;
	    }
        public double GetLengthInMeters() {
		    return 1000 * GetLengthInKiloMeters();
	    }
	    
        public Weight Create(double value) {
		    return new WeightDeterminedByRoadLengthAndQuality(value);
	    }
    }
}