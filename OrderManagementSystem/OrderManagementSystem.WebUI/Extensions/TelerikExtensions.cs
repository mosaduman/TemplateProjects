using BasicExtensions;
using OrderManagementSystem.Core.Filters;

namespace OrderManagementSystem.WebUI.Extensions
{
    public static class TelerikExtensions
    {

        private static IFilter ToFilter(this Kendo.Mvc.IFilterDescriptor descriptor) => (descriptor.GetType().Name == typeof(Kendo.Mvc.FilterDescriptor).Name) ? (IFilter)((Kendo.Mvc.FilterDescriptor)descriptor).ToF() : ((Kendo.Mvc.CompositeFilterDescriptor)descriptor).ToC();
        private static Filter ToF(this Kendo.Mvc.FilterDescriptor descriptor) => new Filter() { Name = descriptor.Member, Operator = (FilterOperator)descriptor.Operator, Value = descriptor.Value != null ? descriptor.Value.GetType().Name == typeof(bool).Name ? (bool)descriptor.Value ? "1" : "0" : descriptor.Value.GetType().Name == typeof(System.DateTime).Name ? ((System.DateTime)descriptor.Value).ToDatetimeString(BasicExtensions.Models.DatetimeFormat.LongDateTimeFormat.DbFormat) : descriptor.Value.GetType().Name == typeof(float).Name || descriptor.Value.GetType().Name == typeof(decimal).Name || descriptor.Value.GetType().Name == typeof(double).Name ? descriptor.Value.ToString().Replace(',', '.') : descriptor.Value.ToString() : null };
        private static CompFilter ToC(this Kendo.Mvc.CompositeFilterDescriptor descriptor) => new CompFilter() { Operator = descriptor.LogicalOperator == Kendo.Mvc.FilterCompositionLogicalOperator.And ? FilterOP.AND : FilterOP.OR, Filters = descriptor.FilterDescriptors.Select(s => (IFilter)(s.GetType().Name == typeof(Kendo.Mvc.FilterDescriptor).Name ? (IFilter)((Kendo.Mvc.FilterDescriptor)s).ToF() : ((Kendo.Mvc.CompositeFilterDescriptor)s).ToC())).ToList() };
        public static DataSourceRequest ToFilterRequest(this Kendo.Mvc.UI.DataSourceRequest request, string defSort) => new DataSourceRequest() { DefSort = defSort, PageNumber = request.Page, PageSize = request.PageSize, Sort = request.Sorts != null && request.Sorts.Any() ? new SortDescriptor() { Direction = request.Sorts.First().SortDirection == Kendo.Mvc.ListSortDirection.Ascending ? SortDirection.ASC : SortDirection.DESC, Name = request.Sorts.First().Member, } : null, Filters = request.Filters != null && request.Filters.Any() ? request.Filters.Select(s => s.ToFilter()).ToList() : null };
    }
}
