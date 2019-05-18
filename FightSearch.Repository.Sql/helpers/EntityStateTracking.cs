namespace FightSearch.Repository.Sql.helpers
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public static class EntityStateTracking
    {
        public static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (EntityEntry entry in entries)
            {
                Debug.WriteLine($"Entity: {entry.Entity.GetType().Name},State: {entry.State.ToString()} ");
            }
        }
    }
}