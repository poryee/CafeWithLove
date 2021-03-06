﻿using System.Web;
using System.Web.Optimization;

namespace CafeWithLove
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/cafedetails").Include(
                        "~/Scripts/cafedetails.js*"));

            bundles.Add(new ScriptBundle("~/bundles/featuredcafes").Include(
                        "~/Scripts/featuredcafes.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/Site.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootflat.css", new CssRewriteUrlTransform()));
            //"~/Content/*.css", new CssRewriteUrlTransform()));

            BundleTable.EnableOptimizations = true;
        }
        
    }
    
}
