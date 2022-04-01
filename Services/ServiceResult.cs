using System.ComponentModel;

namespace Pdi_Car_Rent.Services
{

    public enum ServiceResultStatus
    {
        [Description("Błąd")]
        Error = 0,
        [Description("Sukces")]
        Succes = 1,
        [Description("Ostrzeżenie")]
        Warning,
        [Description("Informacja")]
        Info,
    }
    public class ServiceResult
    {
        public ServiceResultStatus Result { get; set; }
        public ICollection<String> Messages { get; set; }



        public ServiceResult()
        {
            Result = ServiceResultStatus.Succes;
            Messages = new List<string>();
        }



        public static Dictionary<string, ServiceResult> CommonResults { get; set; } = new Dictionary<string, ServiceResult>()
        {
              {"NotFound" , new ServiceResult() {
                  Result =ServiceResultStatus.Error,
                  Messages = new List<string>( new string[] { "Nie znaleziono obiektu" })  } },
              {"OK" , new ServiceResult() {
                  Result =ServiceResultStatus.Succes,
                  Messages = new List<string>()  } }
        };
    }
}

