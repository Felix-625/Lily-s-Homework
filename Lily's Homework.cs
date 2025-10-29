using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'lilysHomework' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static int lilysHomework(List<int> arr)
    {
        // Calculate swaps needed for ascending order
        int ascSwaps = CalculateSwaps(new List<int>(arr), true);
        
        // Calculate swaps needed for descending order  
        int descSwaps = CalculateSwaps(new List<int>(arr), false);
        
        // Return the minimum of both
        return Math.Min(ascSwaps, descSwaps);
    }
    
    private static int CalculateSwaps(List<int> arr, bool isAscending)
    {
        // Create sorted version based on order
        List<int> sorted;
        if (isAscending)
        {
            sorted = arr.OrderBy(x => x).ToList();
        }
        else
        {
            sorted = arr.OrderByDescending(x => x).ToList();
        }
            
        // Create value to index mapping for original array
        Dictionary<int, int> valueToIndex = new Dictionary<int, int>();
        for (int i = 0; i < arr.Count; i++)
        {
            valueToIndex[arr[i]] = i;
        }
        
        // Create a copy to work with
        List<int> arrCopy = new List<int>(arr);
        int swaps = 0;
        
        // Perform swaps to match the target sorted order
        for (int i = 0; i < arrCopy.Count; i++)
        {
            // If current position doesn't have the correct element
            if (arrCopy[i] != sorted[i])
            {
                swaps++;
                
                // Find where the correct element is currently located
                int correctElement = sorted[i];
                int currentPosition = valueToIndex[correctElement];
                
                // Swap elements in the array copy
                int temp = arrCopy[i];
                arrCopy[i] = arrCopy[currentPosition];
                arrCopy[currentPosition] = temp;
                
                // Update the index mapping
                valueToIndex[arrCopy[currentPosition]] = currentPosition;
                valueToIndex[arrCopy[i]] = i;
            }
        }
        
        return swaps;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        int result = Result.lilysHomework(arr);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
