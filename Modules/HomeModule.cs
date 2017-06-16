using Nancy;
using MusicianTracker.Objects;
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
      // Get["/clients/edit/{id}"] = parameters => {
      //   Client foundClient = Client.Find(parameters.id);
      //   return View["client-edit.cshtml", foundClient];
      // };
      Get["/bands/{id}"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        return View["band-details.cshtml", foundBand];
      };
      Get["/venues/{id}"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        return View["venue-details.cshtml", foundVenue];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["name"]);
        newBand.Save();
        return View["bands.cshtml", Band.GetAll()];
      };
      Post["/bands/{id}/new"] = _ => {
        //TODO: Make this route and its complementary venue route better at distinuishing new venues and existing ones.
        Venue newVenue = new Venue(Request.Form["name"]);
        newVenue.Save();
        Band.Find(parameters.id).AddVenue(newVenue);
        return View["band-details.cshtml", Band.Find(parameters.id)];
      };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["name"]);
        newVenue.Save();
        return View["venues.cshtml", Venue.GetAll()];
      };
      Post["/venues/{id}/new"] = _ => {
        Band newBand = new Band(Request.Form["name"]);
        newBand.Save();
        Venue.Find(parameters.id).AddBand(newBand);
        return View["venue-details.cshtml", Venue.Find(parameters.id)];
      };
      // Post["/stylists/new"] = _ => {
      //   Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
      //   newStylist.Save();
      //   return View["stylist-details.cshtml", newStylist];
      // };
      // Patch["clients/edit/{id}"] = parameters => {
      //   Client foundClient = Client.Find(parameters.id);
      //   foundClient.Update(Request.Form["client-name"]);
      //   return View["client-details.cshtml", foundClient];
      // };
      // Delete["clients/delete/{id}"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string, object> {{"clients", Client.GetAll()}, {"stylists", Stylist.GetAll()}};
      //   Client foundClient = Client.Find(parameters.id);
      //   Client.Delete(foundClient.GetId());
      //   return View["index.cshtml", model];
      // };
    }
  }
}
