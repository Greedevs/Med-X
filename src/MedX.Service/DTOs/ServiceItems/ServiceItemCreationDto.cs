using MedX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedX.Service.DTOs.ServiceItems;

public class ServiceItemCreationDto
{
    public long PatientId { get; set; }
    public long ServiceId { get; set; }
    public float Quantity { get; set; }
}
