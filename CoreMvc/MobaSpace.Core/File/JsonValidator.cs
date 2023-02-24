using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobaSpace.Core.File
{
    public class JsonValidator : ValidationAttribute
    {
        public bool Optionnal { get; set; } = false;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                if(Optionnal)
                {
                    return ValidationResult.Success;                    
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else if (value.GetType() == typeof(string))
            {
                var jsonStr = (string)value;
                try
                {
                    JsonConvert.DeserializeObject(jsonStr);
                    return ValidationResult.Success;
                }
                catch
                {
                    return new ValidationResult("Invalid json provided");
                }
            }
            else
            {
                throw new ArgumentException("Invalid attribute usage");
            }
        }
    }
}
