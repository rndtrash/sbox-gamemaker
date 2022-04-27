using GM.Enums;

namespace GM.VM.Variables;

public class StringVariable : Variable
{
	public override VariableType Type => VariableType.String;

	public string Value { get; set; }

	public StringVariable( string value )
	{
		Value = value;
	}

	public override Variable Clone( Context _ ) => new StringVariable( Value );

	public override Variable Get( Context _ ) => this;
}
