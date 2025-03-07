using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KpoApi.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CardiogramState
{
    [Description("Просмотрено")]
    Viewed = 0,
    [Description("Не просмотрено")]
    NoViewed = 1,
    [Description("Одобрено")]
    Approved = 2
}  