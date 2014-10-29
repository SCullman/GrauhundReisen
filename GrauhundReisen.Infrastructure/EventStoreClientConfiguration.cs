using System;

namespace Grauhundreisen.Infrastructure
{

	public struct EventStoreClientConfiguration{

		public Uri ServerUri;
		public String InitActionName;
		public String StoreActionName;
		public String RetrieveActionName;

		public String RemoveActionName;

		public String AccountId;

        public Func<string, Object> DeserializeEvent;

		public bool IsComplete(){

			return ServerUri.ToString ().IsNotNullOrEmpty ()
				&& InitActionName.IsNotNullOrEmpty ()
				&& StoreActionName.IsNotNullOrEmpty ()
				&& RetrieveActionName.IsNotNullOrEmpty ()
				&& AccountId.IsNotNullOrEmpty ();
		}
	}
}
