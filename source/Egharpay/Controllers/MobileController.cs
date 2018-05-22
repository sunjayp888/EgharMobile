using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using HtmlAgilityPack;
using Microsoft.Owin.Security.Authorization;
using Filter = Egharpay.Business.Dto.Filter;
using Mobile = Egharpay.Business.Models.Mobile;

namespace Egharpay.Controllers
{
    public class MobileController : BaseController
    {
        private readonly IMobileBusinessService _mobileBusinessService;
        private readonly IBrandBusinessService _brandBusinessService;
        private readonly ISellerBusinessService _sellerBusinessService;
        private readonly IGoogleBusinessService _googleBusinessService;
        private readonly ITrendBusinessService _trendBusinessService;
        private readonly IPersonnelBusinessService _personnelBusinessService;
        public MobileController(IMobileBusinessService mobileBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService, IBrandBusinessService brandBusinessService, ISellerBusinessService sellerBusinessService, IGoogleBusinessService googleBusinessService, ITrendBusinessService trendBusinessService, IPersonnelBusinessService personnelBusinessService) : base(configurationManager, authorizationService)
        {
            _mobileBusinessService = mobileBusinessService;
            _brandBusinessService = brandBusinessService;
            _sellerBusinessService = sellerBusinessService;
            _googleBusinessService = googleBusinessService;
            _trendBusinessService = trendBusinessService;
            _personnelBusinessService = personnelBusinessService;
        }

        // GET: Mobile
        public ActionResult Index(string filter)
        {
            return View(new BaseViewModel { Filter = filter });
        }

        // GET: Apartment/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            //HttpContext.Server.ScriptTimeout = 300000000;
            //var brandResult = await _brandBusinessService.RetrieveBrands();
            //var mobileResult = await _mobileBusinessService.RetrieveMobiles();
            //GetGoogleImages("nokia 6");
            //var number = 255;
            //var singleBrand = brandResult.Items.Where(e => e.BrandId == number).ToList();
            //foreach (var item in singleBrand)
            //{
            //    var newList = new List<Brand>() { item };
            //    var mobileList = CreateMobileData(newList);
            //    await _mobileBusinessService.CreateMobile(mobileList);
            //}

            //foreach (var item in brandResult.Items.Where(e => e.BrandId == 300).ToList())
            //{
            //    var brandMobile = mobileResult.Items.Where(e => e.BrandId == item.BrandId).ToList();
            //    var mobileImageList = CreateMobileImageData(item, brandMobile);
            //    await _mobileBusinessService.CreateMobileImage(mobileImageList);

            //}

