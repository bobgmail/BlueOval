
    public class ClientIdService
    {
        public string ClientId { get; private set; } = string.Empty; // Default value

        public void SetClientId(string id)
        {
            ClientId = id;
            // You might want to add an event here to notify components that the ID has been set
            // public event Action OnClientIdSet;
            // private void NotifyClientIdSet() => OnClientIdSet?.Invoke();
        }
    }

