using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BookStore.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BookStore
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			Database.SetInitializer(new BookDbInitializer());
			Database.SetInitializer(new CourseDbInitializer());

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();

            var storageACcc =
                CloudStorageAccount.Parse(
                    "DefaultEndpointsProtocol=https;AccountName=storage0acc;AccountKey=B5PhADN8FLKMLhm2IicTqI/JoAaR3s3ayqOG+okceAV9Lkm02LHxsoOWF7RNf/bY9dMI1Q9g+yuA0Ls6wIj7Rg==;EndpointSuffix=core.windows.net");

            var blobClient = new CloudBlobClient(new Uri(@"https://storage0acc.blob.core.windows.net"),
                storageACcc.Credentials);
            CloudBlobContainer container = blobClient.GetContainerReference("testblob");
            container.CreateIfNotExists();
            CloudBlockBlob blob = container.GetBlockBlobReference("timeStamp.txt");
            blob.UploadText(DateTime.Now.ToString());
        }

    }
}