            var brands = await _brandBusinessService.RetrieveBrands(e => true);
            var brandList = brands.Items.ToList();
            var viewModel = new MobileViewModel()
            {
                Mobile = new Business.Models.Mobile(),
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

        public async Task<ActionResult> Detail(int? id)
        {
            await CreateMobileFromLink("https://www.gsmarena.com/huawei_honor_10-9157.php");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = new MobileViewModel();
            if (User.IsSuperUserOrAdminOrSeller())
            {
                var seller = await _sellerBusinessService.RetrieveSellerByPersonnelId(UserPersonnelId);
                viewModel.SellerId = seller?.SellerId ?? 0;
            }
            if (User.IsPersonnel())
            {
                var personnel = await _personnelBusinessService.RetrievePersonnel(UserPersonnelId);
                viewModel.Personnel = personnel.Entity;
            }

            var result = await _mobileBusinessService.RetrieveMobile(id.Value);
            viewModel.MobileName = result.Name;
            viewModel.MobileId = result.MobileId;
            viewModel.BrandId = result.BrandId;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(Filter filter, Paging paging, List<OrderBy> orderBy)
        {
            var data = await RetrieveMobiles(filter, paging, orderBy);
            return data;
        }

        [HttpPost]
        public async Task<ActionResult> MobileData(int id)
        {
            var data = await _mobileBusinessService.RetrieveMobile(id);
            return this.JsonNet(data);
        }

        //[HttpPost]
        //public async Task<ActionResult> Seller(Paging paging, List<OrderBy> orderBy)
        //{
        //    var data = await _sellerBusinessService.RetrieveSellers(orderBy, paging);
        //    return this.JsonNet(data);
        //}

        [HttpPost]
        [Route("Mobile/MobileSellers")]
        public async Task<ActionResult> MobileSellers(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            var data = await _sellerBusinessService.RetrieveMobileSellers(searchKeyword, orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        [Route("Mobile/RetrieveSellersByGeoLocation")]
        public async Task<ActionResult> RetrieveSellersByGeoLocation(string pincode, double latitude, double longitude, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(_sellerBusinessService.RetrieveSellersByGeoLocation(latitude, longitude, pincode, orderBy, paging));
        }

        [HttpPost]
        [Route("Mobile/RetrieveCurrentGeoCoordinates")]
        public async Task<ActionResult> RetrieveCurrentGeoCoordinates()
        {
            return this.JsonNet(await _googleBusinessService.RetrieveCurrentGeoCoordinates());
        }

        [HttpPost]
        public async Task<ActionResult> MobileGalleryImage(int id)
        {
            var data = await _mobileBusinessService.RetrieveMobileGalleryImages(id);
            return this.JsonNet(data);
        }

        [HttpPost]
        [Route("Mobile/Search")]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _mobileBusinessService.Search(searchKeyword, orderBy, paging));
        }

        [Route("Mobile/Compare/{brandId}/{mobileId}")]
        public ActionResult Compare(int? brandId, int? mobileId)
        {
            if (brandId.HasValue && mobileId.HasValue)
                return View(new MobileViewModel() { MobileId = mobileId.Value, BrandId = brandId.Value });
            return View();
        }

        [Route("Mobile/RetrieveMobileByBrandIds")]
        public async Task<ActionResult> RetrieveMobileByBrandIds(string[] brandIds)
        {
            var mobileList = new List<Mobile>();
            foreach (var id in brandIds)
            {
                var brandId = Convert.ToInt32(id);
                var data = await _mobileBusinessService.RetrieveMobiles(new Filter() { IsFilter = true, IsBrandFilter = true, BrandId = brandId });
                mobileList.AddRange(data.Items);
            }
            return this.JsonNet(mobileList);
        }

        [Route("Mobile/RetrieveMobileByMobileIds")]
        public async Task<ActionResult> RetrieveMobileByMobileIds(string[] mobileIds)
        {
            var mobileList = new List<Mobile>();
            foreach (var id in mobileIds)
            {
                var mobileId = Convert.ToInt32(id);
                var data = await _mobileBusinessService.RetrieveMobiles(e => e.MobileId == mobileId);
                mobileList.AddRange(data.Items);
            }
            return this.JsonNet(mobileList);
        }

        [HttpPost]
        [OutputCache(Duration = 30000, VaryByParam = "none")]
        public async Task<ActionResult> SearchField()
        {
            var result = await _mobileBusinessService.RetrieveMetaSearchKeyword();
            var data = result.Select(e => e.MetaKeyword).ToList();
            return this.JsonNet(data);
        }

        public ActionResult AllLatestMobile()
        {
            return RedirectToAction("Index", "Mobile", new { filter = "Islatest" });
        }

        [HttpPost]
        [Route("Mobile/RetrieveMobilesInStore")]
        public async Task<ActionResult> RetrieveMobilesInStore(Paging paging, List<OrderBy> orderBy)
        {
            var filter = new Filter() { IsFilter = true, IsDeviceInStore = true };
            return this.JsonNet(await _mobileBusinessService.RetrieveMobiles(filter, orderBy, paging));
        }

        public async Task<ActionResult> BrandMobile(int id)
        {
            var brand = await _brandBusinessService.RetrieveBrand(id);
            if (brand == null)
                return RedirectToAction("Index");
            return View(new BaseViewModel { BrandId = id, BrandName = brand.Name });
        }


        private async Task<ActionResult> RetrieveMobiles(Filter filter, Paging paging = null, List<OrderBy> orderBy = null)
        {
            if (filter != null && filter.IsFilter)
                return this.JsonNet(await _mobileBusinessService.RetrieveMobiles(filter, orderBy, paging));
            return this.JsonNet(await _mobileBusinessService.RetrieveMobiles(e => true, orderBy, paging));
        }

        private async Task CreateTrend()
        {
            var trendHtmlData = GetHtmlData(string.Format("http://www.gsmarena.com{0}{1}", "/", ""));
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(trendHtmlData);
            var newsItem = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'news-item')]");
            foreach (var item in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'news-item')]"))
            {
                var shortDesription = item.ChildNodes.FirstOrDefault(c => c.Name == "a")?
                          .ChildNodes.FirstOrDefault(e => e.Name == "p")?.InnerHtml;

                if (shortDesription != null)
                {
                    var firstOrDefault = item.ChildNodes.FirstOrDefault(c => c.Name == "a")?
                        .ChildNodes.FirstOrDefault(e => e.Name == "div")?
                        .ChildNodes.FirstOrDefault(e => e.Name == "img");
                    if (firstOrDefault != null)
                    {
                        var imageLink1 = firstOrDefault?.Attributes[0].Value;
                        var f = imageLink1.Split('/')[7];
                        var trendShortName = firstOrDefault?.Attributes[1].Value;

                        //Write File
                        const string trendsDirectory = @"G:\SanjayWorkArea\mumbile.com\mumbileapp\source\Egharpay\TrendImage";
                        var uri = new Uri(imageLink1);
                        var filename = f + imageLink1?.Split('/').Last();
                        if (uri.IsFile)
                            filename = System.IO.Path.GetFileName(uri.LocalPath);

                        var saveImageFullPath = Path.Combine(trendsDirectory, filename);
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(new Uri(imageLink1), saveImageFullPath);
                        }

                        var trend = new Trend
                        {
                            Name = trendShortName,
                            ShortDescription = shortDesription,
                            Filename = filename
                        };
                        await _trendBusinessService.CreateTrend(trend);
                    }
                }
            }
        }


        //private List<Mobile> CreateMobileData(List<Brand> brands)
        //{
        //    // var brands = GetAllBrandlink();

        //    //For eg acer phone

        //    var mobileList = new List<Mobile>();
        //    foreach (var brand in brands)
        //    {
        //        var htmlData = GetHtmlData(string.Format("http://www.gsmarena.com{0}{1}", "/", ""));
        //        var htmlDocument = new HtmlDocument();
        //        htmlDocument.LoadHtml(htmlData);

        //        foreach (var item in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'makers')]"))
        //        {
        //            //Get Data For FirstPage
        //            foreach (var element in item.SelectNodes(".//li"))
        //            {
        //                var homeImage = element.Descendants("img").ToList();
        //                var homeImage1 = element.Descendants("a").ToList();
        //                var links = element.Descendants("a").ToList()[0].Attributes[0].Value;
        //                var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
        //                var htmlMobileDetailDocument = new HtmlDocument();
        //                htmlMobileDetailDocument.LoadHtml(GetHtmlData(appendLink));
        //                var testingObject = htmlMobileDetailDocument.DocumentNode.SelectNodes("//*[@data-spec]");
        //                //  var columnName = testingObject.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "modelname"));
        //                mobileList.Add(CreateMobile(testingObject, brand.BrandId));

        //            }

        //            //Get data For Next Pages
        //            if (htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'nav-pages')]") != null)
        //            {
        //                foreach (var nextPageItem in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'nav-pages')]"))
        //                {
        //                    var linkList = nextPageItem.Descendants("a").ToList();
        //                    foreach (var singleLink in linkList)
        //                    {
        //                        var linkForNextPage = singleLink.Attributes[0].Value;
        //                        var appendLinkForNextPage =
        //                            string.Format("http://www.gsmarena.com{0}{1}", "/", linkForNextPage);
        //                        var htmlMobileNextPageDetailDocument = new HtmlDocument();
        //                        htmlMobileNextPageDetailDocument.LoadHtml(GetHtmlData(appendLinkForNextPage));
        //                        foreach (var nextPageitem in htmlMobileNextPageDetailDocument.DocumentNode.SelectNodes("//div[contains(@class, 'makers')]"))
        //                        {
        //                            //Get Data For FirstPage
        //                            foreach (var element in nextPageitem.SelectNodes(".//li"))
        //                            {
        //                                var links = element.Descendants("a").ToList()[0].Attributes[0].Value;
        //                                var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
        //                                var htmlMobileDetailDocument = new HtmlDocument();
        //                                htmlMobileDetailDocument.LoadHtml(GetHtmlData(appendLink));
        //                                var testingObject =
        //                                    htmlMobileDetailDocument.DocumentNode.SelectNodes("//*[@data-spec]");
        //                                //  var columnName = testingObject.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "modelname"));
        //                                mobileList.Add(CreateMobile(testingObject, brand.BrandId));
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return mobileList;
        //}

        //private List<MobileImage> CreateMobileImageData(Brand brand, List<Mobile> mobile)
        //{
        //    // var brands = GetAllBrandlink();

        //    //For eg acer phone

        //    var mobileList = new List<MobileImage>();

        //    var htmlData = GetHtmlData(string.Format("http://www.gsmarena.com{0}{1}", "/", brand.Link));
        //    var htmlDocument = new HtmlDocument();
        //    htmlDocument.LoadHtml(htmlData);

        //    foreach (var item in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'makers')]"))
        //    {
        //        //Get Data For FirstPage
        //        foreach (var element in item.SelectNodes(".//li"))
        //        {
        //            var homeImage = element.Descendants("img").ToList();
        //            var links = element.Descendants("a").ToList()[0].Attributes[0].Value;
        //            var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
        //            var htmlMobileDetailDocument = new HtmlDocument();
        //            htmlMobileDetailDocument.LoadHtml(GetHtmlData(appendLink));
        //            var testingObject = htmlMobileDetailDocument.DocumentNode.SelectNodes("//*[@data-spec]");
        //            //  var columnName = testingObject.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "modelname"));
        //            mobileList.Add(CreateMobileImage(testingObject, brand.Name, brand.BrandId, homeImage[0].Attributes[0].Value, mobile));

        //        }

        //        //Get data For Next Pages
        //        if (htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'nav-pages')]") != null)
        //        {
        //            foreach (var nextPageItem in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'nav-pages')]"))
        //            {
        //                var linkList = nextPageItem.Descendants("a").ToList();
        //                foreach (var singleLink in linkList)
        //                {
        //                    var linkForNextPage = singleLink.Attributes[0].Value;
        //                    var appendLinkForNextPage =
        //                        string.Format("http://www.gsmarena.com{0}{1}", "/", linkForNextPage);
        //                    var htmlMobileNextPageDetailDocument = new HtmlDocument();
        //                    htmlMobileNextPageDetailDocument.LoadHtml(GetHtmlData(appendLinkForNextPage));
        //                    foreach (var nextPageitem in htmlMobileNextPageDetailDocument.DocumentNode.SelectNodes("//div[contains(@class, 'makers')]"))
        //                    {
        //                        //Get Data For FirstPage
        //                        foreach (var element in nextPageitem.SelectNodes(".//li"))
        //                        {
        //                            var homeImage = element.Descendants("img").ToList();
        //                            var links = element.Descendants("a").ToList()[0].Attributes[0].Value;
        //                            var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
        //                            var htmlMobileDetailDocument = new HtmlDocument();
        //                            htmlMobileDetailDocument.LoadHtml(GetHtmlData(appendLink));
        //                            var testingObject =
        //                                htmlMobileDetailDocument.DocumentNode.SelectNodes("//*[@data-spec]");
        //                            //  var columnName = testingObject.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "modelname"));
        //                            mobileList.Add(CreateMobileImage(testingObject, brand.Name, brand.BrandId, homeImage[0].Attributes[0].Value, mobile));
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return mobileList;
        //}

        private Mobile CreateMobile(HtmlNodeCollection htmlNodeCollection, int brandId)
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
                Technology = nettech?.InnerHtml,
                Network2GBands = net2g?.InnerHtml,
                Network3GBands = net3g?.InnerHtml,
                Network4GBands = net4g?.InnerHtml,
                Speed = speed?.InnerHtml,
                Status = status?.InnerHtml,
                MiscellaneousSound = optionalother?.InnerHtml,
                Radio = radio?.InnerHtml,
                Usb = usb?.InnerHtml,
                Sensors = sensors?.InnerHtml,
                MiscellaneousFeatures = featuresother?.InnerHtml,
                Wlan = wlan?.InnerHtml,
                Weight = weight?.InnerHtml,
                Sim = sim?.InnerHtml,
                VideoPixel = videopixels?.InnerHtml,
                BrandId = brandId
            };
            return mobile;
        }

        //public MobileImage CreateMobileImage(HtmlNodeCollection htmlNodeCollection, string brandName, int brandId, string imageLink, List<Mobile> mobile)
        //{
        //    var rootDirectory = @"G:\MobileImage";
        //    var modelName = htmlNodeCollection.FirstOrDefault(e => e.Attributes.Any(t => t.Value == "modelname"));
        //    var mobileId = mobile.FirstOrDefault(e => e.Name.ToLower() == modelName.InnerHtml.ToLower()) == null ? -1 :
        //        mobile.FirstOrDefault(e => e.Name.ToLower() == modelName.InnerHtml.ToLower()).MobileId;

        //    //Create brand Directory
        //    var brandDirectory = Path.Combine(rootDirectory, brandName);

        //    if (!Directory.Exists(brandDirectory))
        //        Directory.CreateDirectory(brandDirectory);

        //    //Create modelName Directory
        //    if (modelName?.InnerHtml != "Samsung :) Smiley")
        //    {
        //        var mobileDirectory = Path.Combine(brandDirectory, modelName?.InnerHtml);
        //        if (!Directory.Exists(mobileDirectory))
        //        {
        //            try
        //            {
        //                Directory.CreateDirectory(mobileDirectory);
        //            }
        //            catch (Exception Ex)
        //            {
        //                Directory.CreateDirectory(mobileDirectory);
        //            }

        //        }

        //        //Write File
        //        var uri = new Uri(imageLink);
        //        var filename = imageLink.Split('/').Last();
        //        if (uri.IsFile)
        //            filename = System.IO.Path.GetFileName(uri.LocalPath);

        //        var saveImageFullPath = Path.Combine(mobileDirectory, filename);
        //        using (var client = new WebClient())
        //        {
        //            client.DownloadFile(new Uri(imageLink), saveImageFullPath);
        //        }

        //        //Return object

        //        return new MobileImage()
        //        {
        //            BrandId = brandId,
        //            FilePath = saveImageFullPath,
        //            GSMLink = imageLink,
        //            MobileId = mobileId
        //        };
        //    }
        //    return new MobileImage()
        //    {
        //        BrandId = 300,
        //        FilePath = "",
        //        GSMLink = "",
        //        MobileId = mobileId
        //    };
        //}

        //private void GetGoogleImages(string searchTerm)
        //{
        //    var yahooUrl =


        //        "https://in.images.search.yahoo.com/search/images;_ylt=A2oKiHHojLNZ7C8AiRW8HAx.;_ylc=X1MDMjExNDcyMzAwNARfcgMyBGJjawNhdDI3YnRoY3IxcWVoJTI2YiUzRDMlMjZzJTNEOTkEZnIDBGdwcmlkA0hYdmxwRkdoUjRlWVAzZ2NWZjhtaEEEbXRlc3RpZANudWxsBG5fc3VnZwMxMARvcmlnaW4DaW4uaW1hZ2VzLnNlYXJjaC55YWhvby5jb20EcG9zAzAEcHFzdHIDBHBxc3RybAMEcXN0cmwDNwRxdWVyeQNub2tpYSA2BHRfc3RtcAMxNTA0OTM5Mjk4BHZ0ZXN0aWQDbnVsbA--?gprid=HXvlpFGhR4eYP3gcVf8mhA&pvid=_iSrdzEwNi6uiOvsWbDp0Ql9MTIzLgAAAAD40hUK&fr2=sb-top-in.images.search.yahoo.com&p=" + searchTerm.Replace(' ', '+') + "&ei=UTF-8&iscqry=&fr=sfp";
        //    var url = "https://www.google.co.in/search?q=" + searchTerm.Replace(' ', '+') + "&source=lnms&tbm=isch&sa=X&ved=0ahUKEwjO0NHwwJXWAhXINo8KHTurAX0Q_AUICygC&biw=1600&bih=804";
        //    var htmlData = GetHtmlData(yahooUrl);
        //    var htmlDocument = new HtmlDocument();
        //    htmlDocument.LoadHtml(htmlData);
        //    var nodes = htmlDocument.DocumentNode;
        //    var classData = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'sres-cntr')]");
        //    foreach (var item in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'sres-cntr')]"))
        //    {
        //        //Get Data For FirstPage
        //        foreach (var element in item.SelectNodes(".//li"))
        //        {
        //            var homeImage = element.Descendants("img").ToList();
        //            var homeImage1 = element.Descendants("a").ToList();
        //            //var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
        //            //var htmlMobileDetailDocument = new HtmlDocument();
        //            //htmlMobileDetailDocument.LoadHtml(GetHtmlData(appendLink));
        //            //var testingObject = htmlMobileDetailDocument.DocumentNode.SelectNodes("//*[@data-spec]");

        //        }
        //    }
        //}

        //public List<BrandData> GetAllBrandlink()
        //{
        //    var htmlData = GetHtmlData("http://www.gsmarena.com/makers.php3");
        //    var htmlDocument = new HtmlDocument();
        //    htmlDocument.LoadHtml(htmlData);
        //    var brandList = new List<BrandData>();
        //    var brands = new List<Brand>();
        //    foreach (var item in htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'st-text')]"))
        //    {
        //        foreach (var element in item.SelectNodes(".//td"))
        //        {
        //            var brandName = element.FirstChild.ChildNodes[0].InnerHtml;
        //            var numberOfdevice = element.FirstChild.ChildNodes[2].InnerHtml.Split(' ')[0];
        //            var links = element.Descendants("a").ToList()[0].Attributes[0].Value;
        //            var appendLink = string.Format("http://www.gsmarena.com{0}{1}", "/", links);
        //            brandList.Add(new BrandData() { Link = appendLink, Name = brandName, NumberOfDevice = numberOfdevice });
        //            brands.Add(new Brand() { Name = brandName,  NumberOfDevice = numberOfdevice });
        //        }
        //    }
        //    _mobileBusinessService.CreateBrand(brands);
        //    return brandList;
        //}

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
            catch (WebException e)
            {
                return null;
            }
        }

        private async Task CreateMobileFromLink(string url)
        {
            var htmlData = GetHtmlData(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlData);
            var obj = htmlDocument.DocumentNode.SelectNodes("//*[@data-spec]");
            var mobileData = CreateMobile(obj, 329);
            await _mobileBusinessService.CreateMobile(mobileData);
        }
    }
    //public class BrandData
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string Link { get; set; }
    //    public string NumberOfDevice { get; set; }
    //}


}