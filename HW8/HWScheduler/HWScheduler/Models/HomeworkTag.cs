using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HWScheduler.Models
{
    [Keyless]
    [Index(nameof(HwId), nameof(LabelId), Name = "UQ__Homework__44A4ECFE36DD3ADF", IsUnique = true)]
    public partial class HomeworkTag
    {
        public int HwId { get; set; }
        public int LabelId { get; set; }

        [ForeignKey(nameof(HwId))]
        public virtual Homework Hw { get; set; }
        [ForeignKey(nameof(LabelId))]
        public virtual Tag Label { get; set; }
    }
}
