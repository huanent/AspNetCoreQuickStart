using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models
{
    /// <summary>
    /// 数据模型，主要职责为数据验证
    /// </summary>
    public class DemoModel
    {
        [Required]
        public string Name { get; set; }
    }
}
