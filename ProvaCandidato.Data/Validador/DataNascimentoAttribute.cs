using System;
using System.ComponentModel.DataAnnotations;

namespace ProvaCandidato.Data.Validador
{
    public class DataNascimentoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            DateTime dataNascimento = (DateTime)value;

            if (dataNascimento.Date > DateTime.Now.Date)
                return false;

            return true;
        }
    }
}
