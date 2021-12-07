using System.Collections.Concurrent;

namespace SIS.HTTP.Sessions
{
    public static class HttpSessionStorage
    {
        public const string SessionCookieKey = "SIS_ID";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions;

        static HttpSessionStorage()
        {
            sessions = new ConcurrentDictionary<string, HttpSession>();
        }

        public static bool ContainsSession(string id)
        {
            return sessions.ContainsKey(id);
        }

        public static IHttpSession GetSession(string id)
        {
            return sessions.GetOrAdd(id, _ => new HttpSession(id));
        }

        public static void RemoveSession(string id)
        {
            sessions.TryRemove(id, out _);
        }
    }
}
