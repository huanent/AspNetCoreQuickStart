using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dtos
{
    public class DemoItemDto
    {
        [Required]
        [CustomValidation(typeof(DemoItemDto), nameof(OrderValidate))]
        public int Order { get; set; }

        public DemoItem ToDemoItem(Guid id)
        {
            return new DemoItem
            {
                Order = Order,
                DemoId = id
            };
        }

        public static ValidationResult OrderValidate(int order)
        {
            if (order % 2 == 1)
            {
                return new ValidationResult("order必须为偶数");
            }
            return ValidationResult.Success;
        }
    }
}
