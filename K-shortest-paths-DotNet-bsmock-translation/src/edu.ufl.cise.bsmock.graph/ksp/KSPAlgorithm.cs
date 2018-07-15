/*
* Regarding the license (Apache), please find more information 
* in the files "License.txt" and "NOTICE.txt" 
* in the project root directory and also license information at this URL:
* https://github.com/TomasJohansson/adapters-shortest-paths-dotnet/
*/

using edu.ufl.cise.bsmock.graph.util;
using System;
using System.Collections.Generic;

namespace edu.ufl.cise.bsmock.graph.ksp {
    /**
     * Interface class for K-shortest path algorithms.
     *
     * Copyright (C) 2015  Brandon Smock (dr.brandon.smock@gmail.com, GitHub: bsmock)
     *
     * This program is free software: you can redistribute it and/or modify
     * it under the terms of the GNU General Public License as published by
     * the Free Software Foundation, either version 3 of the License, or
     * (at your option) any later version.
     *
     * This program is distributed in the hope that it will be useful,
     * but WITHOUT ANY WARRANTY; without even the implied warranty of
     * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
     * GNU General Public License for more details.
     *
     * You should have received a copy of the GNU General Public License
     * along with this program.  If not, see <http://www.gnu.org/licenses/>.
     *
     * Created by Brandon Smock on December 24, 2015.
     * Last updated by Brandon Smock on December 24, 2015.
     */
    public interface KSPAlgorithm {
        bool IsLoopless();

        IList<Path> Ksp(Graph graph, String sourceLabel, String targetLabel, int K);
    }
}