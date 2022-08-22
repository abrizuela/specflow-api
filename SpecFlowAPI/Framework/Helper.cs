using Bogus;

namespace SpecFlowAPI
{
	public class Helper
	{
		public Faker Faker { get; }

		public Helper()
		{
			Faker = new();
		}

		public string GenerateFakeListId()
		{
			return Faker.Random.AlphaNumeric(22);
		}
	}
}
