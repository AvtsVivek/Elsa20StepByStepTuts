using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace P20731ElsaDashboard.Controllers.ElsaEndpoints.Activities
{
    public class MsActivityDescriptorJsonConverterForList
    : JsonConverter<List<ActivityDescriptor>>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }
        public override List<ActivityDescriptor>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, List<ActivityDescriptor> valueList, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var value in valueList)
            {
                writer.WriteStartObject();

                writer.WriteString(nameof(ActivityDescriptor.Type), value?.Type);
                writer.WriteString(nameof(ActivityDescriptor.DisplayName), value?.DisplayName);
                writer.WriteString(nameof(ActivityDescriptor.Description), value?.Description);
                writer.WriteString(nameof(ActivityDescriptor.Category), value?.Category);
                writer.WriteNumber(nameof(ActivityDescriptor.Traits), (int)value?.Traits!);

                //writer.WriteString(nameof(ActivityPropertyDescriptor.SupportedSyntaxes), supportedSyntaxes);
                if (value?.Outcomes != null)
                {
                    writer.WriteStartArray(nameof(ActivityDescriptor.Outcomes));
                    value?.Outcomes?.ToList().ForEach(outcome => writer.WriteStringValue(outcome));
                    writer.WriteEndArray();
                }

                if (value?.Properties != null)
                {
                    writer.WriteStartArray(nameof(ActivityDescriptor.Properties));
                    value?.Properties?.ToList().ForEach(property => WriteActivityPropertyDescriptor(writer, property));
                    writer.WriteEndArray();
                }

                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        private void WriteActivityPropertyDescriptor(Utf8JsonWriter writer, ActivityPropertyDescriptor value)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(ActivityPropertyDescriptor.Name), value?.Name);
            writer.WriteString(nameof(ActivityPropertyDescriptor.Type), value?.Type);
            writer.WriteString(nameof(ActivityPropertyDescriptor.UIHint), value?.UIHint);
            writer.WriteString(nameof(ActivityPropertyDescriptor.Category), value?.Category);
            writer.WriteString(nameof(ActivityPropertyDescriptor.Label), value?.Label);
            writer.WriteString(nameof(ActivityPropertyDescriptor.Hint), value?.Hint);

            //var supportedSyntaxes = "[";
            //value?.SupportedSyntaxes?.ToList().ForEach(s => supportedSyntaxes = supportedSyntaxes + s + ",");
            //supportedSyntaxes.TrimEnd(',');
            //supportedSyntaxes  = supportedSyntaxes + "]";

            //writer.WriteString(nameof(ActivityPropertyDescriptor.SupportedSyntaxes), supportedSyntaxes);
            if (value?.SupportedSyntaxes != null)
            {
                writer.WriteStartArray(nameof(ActivityPropertyDescriptor.SupportedSyntaxes));
                value?.SupportedSyntaxes?.ToList().ForEach(supportedSyntax => writer.WriteStringValue(supportedSyntax));
                writer.WriteEndArray();
            }

            if (value?.Options != null)
            {
                if (value?.Options.GetType().Name == "String[]")
                {
                    writer.WriteStartArray(nameof(ActivityPropertyDescriptor.Options));

                    var stringArray = (String[])value?.Options!;

                    foreach (var stringItem in stringArray)
                    {
                        writer.WriteStringValue(stringItem);
                    }
                    //writer.WriteString(nameof(ActivityPropertyDescriptor.Options), value?.Options.ToString());
                    writer.WriteEndArray();
                }
                if (value?.Options.GetType().Name == "Object[]")
                {
                    var objectArray = (Object[])value?.Options!;

                    writer.WriteStartArray(nameof(ActivityPropertyDescriptor.Options));

                    foreach (var objectItem in objectArray)
                    {
                        writer.WriteStartObject();
                        var innerArray = (List<IDictionary<string, object>>)objectItem;

                        foreach (var innerItem in innerArray)
                        {
                            var result = innerItem.FirstOrDefault(x => x.Key == "text").Value as ExpandoObject;
                            //var temp = innerItem.Where(v => v.Key == keyNameVariable).Select(x => x.Value).FirstOrDefault();
                            var eoKey = innerItem.Keys.ToList().FirstOrDefault();
                            var eoValue = innerItem[eoKey!];
                            writer.WriteString(eoKey!, eoValue.ToString());
                        }

                        //foreach (var innerKey in innerArray)
                        //{}
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();

                }
                //else
                //{
                //    writer.WriteStartArray(nameof(ActivityPropertyDescriptor.Options));
                //    //value?.Options?.ToList().ForEach(supportedSyntax => writer.WriteStringValue(supportedSyntax));
                //    writer.WriteEndArray();
                //}
            }

            writer.WriteEndObject();
        }
    }
}
