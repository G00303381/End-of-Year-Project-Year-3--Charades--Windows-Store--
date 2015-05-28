using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Leaderboard.DataObjects
{
    public class Player : EntityData
    {
        public string Name { get; set; }
    }
}