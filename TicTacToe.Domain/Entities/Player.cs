using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TicTacToe.Domain.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Player
    {
        X,        
        O
    }
}
