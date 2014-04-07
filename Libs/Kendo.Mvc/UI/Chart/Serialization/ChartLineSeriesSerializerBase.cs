﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.Extensions;
namespace Kendo.Mvc.UI
{
    internal abstract class ChartLineSeriesSerializerBase : ChartSeriesSerializerBase
    {
        private readonly ILineSeries series;

        public ChartLineSeriesSerializerBase(ILineSeries series)
            : base(series)
        {
            this.series = series;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("type", series.Orientation == ChartSeriesOrientation.Horizontal ? "line" : "verticalLine")
                .Add("stack", series.Stacked, false)
                .Add("aggregate", series.Aggregate.ToString().ToLowerInvariant(), () => series.Aggregate != null)
                .Add("field", series.Member, () => { return series.Data == null && series.Member.HasValue(); })
                .Add("categoryField", series.CategoryMember, () => { return series.Data == null && series.CategoryMember.HasValue(); })
                .Add("data", series.Data, () => { return series.Data != null; })
                .Add("width", series.Width, () => series.Width.HasValue)
                .Add("dashType", series.DashType.ToString().ToLowerInvariant(), () => series.DashType.HasValue)
                .Add("noteTextField", series.NoteTextMember, () => series.NoteTextMember.HasValue())
                .Add("missingValues", series.MissingValues.ToString().ToLowerInvariant(),
                                      () => series.MissingValues.HasValue);

            var labelsData = series.Labels.CreateSerializer().Serialize();
            if (labelsData.Count > 0)
            {
                result.Add("labels", labelsData);
            }

            var markers = series.Markers.CreateSerializer().Serialize();
            if (markers.Count > 0)
            {
                result.Add("markers", markers);
            }

            return result;
        }
    }
}
