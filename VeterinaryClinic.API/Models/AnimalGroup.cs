﻿using System;
using System.Collections.Generic;

namespace VeterinaryClinic.API.Models;

public partial class AnimalGroup
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
