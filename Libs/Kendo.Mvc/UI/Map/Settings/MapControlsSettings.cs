namespace Kendo.Mvc.UI
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Routing;
    using Kendo.Mvc.Extensions;

    public class MapControlsSettings : JsonObject
    {
        public MapControlsSettings()
        {
            //>> Initialization
        
            Navigator = new MapControlsNavigatorSettings();
                
            Zoom = new MapControlsZoomSettings();
                
        //<< Initialization

            
        }

        

        //>> Fields
        
        public bool? Attribution { get; set; }
        
        public MapControlsNavigatorSettings Navigator
        {
            get;
            private set;
        }
        
        public MapControlsZoomSettings Zoom
        {
            get;
            private set;
        }
        
        //<< Fields

        protected override void Serialize(IDictionary<string, object> json)
        {
            //>> Serialization
        
            if (Attribution.HasValue)
            {
                json["attribution"] = Attribution;
            }
                
            var navigator = Navigator.ToJson();
            if (navigator.Any())
            {
                json["navigator"] = navigator;
            }
                
            var zoom = Zoom.ToJson();
            if (zoom.Any())
            {
                json["zoom"] = zoom;
            }
                
        //<< Serialization
        }
    }
}
