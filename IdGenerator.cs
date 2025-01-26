using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiNewsApp2
{
    public static class IdGenerator
    {
        private const string LastIdKey = "LastUsedId";

        public static int GetNextId()
        {
            int nextId = Preferences.Get(LastIdKey, 0) + 1;
            Preferences.Set(LastIdKey, nextId);
            return nextId;
        }

        public static void ResetId()
        {
            Preferences.Set(LastIdKey, 0);
        }
    }
}
