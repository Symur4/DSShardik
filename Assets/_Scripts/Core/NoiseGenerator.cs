using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public static class NoiseGenerator
    {
        /// heightMultiplier : The maximum height of the terrain
        /// octaves : Number of iterations (the more there is, the more detailed the terrain will be)
        /// persistance : The higher it is, the rougher the terrain will be (this value should be between 0 and 1 excluded)
        /// lacunarity : The higher it is, the more "feature" the terrain will have (should be strictly positive)
        public static float GetNoiseAt(int x, int z, float scale, float heightMultiplier, int octaves, float persistance, float lacunarity, int seed)
        {
            float PerlinValue = 0f;
            float amplitude = 1f;
            float frequency = 1f;

            for (int i = 0; i < octaves; i++)
            {
                // Get the perlin value at that octave and add it to the sum
                PerlinValue += Mathf.PerlinNoise((x * frequency + seed) * scale,
                    (z * frequency + seed) * scale)
                    * amplitude;

                // Decrease the amplitude and the frequency
                amplitude *= persistance;
                frequency *= lacunarity;
            }

            // Return the noise value
            return PerlinValue * heightMultiplier;
        }


    }
}
