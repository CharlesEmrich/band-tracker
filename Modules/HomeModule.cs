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
      // Get["/clients"] = _ => {
      //   Dictionary<string, object> model = new Dictionary<string, object> {{"clients", Client.GetAll()}, {"stylists", Stylist.GetAll()}};
      //   return View["clients.cshtml", model];
      // };
      // Get["/stylists"] = _ => {
      //   Dictionary<string, object> model = new Dictionary<string, object> {{"clients", Client.GetAll()}, {"stylists", Stylist.GetAll()}};
      //   return View["stylists.cshtml", model];
      // };
      // Get["/clients/edit/{id}"] = parameters => {
      //   Client foundClient = Client.Find(parameters.id);
      //   return View["client-edit.cshtml", foundClient];
      // };
      // Get["/clients/view/{id}"] = parameters => {
      //   Client foundClient = Client.Find(parameters.id);
      //   return View["client-details.cshtml", foundClient];
      // };
      // Get["/stylists/view/{id}"] = parameters => {
      //   Stylist foundStylist = Stylist.Find(parameters.id);
      //   return View["stylist-details.cshtml", foundStylist];
      // };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["name"]);
        newBand.Save();
        return View["bands.cshtml", Band.GetAll()];
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
