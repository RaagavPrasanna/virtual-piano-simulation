// Raagav Prasanna 2036159

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PianoSimulation;

namespace PianoSimulationTests
{
  // Tests for the PianoWire Class
  [TestClass]
  public class PianoWireTests
  {

    // Test Method for NoteFrequency.
    [TestMethod]
    public void TestNoteFrequency()
    {
      PianoWire pw = new PianoWire(44100, 440);

      double expected = 440;

      double result = pw.NoteFrequency;

      Assert.AreEqual(expected, result);
    }

    // Test Method for Number Of Samples.
    [TestMethod]
    public void TestNumberOfSamples()
    {
      PianoWire pw = new PianoWire(44100, 440);

      int expected = 100;

      int result = pw.NumberOfSamples;

      Assert.AreEqual(expected, result);
    }

    // Test for Sample checks the whether the value of Sample converges to zero in a finite manner.
    [TestMethod]
    public void TestSample()
    {
      bool passed = false;

      PianoWire pw = new PianoWire(44100, 440);

      pw.Strike();
      double sampVal = 0;
      for (int i = 0; i < 100000000; i++)
      {
        sampVal = pw.Sample();
        if (sampVal < 0)
        {
          sampVal *= -1;
        }
        if (sampVal < 0.0000001)
        {
          passed = true;
          break;
        }
      }

      if (!passed)
      {
        Assert.Fail("Did not converge to 0.");
      }
    }

    // Test for Strike checks whether the values in the Circular Array are between 0.5 and -0.5
    [TestMethod]
    public void TestStrike()
    {
      PianoWire pw = new PianoWire(44100, 440);

      pw.Strike();
      double sampVal = pw.Sample();
      if (!(sampVal >= -0.5 && sampVal <= 0.5))
      {
        Assert.Fail("Number is out of possible range.");
      }
    }
  }
}