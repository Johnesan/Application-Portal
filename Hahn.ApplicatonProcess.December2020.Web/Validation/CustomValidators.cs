using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Validation
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> ValidCountry<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(country => new HttpClient().GetAsync($"https://restcountries.eu/rest/v2/name/{country}?fullText=true").Result.IsSuccessStatusCode);
        }
    }
}
