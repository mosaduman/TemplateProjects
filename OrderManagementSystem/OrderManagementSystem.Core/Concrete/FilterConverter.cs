using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OrderManagementSystem.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Concrete
{
    public class FilterConverter : JsonConverter<IFilter>
    {
        public override bool CanWrite => false;

        public override IFilter ReadJson(JsonReader reader, Type objectType, IFilter existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            char filterType = jsonObject["Type"].ToObject<char>();
            IFilter filter;

            switch (filterType)
            {
                case 'F':
                    filter = new Filter
                    {
                        Name = jsonObject["Name"].ToString(),
                        Operator = jsonObject["Operator"].ToObject<FilterOperator>(),
                        Value = jsonObject["Value"].ToString()
                    };
                    break;

                case 'C':
                    FilterOP operatorType = jsonObject["Operator"].ToObject<FilterOP>();
                    //List<IFilter> filters = jsonObject["Filters"].ToObject<List<Filter>>(serializer);
                    if (jsonObject["Filters"] != null)
                    {

                        List<IFilter> filters = JsonConvert.DeserializeObject<List<IFilter>>(jsonObject["Filters"].ToString(), new FilterConverter());
                        filter = new CompFilter(operatorType, filters.ToArray());
                    }
                    else
                    {
                        filter = new CompFilter(operatorType, null);
                    }
                    break;

                default:
                    throw new JsonSerializationException("Invalid filter type.");
            }
            return filter;
        }

        public override void WriteJson(JsonWriter writer, IFilter value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
