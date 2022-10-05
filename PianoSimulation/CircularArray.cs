// Raagav Prasanna 2036159
using System;


namespace PianoSimulation
{
  // Circular data structure 
  public class CircularArray : IRingBuffer
  {
    private double[] _samples;
    private int _index;

    // Constructor that takes as input a length argument to initialize array and initializes the pointer index to 0.
    public CircularArray(int length)
    {
      this._samples = new double[length];
      this._index = 0;
    }

    // Property to return the Length of the Data Structure.
    public int Length
    {
      get
      {
        return this._samples.Length;
      }
    }

    // Method to remove the first index and add a value to the end of the data structure.
    public double Shift(double value)
    {
      double retVal = this._samples[this._index];
      this._samples[this._index] = value;
      if (this._index == this._samples.Length - 1)
      {
        this._index = 0;
      }
      else
      {
        this._index++;
      }
      return retVal;
    }

    // Indexer to return value relative to the circular nature of the data structure.
    public double this[int pos]
    {
      get
      {
        return this._samples[(this._index + pos) % this._samples.Length];
      }
    }

    // Takes as input an array of doubles and populates the data structure with these values. 
    public void Fill(double[] array)
    {
      if (array.Length != this._samples.Length)
      {
        throw new ArgumentException("Input array must be of size: " + this._samples.Length);
      }
      for (int i = 0; i < array.Length; i++)
      {
        this._samples[i] = array[i];
      }
    }
  }
}
