// Raagav Prasanna 2036159

using System;

namespace PianoSimulation
{
  // Class to Represent a specific PianoWire for a given note.
  public class PianoWire : IMusicalString
  {
    private CircularArray _noteSamples;
    private double _noteFrequency;

    // Class that takes as input a sampling rate and note Frequency, to initialize the circular data structure.
    public PianoWire(double samplingRate, double noteFrequency)
    {
      this._noteSamples = new CircularArray((int)(samplingRate / noteFrequency));
      this._noteFrequency = noteFrequency;
    }

    // Property to return the frequency of the note.
    public double NoteFrequency
    {
      get
      {
        return this._noteFrequency;
      }
    }

    // Property to return the length of the circular data structure.
    public int NumberOfSamples
    {
      get
      {
        return this._noteSamples.Length;
      }
    }

    // Method that creates a double array full of values between -0.5 and 0.5 to populate the circular array. 
    public void Strike()
    {
      Random rand = new Random();


      double[] samples = new double[this._noteSamples.Length];

      for (int i = 0; i < samples.Length; i++)
      {
        samples[i] = rand.NextDouble() - 0.5;
      }

      this._noteSamples.Fill(samples);
    }

    // Method that performs the shifting of the circular array, and adds the average of the first two indexes at the end.
    public double Sample(double decay = 0.996)
    {
      return this._noteSamples.Shift(decay * ((this._noteSamples[0] + this._noteSamples[1]) / 2));
    }
  }
}