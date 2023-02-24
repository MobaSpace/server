using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobaSpace.Core.File
{
    public class AllowedExtensions : ValidationAttribute
    {
        public string Extensions { get; set; }
        public bool Optionnal { get; set; }

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
            else if ((value as IFormFile) != null)
            {
                var file = (IFormFile)value;
                if (!this.ValidateExtension(Path.GetExtension(file.FileName)))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else if (value.GetType() == typeof(string))
            {
                var filename = (string)value;
                if (!this.ValidateExtension(Path.GetExtension(filename)))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else
            {
                throw new ArgumentException("Invalid attribute usage");
            }
            
            return ValidationResult.Success;
        }
        private bool ValidateExtension(string ext)
        {
            return this.Extensions.Contains(ext);
        }
    }
}
