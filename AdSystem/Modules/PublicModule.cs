using System;
using Nancy;
using Newtonsoft.Json;
using System.Text;
using AdSystem.Models;
using AdSystem.ApiObjects;
using System.Xml.Linq;
using Nancy.ModelBinding;
using System.Linq;
using Isopoh.Cryptography.Argon2;
using AdSystem.RTBSystem;

namespace AdSystem.Modules
{
    public class PublicModule : IAdSystemModule
    {
        private static bool IsValidDomainName(string name)
        {
            return Uri.CheckHostName(name) != UriHostNameType.Unknown;
        }
        private static bool IsValidUrl(string url)
        {
            return (Uri.IsWellFormedUriString(url, UriKind.Absolute));
        }
        protected string GetIPAddress()
        {
            string ipAddress = this.Request.Headers["HTTP_X_FORWARDED_FOR"].FirstOrDefault();

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return this.Request.Headers["REMOTE_ADDR"].FirstOrDefault();
        }
        private bool verifyApitype(dynamic args)
        {
            if (string.IsNullOrWhiteSpace(args.apitype))
            {
                return false;
            }
            else
            {
                string apitype = (string)(args.apitype);
                if (apitype != "advertiser" && apitype != "publisher")
                {
                    return false;
                }
            }
            return true;
        }
        public PublicModule(IAppConfiguration appConfig)
        {
            var ctx = new AdSystemDbContext();
            Post("/api/public/login/{apitype}", args =>
            {
                if (!verifyApitype(args)) return 404;
                if (string.IsNullOrWhiteSpace(this.Request.Form.username) || string.IsNullOrWhiteSpace(this.Request.Form.password))
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "Username or password was not provided.");
                }
                else
                {
                    string username = (string)(this.Request.Form.username);
                    string password = (string)(this.Request.Form.password);
                    string apitype = (string)(args.apitype);
                    UserAccount account = null;
                    if (apitype == "publisher")
                    {
                        Publisher pub = db.Publishers.Where(a => a.account.username == username).FirstOrDefault();
                        if (pub == null) return ErrorResponse(HttpStatusCode.Forbidden, "AccessViolationException", "Account with provided username does not exist.");
                        account = db.Accounts.Where(a => a.id == pub.accountId).FirstOrDefault();

                    } else if (apitype == "advertiser")
                    {
                        Advertiser adv = db.Advertisers.Where(a => a.account.username == username).FirstOrDefault();
                        if (adv == null) return ErrorResponse(HttpStatusCode.Forbidden, "AccessViolationException", "Account with provided username does not exist.");
                        account = db.Accounts.Where(a => a.id == adv.accountId).FirstOrDefault();
                    } else
                    {
                        return ErrorResponse(HttpStatusCode.Forbidden, "AccessViolationException", "Wrong API type.");
                    }
                    if (Argon2.Verify(account.password, password))
                    {
                        account.apiKey = Guid.NewGuid();
                        db.SaveChanges();
                        var data = new LoginData(account.apiKey.ToString("N"));
                        return SuccessResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return ErrorResponse(HttpStatusCode.Forbidden, "AccessViolationException", "Incorrect password was provided.");
                    }
                }
            });
            Post("/api/public/register/{apitype}", args =>
            {
                if (!verifyApitype(args)) return 404;
                if (string.IsNullOrWhiteSpace(this.Request.Form.username) || string.IsNullOrWhiteSpace(this.Request.Form.password))
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "Username or password was not provided.");
                } else
                {
                    string username = (string)(this.Request.Form.username);
                    string password = (string)(this.Request.Form.password);
                    string apitype = (string)(args.apitype);
                    if (username.Length >= 4 && username.Length <= 32)
                    {
                        if (db.Accounts.Where(a => a.username == username).FirstOrDefault() != null)
                        {
                            return ErrorResponse(HttpStatusCode.Forbidden, "ArgumentException", "Account with that username already exists.");
                        }
                        if (apitype == "publisher")
                        {
                            if (string.IsNullOrWhiteSpace(this.Request.Form.domain) || string.IsNullOrWhiteSpace(this.Request.Form.categories))
                            {
                                return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "You need to specify valid domain and categories.");
                            }
                            if(!IsValidDomainName(this.Request.Form.domain))
                            {
                                return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "Provided domain is in invalid format.");
                            }
                            string[] cats = ((string)(this.Request.Form.categories)).Split(',');
                            foreach(string cat in cats)
                            {
                                if(!Program.config.rtbConfig.categories.ContainsKey(cat))
                                    return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "One of the provided categories is not compatibile with the OpenRTB format.");
                            }
                                UserAccount acc = new UserAccount();
                                acc.username = username;
                                acc.password = Argon2.Hash(password);
                                acc.apiKey = Guid.NewGuid();
                                db.SaveChanges();
                                Publisher publisherAccount = new Publisher();
                                publisherAccount.account = acc;
                                publisherAccount.categories = string.Join(",", cats);
                                publisherAccount.domain = this.Request.Form.domain;
                                db.Publishers.Add(publisherAccount);
                                db.SaveChanges();
                                LoginData data = new LoginData(acc.apiKey.ToString("N"));
                                return SuccessResponse(HttpStatusCode.Created, data);
                        }
                        else if (apitype == "advertiser")
                        {
                            if (string.IsNullOrWhiteSpace(this.Request.Form.openRtbUrl))
                            {
                                return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "You need to specify valid OpenRTB api url.");
                            }
                            if (!IsValidUrl(this.Request.Form.openRtbUrl))
                            {
                                return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "Provided OpenRTB api url is in invalid format.");
                            }
                            else
                            {
                                UserAccount acc = new UserAccount();
                                acc.username = username;
                                acc.password = Argon2.Hash(password);
                                acc.apiKey = Guid.NewGuid();
                                Advertiser advertiserAccount = new Advertiser();
                                advertiserAccount.account = acc;
                                advertiserAccount.openRtbUrl = this.Request.Form.openRtbUrl;
                                db.Advertisers.Add(advertiserAccount);
                                db.SaveChanges();
                                LoginData data = new LoginData(acc.apiKey.ToString("N"));
                                return SuccessResponse(HttpStatusCode.Created, data);
                            }
                        } else
                        {
                            return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "Incorrect API type (publisher/advertiser)");
                        }
                    } else
                    {
                        return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "Username length has to be between 4 and 32 characters.");
                    }
                }
            });
            Get("/api/public/ad", _ =>
            {
                int pubid;
                if (!int.TryParse(this.Request.Query.publisherid, out pubid))
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "You need to specify valid publisher id.");
                }
                Publisher publisher = db.Publishers.Where(a => a.id == pubid).FirstOrDefault();
                if (publisher == null)
                {
                    return ErrorResponse(HttpStatusCode.BadRequest, "FormatException", "Provided publisher does not exist.");
                }
                RTBClient rtbclient = new RTBClient(this.Request.Headers, this.db, publisher, GetIPAddress(), this.Request.Url);
                var adbid = rtbclient.GetAdBidResponse();
                PublisherAdvertisement pubad;
                if (adbid == null)
                {
                    pubad = new PublisherAdvertisement();
                } else
                {
                    pubad = new PublisherAdvertisement(adbid, publisher);
                }
                
                return SuccessResponse(HttpStatusCode.OK, pubad);
            });
        }
    }
}
