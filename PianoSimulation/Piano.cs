// Raagav Prasanna 2036159
using System;
using System.Collections.Generic;

namespace PianoSimulation
{
  // Class to represent entire Piano.
  public class Piano : IPiano
  {
    private List<IMusicalString> _wires;
    private string _keys;

    // Constructor to initialize all piano keys.
    public Piano(string keys = "q2we4r5ty7u8i9op-[=zxdcfvgbnjmk,.;/' ", int samplingRate = 44100)
    {
      this._keys = keys;
      this._wires = new List<IMusicalString>(keys.Length);
      for (int i = 0; i < keys.Length; i++)
      {
        this._wires.Add(new PianoWire(samplingRate, calcSamplingRate(i)));
      }
    }

    // Private helper method to calculate the sample rate of a given note.
    private double calcSamplingRate(int ind)
    {
      return Math.Pow(2, (ind - 24) / 12.0) * 440;
    }

    // Method to Strike a specific piano key.
    public void StrikeKey(char key)
    {
      int ind = this._keys.IndexOf(key);
      if (ind == -1)
      {
        throw new ArgumentException($"Invalid key {key}");
      }
      this._wires[ind].Strike();
    }

    // Method that returns a frequency that will simulate the sound of a piano key.
    public double Play()
    {
      double sum = 0;
      foreach (var wire in this._wires)
      {
        sum += wire.Sample();
      }
      return sum;
    }

    // Returns a list of descriptions for each note.
    public List<string> GetPianoKeys()
    {
      List<string> wiresDescrip = new List<string>();
      for (int i = 0; i < this._keys.Length; i++)
      {
        wiresDescrip.Add($"{this._keys[i]} : {this._wires[i].NoteFrequency}");
      }
      return wiresDescrip;
    }

    // Property for the keys attribute.
    public string Keys
    {
      get
      {
        return this._keys;
      }
    }
  }
}