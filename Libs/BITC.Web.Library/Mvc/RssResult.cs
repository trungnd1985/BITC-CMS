﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace BITC.Web.Library.Mvc
{
    public class RssResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }

        private readonly SyndicationFeedFormatter feed;
        public SyndicationFeedFormatter Feed{
            get { return feed; }
        }

        public RssResult(SyndicationFeedFormatter feed)
        {
            this.feed = feed;
        }

        public override void ExecuteResult(ControllerContext context) {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/rss+xml";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (feed != null)
                using (var xmlWriter = new XmlTextWriter(response.Output)) {
                    xmlWriter.Formatting = Formatting.Indented;
                    feed.WriteTo(xmlWriter);
                }
        }
    }
}
