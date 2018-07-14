package roadrouting;

import com.programmerare.shortestpaths.core.api.Weight;

/**
 * @author Tomas Johansson
 */
public final class WeightDeterminedByRoadLengthAndQuality implements Weight {  

	private final double weightValue;

	private WeightDeterminedByRoadLengthAndQuality(final double weightValue) {
		this.weightValue = weightValue;
	}
	
	public WeightDeterminedByRoadLengthAndQuality(final int roadLength, final RoadQuality roadQuality) {
		final double multiplierForRoadQuality = roadQuality == RoadQuality.BAD ? 1.5 : 1;
		weightValue = roadLength * multiplierForRoadQuality;
	}

	public double getWeightValue() {
		return weightValue;
	}

	public String renderToString() {
		return toString();
	}

	@Override
	public String toString() {
		return "WeightDeterminedByRoadLengthAndQuality [weightValue=" + weightValue + "]";
	}


	// the purpose of these two methods below is to see that they can be used even in the instance created by the algorithms as the total weight
	public double getLengthInKiloMeters() {
		return getWeightValue();
	}
	public double getLengthInMeters() {
		return 1000 * getLengthInKiloMeters();
	}
	public Weight create(double value) {
		return new WeightDeterminedByRoadLengthAndQuality(value);
	}
}
