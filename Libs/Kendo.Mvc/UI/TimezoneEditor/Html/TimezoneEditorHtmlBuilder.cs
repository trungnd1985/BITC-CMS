﻿namespace Kendo.Mvc.UI
{
    public class TimezoneEditorHtmlBuilder
    {
        public TimezoneEditorHtmlBuilder(TimezoneEditor component)
        {
            this.Component = component;
        }

        public TimezoneEditor Component
        {
            get;
            private set;
        }

        public IHtmlNode Build()
        {
            var value = Component.GetValue(Component.Value);

            return new HtmlElement("div")
                .Attribute("id", Component.Id)
                .Attributes(new { name = Component.Name, id = Component.Id })
                //.ToggleAttribute("value", value, value.HasValue())
                .Attributes(Component.HtmlAttributes)
                .Attributes(Component.GetUnobtrusiveValidationAttributes())
                .ToggleClass("input-validation-error", !Component.IsValid());
        }
    }
}
