using System.ComponentModel.DataAnnotations;

namespace ProvaCandidato.Data.Validador
{
    public class CidadeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || value.ToString() == "-1")
                return false;           

            return true;
        }
    }
}
