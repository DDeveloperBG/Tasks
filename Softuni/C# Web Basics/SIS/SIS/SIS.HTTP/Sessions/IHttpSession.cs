namespace SIS.HTTP.Sessions
{
    public interface IHttpSession
    {
        public string Id { get; }

        public object GetParameter(string name);

        public bool ContainsParameter(string name);

        public void AddParameter(string name, object parameter);

        public void ClearParameters();
    }
}
