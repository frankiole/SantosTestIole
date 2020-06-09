/***********************
 * Name: Frank Iole
 * Date: 6.8.2020
 * File: Program.cs
 * Desc: A program that filters and sorts doubles into histogram bins
 * Input: inputTXT, input text file path; outputTXT, output histogram text file path
 **********************/

using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        /*****************
         * Method: Histogram
         * Desc: Computes histogram data for a specified amount of bins
         * Input: vals, a double List containing data; bins, an integer number of bins
         * Output: an int List containing computed histogram data
         * **************/

        static List<int> Histogram(List<double> vals, int bins = 255)
        {
            vals.Sort(); // puts the data in ascending order

            double min = vals[0]; // the minimum of the set is the first value
            double max = vals[^1]; // the maximum of the set is the last value
            double interval = (max - min) / bins; // the interval is the range of data divided by the number of bins
            double highBound = min; // initializes high bound to lowest number

            List<int> hist = new List<int> {}; // allocates space for histogram data to be computed

            for (int i = 0; i < vals.Count; i++) // loops through all numbers in data set
            {
                if (vals[i] >= highBound) // to detect when a new highBound needs to be set
                { // the first value in the set will always fall into this loop since the highBound is the lowest number
                    while (vals[i] >= highBound) // loops until the value lands in its appropriate bin
                    {
                        highBound += interval; // highBound is raised until it is higher than the compared value
                        hist.Add(0); // appends initial values for all bins specified in the input
                    }
                }

                hist[^1]++; // adds 1 to tally the number of datapoints in each bin

            }

            return hist; // returns calculated data
        }

        /*****************
         * Method: Plot
         * Desc: Prints a histogram on the command window; the lower bin bound is located on the same line, and the upper bound is located a line below.
         * Input: vals, a double List containing data; bins, an integer number of bins
         * Output: none
         * **************/

        static void Plot(List<double> vals, int bins = 255)
        {
            vals.Sort();

            double min = vals[0];
            double max = vals[^1];
            double interval = (max - min) / bins;
            double axis = min;

            foreach (int i in Histogram(vals))
            {
                Console.Write(axis.ToString("N6"));
                Console.Write("| ");
                for (int j = 0; j < i; j++)
                {
                    Console.Write("▒"); // prints blocks corresponding to amount of points in each bin
                }
                Console.Write("\n");
                axis += interval;
            }
        }

        static void Main(string[] args)
        {
            // hardcoded text file paths
            string inputTXT = @"C:\Users\fiole\source\repos\ConsoleApp1\ConsoleApp1\PreEmploymentTaskData.txt";
            string outputTXT = @"C:\Users\fiole\source\repos\ConsoleApp1\ConsoleApp1\Solved.txt";

            var nums = System.IO.File.ReadLines(inputTXT); // reads input text file path

            List<double> values = new List<double> {}; // allocates space for values stored in the text file

            foreach (var i in nums) // hits each datapoint in initial read
            {
                double num = System.Convert.ToDouble(i);
                if(num != 5)
                {
                    values.Add(num); // filters out 5's and stores datapoints to values List
                }
            }

            List<int> histogram = Histogram(values);

            TextWriter tw = new StreamWriter(outputTXT); // prepares the text writer

            foreach (double i in histogram) // writes calculated histogram data for 255 bins
            {
                tw.WriteLine(i);
            }

            tw.Close(); // closes writer

            // BONUS

            Plot(values); // plots a histogram (see Plot method)

        }
    }
}
