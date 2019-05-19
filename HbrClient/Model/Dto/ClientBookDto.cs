using SQLite;
using System;

namespace HbrClient.Model.Dto
{
    public class ClientBookDto : BookDto, IClientEntity
    {
        public bool ModifiedOffline { get; set; }
    }
}