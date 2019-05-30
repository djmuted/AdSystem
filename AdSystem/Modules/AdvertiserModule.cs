using System;
using Nancy;
using Newtonsoft.Json;
using System.Text;
using AdSystem.Models;
using AdSystem.ApiObjects;
using System.Xml.Linq;
using Nancy.ModelBinding;
using Nancy.Authentication.Stateless;
using AdSystem.NancySettings;
using System.Security.Claims;
using System.Linq;
using System.Drawing;
using System.IO;

namespace AdSystem.Modules
{
    public class AdvertiserModule : IAdSystemModule
    {
        private Advertiser advertiser;
        public AdvertiserModule() : base ("/api/advertiser")
        {
            Before += ctx => {
                Guid apiKey;
                if (Guid.TryParse(ctx.Request.Headers.Authorization, out apiKey))
                {
                    advertiser = db.Advertisers.Where(a => a.account.apiKey == apiKey).FirstOrDefault();
                }
                if (advertiser == null)
                {
                    return ErrorResponse(HttpStatusCode.Unauthorized, "UnauthorizedAccessException", "API key is invalid or expired.");
                }
                return null;
            };
            Post("/ads", _ => {
                var postedFile = Request.Files.FirstOrDefault();
                if (postedFile == null)
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "FileNotFoundException", "No ad image was provided during the request.");
                }
                if (!string.IsNullOrWhiteSpace(this.Request.Form.name))
                {
                    string myname = (string)(this.Request.Form.name);
                    if(myname.Length <= 32)
                    {
                        Image img;
                        try
                        {
                            img = Bitmap.FromStream(postedFile.Value);
                        } catch(Exception ex)
                        {
                            return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "The ad image format was not recognized."+ex);
                        }
                        if(img.Width == 728 && img.Height == 90)
                        {
                            var ad = new Ad() { name = myname, advertiser = advertiser };

                            db.Ads.Add(ad);
                            db.SaveChanges();

                            img.Save(Path.Combine("adimg", ad.id.ToString("N")+".png"));

                            return SuccessResponse(HttpStatusCode.Created, ad);
                        } else
                        {
                            return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "The ad image dimensions have to be 728x90 pixels.");
                        }
                    } else
                    {
                        return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "The ad name has needs to have its length between 1 and 32 characters.");
                    }
                }
                return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "The ad name was not provided.");

            }, name: "AddAd");
            Get("/ads", _ => {
                int page;
                int pageSize;
                if (!int.TryParse(this.Request.Query["page"], out page))
                {
                    page = 1;
                }
                if (!int.TryParse(this.Request.Query["pageSize"], out pageSize))
                {
                    pageSize = 10;
                }
                if (pageSize < 1 || pageSize > 100)
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "IndexOutOfRangeException", "Page size has to be an integer between 1 and 100.");
                }

                if (page < 1)
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "IndexOutOfRangeException", "Incorrect page number, it cannot be smaller than 1.");
                }
                var ret = new response(Pages.GetPaged<Ad>(db.Ads.Where(a => a.advertiser.id == advertiser.id), page, pageSize));
                return Serialize(ret);
            }, name: "GetAds");
            Get("/ads/{guid}", _ => {
                Guid guid;
                if (!Guid.TryParse(_.guid, out guid))
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "The key format is invalid.");
                }
                Ad ad = db.Ads.Where(a => a.advertiser.id == advertiser.id && a.id == guid).FirstOrDefault();
                if (ad != null)
                {
                    return SuccessResponse(HttpStatusCode.OK, ad);
                }
                else
                {
                    return ErrorResponse(HttpStatusCode.NotFound, "ObjectNotFoundException", "Ad with specified id was not found.");
                }
            }, name: "GetAd");
            Delete("/ads/{guid}", _ => {
                Guid guid;
                if (!Guid.TryParse(_.guid, out guid))
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "The key format is invalid.");
                }
                Ad ad = db.Ads.Where(a => a.advertiser.id == advertiser.id && a.id == guid).FirstOrDefault();
                db.Ads.Remove(ad);
                db.SaveChanges();
                if (ad != null)
                {
                    return SuccessResponse(HttpStatusCode.OK, ad);
                }
                else
                {
                    return ErrorResponse(HttpStatusCode.NotFound, "ObjectNotFoundException", "Ad with specified id was not found.");
                }
            }, name: "DeleteAd");
        }
    }
}
