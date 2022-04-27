using GM.VM.Variables;

namespace GM.VM.Functions;

public class LogFunction : Function
{
	public override string Name { get; internal set; } = "Log";

	public override Variable Run( Context context, params Variable[] arguments )
	{
		if ( arguments.Length == 0 )
			return null;

		var argument = arguments[0];

		string output;
		if ( argument is null )
		{
			output = "(null)";
		}
		else
		{
			argument = argument.Get( context );
			switch ( argument.Type )
			{
				case Enums.VariableType.Number:
					output = $"{(argument as NumberVariable).Value}";
					break;
				case Enums.VariableType.String:
					output = $"\"{(argument as StringVariable).Value}\"";
					break;
				case Enums.VariableType.FromPool:
					output = $"pointer to {(argument as FromPoolVariable).VariableName}";
					break;
				default:
					output = "(unknown)";
					break;
			}
		}

		Log.Info( $"{output} @ {string.Join( " <- ", context.GetTrace() )}" );
		return null;
	}
}
