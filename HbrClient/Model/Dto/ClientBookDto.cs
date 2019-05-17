﻿using SQLite;
using System;

namespace HbrClient.Model.Dto
{
    public class ClientBookDto: BookDto, IClientEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ClientId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}