using Nancy;
using MusicianTracker.Objects;
using System;
using System.Collections.Generic;

namespace MusicianTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/bands"] = _ => {
        return View["bands.cshtml", Band.GetAll()];
      };
      Get["/venues"] = _ => {
        return View["venues.cshtml", Venue.GetAll()];
      };
      Get["/bands/{id}"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        return View["band-details.cshtml", foundBand];
      };
      Get["/venues/{id}"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        return View["venue-details.cshtml", foundVenue];
      };
      Get["/bands/random"] = _ => {
        List<Band> allBands = Band.GetAll();
        int rnd = new Random().Next(0, allBands.Count);
        Band randomBand = allBands[rnd];
        return View["band-details.cshtml", randomBand];
      };
      Post["/bands/new"] = _ => {
        if (Request.Form["name"] != "") {
          Band newBand = new Band(Request.Form["name"]);
          newBand.Save();
        }
        return View["bands.cshtml", Band.GetAll()];
      };
      Post["/bands/{id}/new"] = parameters => {
        //TODO: Make this route and its complementary venue route better at distinuishing new venues and existing ones.
        Venue newVenue = new Venue(Request.Form["name"]);
        newVenue.Save();
        Band.Find(parameters.id).AddVenue(newVenue);
        return View["band-details.cshtml", Band.Find(parameters.id)];
      };
      Post["/venues/new"] = _ => {
        if (Request.Form["name"] != "") {
          Venue newVenue = new Venue(Request.Form["name"]);
          newVenue.Save();
        }
        return View["venues.cshtml", Venue.GetAll()];
      };
      Post["/venues/{id}/new"] = parameters => {
        Band newBand = new Band(Request.Form["name"]);
        newBand.Save();
        Venue.Find(parameters.id).AddBand(newBand);
        return View["venue-details.cshtml", Venue.Find(parameters.id)];
      };
    }
  }
}
