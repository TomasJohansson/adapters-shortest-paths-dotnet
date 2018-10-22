/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code in this "core" project is licensed with MIT.
* Other projects within this Visual Studio solution may be released with other licenses e.g. Apache.
* Please find more information in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and/or in the solution root directory.
* It should also be possible to find more license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

namespace Programmerare.ShortestPaths.Core.Api
{
    /// <summary>
    /// A Weight represents some kind of cost for an Edge, i.e. a cost for going from the start Vertex to the end Vertex of the Edge.
    /// When trying to find the shortest paths between two vertices in a Graph, it is the sum of the weights that are minimized.
    /// In real world scenarios, the cost can be for example distance or time (in some unit) for going between two places (vertices).
    /// </summary>
    public interface Weight : StringRenderable {

        /**
	     * TODO maybe: Improve documentation by 
         * motivating why not a double is simply used everywhere 
         * instead of defining a trivial interface ... 
         * ( and then maybe mention DDD and the book Prefactoring ...) 
	     */

        /// <summary>
        /// the actual numerical value for the weight.
        /// </summary>
        double WeightValue { get; }

        /// <summary>
        /// Factory method not intended to be used by client code, but must of course be implemented if 
        /// you create your own implementation instead of the default implementation.
        /// The purpose is that it will be used by PathFinderBase, for creating the total weight with the same implementing class as the weights in the edges. 
        /// Instead of forcing client code to pass a Weight factory class trough a factory creating the PathFinder,
        /// this factory has been placed here, and then any weight (e.g. the first found) can be used by assuming all edges have the same weight implementation.
        /// When the create method is used, it might be considered as a variant of or at least similar to the design pattern Prototype, 
        /// though not cloning itself with the same value but the value is instead a parameter for the method.
        /// </summary>
        /// <param name="value">the actual numerical value for the weight</param>
        /// <returns>
        /// an instance of Weight which should be of the same class as the implementing class.
	    ///     For example: if "class A implements Weight" then class A should implement the method as something like "return new A(value)"
        /// </returns>
        Weight Create(double value);
    }
}