using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using WorkDiaryWebApp.Core.Constants;

namespace WorkDiaryWebApp.Core.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly string custumDateFormat;

        public DateTimeModelBinder(string _custumDateFormat)
        {
            custumDateFormat = _custumDateFormat;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueResult != ValueProviderResult.None && !String.IsNullOrEmpty(valueResult.FirstValue))
            {
                DateTime actualValue = DateTime.MinValue;
                bool success = false;
                string dateValue = valueResult.FirstValue;

                try
                {
                    actualValue = DateTime.ParseExact(dateValue, FormatConstant.DateTimeFormat, CultureInfo.InvariantCulture);
                    success = true;
                }
                catch (FormatException)
                {
                    try
                    {
                        actualValue = DateTime.Parse(dateValue, new CultureInfo("bg-bg"));
                    }
                    catch (Exception e)
                    {
                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    }
                   
                }

                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }

                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(actualValue);
                }
            }

            return Task.CompletedTask;
        }
    }
    
}
