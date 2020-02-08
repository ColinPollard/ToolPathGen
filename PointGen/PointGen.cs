//Author: Colin Pollard 2/7/2020
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointGen
{
    /// <summary>
    /// Internal class used to read csv file and do core math.
    /// </summary>
    public class PointGen
    {
        public delegate void SuccessHandler(double time, int steps);
        public event SuccessHandler success;

        string filePath, outputPath;
        int maxPoints;
        int numPaths;
        double pathHeight;
        double timeStep;
        double velocity;

        //List of <x,y,z> points.
        List<Tuple<double, double, double>> points;

        //Lists for tool path x,y,z
        List<double> xPoints, yPoints, zPoints;

        double estimatedTime;
        int toolPathPoints;

        /// <summary>
        /// Creates a new PointGen object.
        /// </summary>
        /// <param name="filepath"></param>
        public PointGen(string filepath, string outputPath)
        {
            this.filePath = filepath;
            this.outputPath = outputPath;

            //Instantiate new lists for point holding.
            points = new List<Tuple<double, double, double>>();
            xPoints = new List<double>();
            yPoints = new List<double>();
            zPoints = new List<double>();

            estimatedTime = 0;
            toolPathPoints = 0;
        }

        /// <summary>
        /// Runs toolpath generation.
        /// </summary>
        public void Run()
        {
            //Load the points from the file, error check.
            ReadFromFile();

            //Generate points from keypoints
            GeneratePaths();

            //Create file from toolpaths
            SaveFile();

            success(estimatedTime, toolPathPoints);
        }

        /// <summary>
        /// Reads from the selected csv file, generates point list and parameters.
        /// </summary>
        public void ReadFromFile()
        {

            using (var reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                //Check to see if all header file information is there
                if (values.Length < 5)
                    throw new ArgumentException("Too few header details, check to make sure in format of: MaxPoints, NumPaths, PathHeight, TimeStep, Velocity.");

                maxPoints = Int32.Parse(values[0]);
                numPaths = Int32.Parse(values[1]);
                pathHeight = Double.Parse(values[2]);
                timeStep = Double.Parse(values[3]);
                velocity = Double.Parse(values[4]);

                //Loop through each line of the csv reading points
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    values = line.Split(',');

                    points.Add(new Tuple<double, double, double>(Double.Parse(values[0]), Double.Parse(values[1]), Double.Parse(values[2])));
                }
            }
        }

        /// <summary>
        /// Generates list of time-stepped points from keypoints.
        /// </summary>
        public void GeneratePaths()
        {
            //Make sure points are populated.
            if (points.Count < 2)
                throw new Exception("Too few points, make sure to read from file containing more than one point first.");

            double subPassTime;
            int subPassSteps;

            //Iterate through z paths
            for(int z = 0; z < numPaths; z++)
            {
                //Iterate through points, calculate paths between.
                for (int i = 1; i < points.Count; i++)
                {
                    //Calculate time from length/velocity, steps from time/timestep
                    subPassTime = Math.Sqrt(Math.Pow(points[i].Item1 - points[i - 1].Item1, 2) + Math.Pow(points[i].Item2 - points[i - 1].Item2, 2) + Math.Pow(points[i].Item3 - points[i - 1].Item3, 2)) / velocity;
                    subPassSteps = (int)(subPassTime / timeStep);


                    for (int j = 0; j < subPassSteps; j++)
                    {
                        //X = ((NewX-OldX)/steps * current step) + oldX
                        xPoints.Add((((points[i].Item1 - points[i - 1].Item1) / subPassSteps) * j) + points[i - 1].Item1);
                        yPoints.Add((((points[i].Item2 - points[i - 1].Item2) / subPassSteps) * j) + points[i - 1].Item2);
                        zPoints.Add((((points[i].Item3 - points[i - 1].Item3) / subPassSteps) * j) + points[i - 1].Item3 + (z * pathHeight));
                    }

                    //Update running tallies.
                    estimatedTime += subPassTime;
                    toolPathPoints += subPassSteps;
                }
            }
            if (toolPathPoints > maxPoints)
                throw new Exception("Number of points generated exceeds maximum allowed. Try reducing timestep to decrease points.");
        }

        /// <summary>
        /// Generates CSV file from toolpath.
        /// </summary>
        public void SaveFile()
        {
            //Write XPoints
            using (StreamWriter w = new StreamWriter(outputPath + @"/X.csv"))
            {
                w.WriteLine(String.Join(",", xPoints));
            }
            //Write YPoints
            using (StreamWriter w = new StreamWriter(outputPath + @"/Y.csv"))
            {
                w.WriteLine(String.Join(",", yPoints));
            }
            //Write ZPoints
            using (StreamWriter w = new StreamWriter(outputPath + @"/Z.csv"))
            {
                w.WriteLine(String.Join(",", zPoints));
            }
        }
    }
}
