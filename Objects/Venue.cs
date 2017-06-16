using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace MusicianTracker.Objects
{
  public class Venue
  {
    public int Id  { get; set; }
    public string Name { get; set; }

    public Venue(string name, int id = 0)
    {
      Id = id;
      Name = name;
    }
    public override bool Equals(System.Object otherVenue)
    {
        if (!(otherVenue is Venue))
        {
          return false;
        }
        else
        {
          Venue newVenue = (Venue) otherVenue;
          bool idEquality = (this.Id == newVenue.Id);
          bool nameEquality = (this.Name == newVenue.Name);
          return (idEquality && nameEquality);
        }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName);", conn);

      SqlParameter nameParameter = new SqlParameter("@VenueName", this.Name);
      cmd.Parameters.Add(nameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allVenues;
    }

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = id.ToString();
      cmd.Parameters.Add(venueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;
      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenue;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName OUTPUT INSERTED.name WHERE id = @VenueId;", conn);
      SqlParameter newNameParameter = new SqlParameter("@NewName", newName);
      cmd.Parameters.Add(newNameParameter);
      SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.Id);
      cmd.Parameters.Add(venueIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Name = rdr.GetString(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public void AddBand(Band band)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId);", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.Id;
      cmd.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = band.Id;
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
      conn.Close();
      }
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id)  WHERE venues.id = @VenueId;", conn);

     SqlParameter bandIdParameter = new SqlParameter();
     bandIdParameter.ParameterName = "@VenueId";
     bandIdParameter.Value = this.Id;

     cmd.Parameters.Add(bandIdParameter);
     SqlDataReader rdr = cmd.ExecuteReader();

     List<Band> bands = new List<Band>{};

     while(rdr.Read())
     {
       int bandId = rdr.GetInt32(0);
       string bandName = rdr.GetString(1);

       Band newBand = new Band(bandName, bandId);
       bands.Add(newBand);
     }

     if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
     return bands;
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.Id;

      cmd.Parameters.Add(venueIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();

      cmd = new SqlCommand("DELETE FROM bands_venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
