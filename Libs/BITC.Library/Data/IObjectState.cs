using System.ComponentModel.DataAnnotations.Schema;

namespace BITC.Library.Data
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
