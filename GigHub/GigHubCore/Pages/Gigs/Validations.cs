using System;
using System.ComponentModel.DataAnnotations;

namespace GigHubCore.Pages.Gigs
{
    //Classes de Validação de Data e Hora no cadastro de Shows (Gig)
    public class DateValidation : ValidationAttribute
    {
        //Tenta fazer a converção e verifica se a data já passou
        public override bool IsValid(object value)
        {
            var isValid = DateTime.TryParse(Convert.ToString(value), out DateTime dateTime);
            return (dateTime > DateTime.Now);
        }
    }

    public class TimeValidation : ValidationAttribute
    {
        //Tenta fazer a conversão
        public override bool IsValid(object value)
        {
            var isValid = DateTime.TryParse(Convert.ToString(value), out DateTime dateTime);
            return (isValid);
        }
    }
}
