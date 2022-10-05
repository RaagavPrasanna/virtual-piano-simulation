// Raagav Prasanna 2036159

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PianoSimulation;

namespace PianoSimulation
{
  [TestClass]
  public class PianoWireTests
  {

    // Tests whether the StrikeKey method throws an exception if given an invalid key.
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestStrikeKeyException()
    {
      Piano p = new Piano();
      p.StrikeKey('s');
    }
  }
}