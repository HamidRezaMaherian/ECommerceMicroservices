namespace UI.Domain.ValueObjects
{
	public class Blob : ValueObject
	{
		public string Server { get; private set; }
		public string RelativePath { get; private set; }
		public string Name { get; private set; }

		public Blob(string server, string relativePath, string name)
		{
			Server = server;
			RelativePath = relativePath;
			Name = name;
		}
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Server;
			yield return RelativePath;
			yield return Name;
		}
		public override string ToString()
		{
			return Path.Combine(Server, RelativePath, Name);
		}
	}
}
