// Raagav Prasanna 2036159

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PianoSimulation;

namespace PianoSimulationTests
{
  [TestClass]
  public class CircularArrayTests
  {
    // Test method for the Length Property.
    [TestMethod]
    public void TestLength()
    {
      var expected = 5;

      var result = new CircularArray(5).Length;

      Assert.AreEqual(expected, result);
    }

    // Tests whether Fill will throw an exception if the array inputted has a different length than that of the Circular array.
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestFillException()
    {
      CircularArray ca = new CircularArray(4);
      ca.Fill(new double[] { 1, 2, 3 });
    }

    // Tests if each value of the array inputted by Fill, matches that of the array that belongs to the Circular data structure.
    [TestMethod]
    public void TestFill()
    {
      CircularArray ca = new CircularArray(4);
      double[] arr = new double[] { 1, 2, 3, 4 };
      ca.Fill(arr);
      for (int i = 0; i < ca.Length; i++)
      {
        Assert.AreEqual(ca[i], arr[i]);
      }
    }


    // Tests the indexer by making sure it returns that correct value in relation to the Circular nature of the data structure.
    [TestMethod]
    public void testIndexer()
    {
      CircularArray ca = new CircularArray(5);


      ca.Fill(new double[] { 1, 2, 3, 4, 5 });

      ca.Shift(6);

      double expected = 2;

      double result = ca[0];

      Assert.AreEqual(expected, result);

    }

    // Tests the Shift method by ensuring the new value is added to the end of the data structure.
    [TestMethod]
    public void TestShift()
    {
      CircularArray ca = new CircularArray(3);

      ca.Fill(new double[] { 6, 5, 9 });

      ca.Shift(2);

      double expected = 2;

      double result = ca[ca.Length - 1];

      Assert.AreEqual(expected, result);

    }
  }
}
