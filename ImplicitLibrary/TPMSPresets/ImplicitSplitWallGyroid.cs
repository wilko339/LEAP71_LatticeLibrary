//
// SPDX-License-Identifier: Apache-2.0
//
// The LEAP 71 ShapeKernel is an open source geometry engine
// specifically for use in Computational Engineering Models (CEM).
//
// For more information, please visit https://leap71.com/shapekernel
// 
// This project is developed and maintained by LEAP 71 - © 2023 by LEAP 71
// https://leap71.com
//
// Computational Engineering will profoundly change our physical world in the
// years ahead. Thank you for being part of the journey.
//
// We have developed this library to be used widely, for both commercial and
// non-commercial projects alike. Therefore, have released it under a permissive
// open-source license.
// 
// The LEAP 71 ShapeKernel is based on the PicoGK compact computational geometry 
// framework. See https://picogk.org for more information.
//
// LEAP 71 licenses this file to you under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with the
// License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, THE SOFTWARE IS
// PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED.
//
// See the License for the specific language governing permissions and
// limitations under the License.   
//

using System;
using System.Numerics;
using PicoGK;


namespace Leap71
{
	namespace LatticeLibrary
    {
        public class ImplicitSplitWallGyroid : IImplicit
		{
            protected float         m_fFrequencyScale;
			protected float		    m_fWallThickness;
            protected Vector3       m_vCentrePoint;

            /// <summary>
            /// Helper class for an implicit gyroid pattern.
            /// </summary>
            public ImplicitSplitWallGyroid(float fUnitSize, Vector3 vCentre, float fWallThickness)
			{
                m_fFrequencyScale   = (2f * (float)Math.PI) / fUnitSize;
                m_fWallThickness    = fWallThickness;
                m_vCentrePoint      = vCentre;
            }

			virtual public float fSignedDistance(in Vector3 vecPt)
			{
                double dX = vecPt.X - m_vCentrePoint.X;
                double dY = vecPt.Y - m_vCentrePoint.Y;
                double dZ = vecPt.Z - m_vCentrePoint.Z;

                //calculate the gyroid surface equation
                double dDist =   Math.Sin(m_fFrequencyScale * dX) * Math.Cos(m_fFrequencyScale * dY) +
                                 Math.Sin(m_fFrequencyScale * dY) * Math.Cos(m_fFrequencyScale * dZ) +
                                 Math.Sin(m_fFrequencyScale * dZ) * Math.Cos(m_fFrequencyScale * dX);

                float fFinalDist = (float)Math.Min((float)(Math.Max(dDist, Math.Abs(dDist) - 0.5f * m_fWallThickness)),
                    (float)(Math.Max(-dDist, Math.Abs(dDist) - 0.5f * m_fWallThickness)));

                return fFinalDist;
            }
		}
    }
}