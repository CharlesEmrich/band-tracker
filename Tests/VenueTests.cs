using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("BandTrackerTests")]
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Venue.DeleteAll();
    }
    [Fact]
    public void Venue_Equal_ReturnsTrueForIdenticalObjects()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("Crystal Ballroom");
      Venue secondVenue = new Venue("Crystal Ballroom");
      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }
    [Fact]
    public void Venue_VenuesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Venue_Save_SavesVenueToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Star Theatre");
      testVenue.Save();
      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};
      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Venue_Find_FindsVenueInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("The Lovecraft");
      testVenue.Save();
      //Act
      Venue foundVenue = Venue.Find(testVenue.Id);
      //Assert
      Assert.Equal(testVenue, foundVenue);
    }
    [Fact]
    public void Venue_Update_UpdatesVenueNameInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("Holecene");
      testVenue.Save();
      string newName = "Holocene";
      //Act
      testVenue.Update(newName);
      string result = testVenue.Name;
      //Assert
      Assert.Equal(newName, result);
    }
    [Fact]
    public void Venue_Delete_RemovesVenueFromDatabase()
    {
      //Arrange
      Venue testCase1 = new Venue("Aladdin Theatre");
      testCase1.Save();
      Venue testCase2 = new Venue("Doug Fir Lounge");
      testCase2.Save();
      //Act
      testCase1.Delete();
      int actual = Venue.GetAll().Count;
      int expected = 1;
      //Assert
      Assert.Equal(expected, actual);
    }
  }
}
