using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HassRestClient.Models;

namespace NuimoHass.WPF.ViewModels
{
    public class HassEntity
    {
        public HassEntity(EntityState state)
        {
            FriendlyName = $"{(string)state.Attributes["friendly_name"]} (Id: {state.EntityId})";
            Id = state.EntityId;
        }

        public HassEntity()
        {
            Id = "";
        }
        public string FriendlyName { get; set; }
        public string Id { get; set; }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as HassEntity;
            return other != null && Equals(other);
        }

        protected bool Equals(HassEntity other)
        {
            return string.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
