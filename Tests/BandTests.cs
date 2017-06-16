using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using MusicianTracker.Objects;

namespace MusicianTracker
{
  [Collection("MusicianTrackerTests")]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
    }
    [Fact]
    public void Band_Equal_ReturnsTrueForIdenticalObjects()
    {
      //Arrange, Act
      Band firstBand = new Band("David Bowie");
      Band secondBand = new Band("David Bowie");
      //Assert
      Assert.Equal(firstBand, secondBand);
    }
    [Fact]
    public void Band_BandsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Band_Save_SavesBandToDatabase()
    {
      //Arrange
      Band testBand = new Band("Lightning Bolt");
      testBand.Save();
      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};
      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Band_Find_FindsBandInDatabase()
    {
      //Arrange
      Band testBand = new Band("Kazumoto Endo");
      testBand.Save();
      //Act
      Band foundBand = Band.Find(testBand.Id);
      //Assert
      Assert.Equal(testBand, foundBand);
    }
    [Fact]
    public void Band_Update_UpdatesBandNameInDatabase()
    {
      //Arrange
      Band testBand = new Band("Farmakon");
      testBand.Save();
      string newName = "Pharmakon";
      //Act
      testBand.Update(newName);
      string result = testBand.Name;
      //Assert
      Assert.Equal(newName, result);
    }
    [Fact]
    public void Band_Delete_RemovesBandFromDatabase()
    {
      //Arrange
      Band testCase1 = new Band("Nick Cave");
      testCase1.Save();
      Band testCase2 = new Band("Lou Reed");
      testCase2.Save();
      //Act
      testCase1.Delete();
      int actual = Band.GetAll().Count;
      int expected = 1;
      //Assert
      Assert.Equal(expected, actual);
    }
    [Fact]
    public void Band_DeleteAll_RemovesAllBandsFromDatabase()
    {
      //Arrange
      Band testCase1 = new Band("Carpenter Brut");
      testCase1.Save();
      Band testCase2 = new Band("Aphex Twin");
      testCase2.Save();
      //Act
      Band.DeleteAll();
      int actual = Band.GetAll().Count;
      int expected = 0;
      //Assert
      Assert.Equal(expected, actual);
    }
    [Fact]
    public void Venue_AddBand_AddsBandAssociationToVenuesBands()
    {
      //Arrange
      Venue testVenue1 = new Venue("Holocene");
      testVenue1.Save();
      Venue testVenue2 = new Venue("The Roseland");
      testVenue2.Save();
      Band testBand = new Band("Surfer Blood");
      testBand.Save();
      //Act
      testVenue.AddBand(testVenue1);
      testVenue.AddBand(testVenue2);
      List<Venue> actual = testBand.GetVenues();
      List<Venue> expected = new List<Venue> {testVenue1, testVenue2};
      //Assert
      Assert.Equal(expected, actual);
    }
  }
}
