using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using HtmlAgilityPack;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class MobileController : BaseController
    {
        private readonly IMobileBusinessService _mobileBusinessService;
        private readonly IBrandBusinessService _brandBusinessService;
        public MobileController(IMobileBusinessService mobileBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService, IBrandBusinessService brandBusinessService) : base(configurationManager, authorizationService)
        {
            _mobileBusinessService = mobileBusinessService;
            _brandBusinessService = brandBusinessService;
        }

        // GET: Mobile
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Apartment/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            HttpContext.Server.ScriptTimeout = 300000000;
            var brandResult = await _brandBusinessService.RetrieveBrands();
            var singleBrand = brandResult.Items.Where(e => e.BrandId > 281).ToList();
            foreach (var item in singleBrand)
            {
                var newList = new List<Brand>() { item };
                var mobileList = CreateMobileData(newList);
                await _mobileBusinessService.CreateMobile(mobileList);
            }

            var brands = await _brandBusinessService.RetrieveBrands();
            var brandList = brands.Items.ToList();
            var viewModel = new MobileViewModel()
            {
                Mobile = new Mobile(),
                Brands = new SelectList(brandList, "BrandId", "Name")
            };
            return View(viewModel);
        }

        // POST: Apartment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MobileViewModel mobileViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Apartment
                var result = await _mobileBusinessService.CreateMobile(mobileViewModel.Mobile);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Exception);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(mobileViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _mobileBusinessService.RetrieveMobiles(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _mobileBusinessService.Search(searchKeyword, orderBy, paging));
        }

        private List<Mobile> CreateMobileData(List<Brand> brands)
        {
            // var brands = GetAllBrandlink();

            //For eg acer phone

            var mobileList = new List<Mobile>();
            foreach (var brand in brands)
            {
                var htmlData = GetHtmlData(string.Format("http://www.gsmarena.com{0}{1}", "/", brand.Link));
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlData);

                foreach (var item in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'makers')]"))
                {
                    //Get Data For FirstPage
                    foreach (var element in item.SelectNodes(".//li"))
                    {
                        var links = element.Descendants("a").ToList()[0].Attributes[0].Value;
                        var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
                        var htmlMobileDetailDocument = new HtmlDocument();
                        htmlMobileDetailDocument.LoadHtml(GetHtmlData(appendLink));
                        var testingObject = htmlMobileDetailDocument.DocumentNode.SelectNodes("//*[@data-spec]");
                        //  var columnName = testingObject.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "modelname"));
                        mobileList.Add(CreateMobile(testingObject, brand.BrandId));

                    }

                    //Get data For Next Pages
                    if (htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'nav-pages')]") != null)
                    {
                        foreach (var nextPageItem in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'nav-pages')]"))
                        {
                            var linkForNextPage = nextPageItem.Descendants("a").ToList()[0].Attributes[0].Value;
                            var appendLinkForNextPage =
                                string.Format("http://www.gsmarena.com{0}{1}", "/", linkForNextPage);
                            var htmlMobileNextPageDetailDocument = new HtmlDocument();
                            htmlMobileNextPageDetailDocument.LoadHtml(GetHtmlData(appendLinkForNextPage));
                            foreach (var nextPageitem in htmlMobileNextPageDetailDocument.DocumentNode.SelectNodes(
                                "//div[contains(@class, 'makers')]"))
                            {
                                //Get Data For FirstPage
                                foreach (var element in nextPageitem.SelectNodes(".//li"))
                                {
                                    var links = element.Descendants("a").ToList()[0].Attributes[0].Value;
                                    var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
                                    var htmlMobileDetailDocument = new HtmlDocument();
                                    htmlMobileDetailDocument.LoadHtml(GetHtmlData(appendLink));
                                    var testingObject =
                                        htmlMobileDetailDocument.DocumentNode.SelectNodes("//*[@data-spec]");
                                    //  var columnName = testingObject.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "modelname"));
                                    mobileList.Add(CreateMobile(testingObject,brand.BrandId));
                                }
                            }
                        }
                    }
                }
            }
            return mobileList;
        }

        public Mobile CreateMobile(HtmlNodeCollection htmlNodeCollection,int brandId)
        {
            var modelName = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "modelname"));
            var released = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "released-hl"));
            var body = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "body-hl"));
            var os = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "os-hl"));
            var storage = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "storage-hl"));
            var displayres = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "displayres-hl"));
            var camerapixels = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "camerapixels-hl"));
            var videopixels = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "videopixels-hl"));
            var ramsize = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "ramsize-hl"));
            var batsize = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "batsize-hl"));
            var battypehl = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "battype-hl"));
            var comment = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "comment"));
            var nettech = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "nettech"));
            var net2g = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "net2g"));
            var net3g = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "net3g"));
            var net4g = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "net4g"));
            var speed = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "speed"));
            var gprstext = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "gprstext"));
            var edge = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "edge"));
            var year = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "year"));
            var status = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "status"));
            var dimensions = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "dimensions"));
            var weight = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "weight"));
            var sim = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "sim"));
            var displaytype = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "displaytype"));
            var displayresolution = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "displayresolution"));
            var chipset = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "chipset"));
            var cpu = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "cpu"));
            var gpu = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "gpu"));
            var memoryslot = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "memoryslot"));
            var internalmemory = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "internalmemory"));
            var cameraprimary = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "cameraprimary"));
            var camerafeatures = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "camerafeatures"));
            var cameravideo = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "cameravideo"));
            var camerasecondary = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "camerasecondary"));
            var optionalother = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "optionalother"));
            var wlan = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "wlan"));
            var bluetooth = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "bluetooth"));
            var gps = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "gps"));
            var radio = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "radio"));
            var usb = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "usb"));
            var sensors = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "sensors"));
            var featuresother = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "featuresother"));
            var batdescription1 = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "batdescription1"));
            var colors = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "colors"));
            var price = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "price"));
            var batteryMusicPlay = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "batmusicplay1"));
            var batteryTalkTime = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "battalktime1"));

            var mobile = new Mobile()
            {
                Name = modelName?.InnerHtml,
                Announced = year?.InnerHtml,
                BatteryMusicPlay = batteryMusicPlay?.InnerHtml,
                BatterySize = batsize?.InnerHtml,
                BatteryTalkTime = batteryTalkTime?.InnerHtml,
                BatteryType = battypehl?.InnerHtml,
                Bluetooth = bluetooth?.InnerHtml,
                BodyDimension = body?.InnerHtml,
                CameraFeatures = camerafeatures?.InnerHtml,
                CameraPixel = camerapixels?.InnerHtml,
                CardSlot = memoryslot?.InnerHtml,
                Chipset = chipset?.InnerHtml,
                Colours = colors?.InnerHtml,
                Cpu = cpu?.InnerHtml,
                Dimensions = dimensions?.InnerHtml,
                DisplayResolution = displayresolution?.InnerHtml,
                DisplaySize = displayres?.InnerHtml,
                DisplayType = displaytype?.InnerHtml,
                Edge = edge?.InnerHtml,
                Gprs = gprstext?.InnerHtml,
                Gps = gps?.InnerHtml,
                Gpu = gpu?.InnerHtml,
                InternalMemory = internalmemory?.InnerHtml,
                Loudspeaker = "Yes",
                MiscellaneousBattery = batdescription1?.InnerHtml,
                ReleasedDate = released?.InnerHtml,
                OS = os?.InnerHtml,
                Video = cameravideo?.InnerHtml,
                Storage = storage?.InnerHtml,
                RAM = ramsize?.InnerHtml,
                Technology = nettech?.InnerHtml,
                Network2GBands = net2g?.InnerHtml,
                Network3GBands = net3g?.InnerHtml,
                Network4GBands = net4g?.InnerHtml,
                Speed = speed?.InnerHtml,
                PrimaryCamera = cameraprimary?.InnerHtml,
                Status = status?.InnerHtml,
                SecondaryCamera = camerasecondary?.InnerHtml,
                MiscellaneousSound = optionalother?.InnerHtml,
                Radio = radio?.InnerHtml,
                Usb = usb?.InnerHtml,
                Sensors = sensors?.InnerHtml,
                MiscellaneousFeatures = featuresother?.InnerHtml,
                Wlan = wlan?.InnerHtml,
                Weight = weight?.InnerHtml,
                Sim = sim?.InnerHtml,
                VideoPixel = videopixels?.InnerHtml,
                BrandId=brandId
            };
            return mobile;
        }

        public List<BrandData> GetAllBrandlink()
        {
            var htmlData = GetHtmlData("http://www.gsmarena.com/makers.php3");
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlData);
            var brandList = new List<BrandData>();
            var brands = new List<Brand>();
            foreach (var item in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'st-text')]"))
            {
                foreach (var element in item.SelectNodes(".//td"))
                {
                    var brandName = element.FirstChild.ChildNodes[0].InnerHtml;
                    var numberOfdevice = element.FirstChild.ChildNodes[2].InnerHtml.Split(' ')[0];
                    var links = element.Descendants("a").ToList()[0].Attributes[0].Value;
                    var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
                    brandList.Add(new BrandData() { Link = appendLink, Name = brandName, NumberOfDevice = numberOfdevice });
                    brands.Add(new Brand() { Name = brandName, Link = links, NumberOfDevice = numberOfdevice });
                }
            }
            _mobileBusinessService.CreateBrand(brands);
            return brandList;
        }

        public string GetHtmlData(string url)
        {
            try
            {
                string data = null;
                var request = WebRequest.Create(url);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    request.Timeout = 300000;
                    using (Stream receiveStream = response.GetResponseStream())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            //  var receiveStream = response.GetResponseStream();
                            StreamReader readStream = null;

                            if (response.CharacterSet == null)
                            {
                                readStream = new StreamReader(receiveStream);
                            }
                            else
                            {
                                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                            }

                            data = readStream.ReadToEnd();
                            //response.Close();
                            //response.Dispose();
                            //readStream.Close();
                            //readStream.Dispose();
                        }
                        return data;
                    }
                }

            }
            catch (WebException e) when (e.Status == WebExceptionStatus.Timeout)
            {
                return null;
            }
        }
    }
    public class BrandData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string NumberOfDevice { get; set; }
    }
}