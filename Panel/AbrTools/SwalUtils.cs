using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrieTechnologies.Razor.SweetAlert2;

namespace AbrBlazorTools
{
    public static class SwalUtils
    {
        public static async Task<SweetAlertResult> ShowToast(this SweetAlertService service, string message)
        {
            return await service.FireAsync(
                new SweetAlertOptions
                {
                    Toast = true,
                    Text = message,
                    Timer = 5000
                });
        }


        public static async Task<SweetAlertResult> ShowSwal(this SweetAlertService service, string title, string message, SweetAlertIcon icon)
        {
            if (icon == SweetAlertIcon.Question)
                return await service.FireAsync(new SweetAlertOptions
                {
                    ConfirmButtonText="Yes",
                    ShowConfirmButton = true,
                    ShowDenyButton = true,
                    ShowCloseButton = false,                    
                    Html = message,
                    Icon = icon,
                    Title = title,
                });
            return await service.FireAsync(new SweetAlertOptions
            {                
                Html = message,
                Icon = icon,
                Title = title,
            });
        }
        
        public static int TransformNumber (double input , int min , int max , int minDeg , int maxDeg )
        {
            input = Math.Max(input, min);
            input = Math.Min(input, max);
            // zero based
            input /= (max - min);
            // transform
            input *= (maxDeg - minDeg);

            return (int) input;
        }
    }
}
