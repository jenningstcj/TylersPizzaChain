namespace TylersPizzaChain.Pipelines.BuildingBlocks
{
    public static class IConfigurationExtensions
	{
		public static int GetLeadTimeInMinutes_Pickup(this IConfiguration configuration)
			=> configuration.GetValue<int>("Options:LeadTimesInMinutes:Pickup");

        public static int GetLeadTimeInMinutes_EatIn(this IConfiguration configuration)
            => configuration.GetValue<int>("Options:LeadTimesInMinutes:EatIn");

		public static int GetLeadTimeInMinutes_Delivery(this IConfiguration configuration)
            => configuration.GetValue<int>("Options:LeadTimesInMinutes:Delivery");
	}
}

