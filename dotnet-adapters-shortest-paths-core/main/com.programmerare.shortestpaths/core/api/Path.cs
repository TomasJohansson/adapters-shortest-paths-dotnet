/*
* Copyright (c) Tomas Johansson , http://www.programmerare.com
* The code is made available under the terms of the MIT License.
* https://github.com/TomasJohansson/adapters-shortest-paths/blob/master/adapters-shortest-paths-core/License.txt
*/

using com.programmerare.shortestpaths.core.api.generics;

namespace com.programmerare.shortestpaths.core.api
{
    /**
     * @see PathGenerics 
     * @author Tomas Johansson
     */
    public interface Path : PathGenerics< Edge , Vertex , Weight > {}
}