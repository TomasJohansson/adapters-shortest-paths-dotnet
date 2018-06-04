/**
 * Generally reusable implementations for some of the interfaces in the core api.
 * Note that there are no such general implementations for PathFinder and PathFinderFactory,
 * but those should instead be implemented in implementing Adapter libraries.
 * (however, there is a base class for PathFinderFactory, but most of the implementation need to provided in another library)
 * There is also an EdgeMapper class in this package, which does not quite belong here
 * (according to the semantic of the package name, but want it here because of package level access from PathFinderFactoryBase) 
 */
/**
 * @author Tomas Johansson
 *
 */
package com.programmerare.shortestpaths.core.impl;